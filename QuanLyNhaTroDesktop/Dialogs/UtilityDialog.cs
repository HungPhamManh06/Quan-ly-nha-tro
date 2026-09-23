using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop.Dialogs;

/// <summary>
/// Dialog ghi/chỉnh sửa chỉ số điện nước theo phòng + kỳ.
/// Tự gợi ý chỉ số cũ từ kỳ trước và đơn giá từ lần ghi gần nhất.
///
/// GIAO DIỆN: UtilityDialog.Designer.cs — mở bằng Visual Studio Designer để chỉnh sửa.
/// File này chỉ chứa phần XỬ LÝ.
/// </summary>
public partial class UtilityDialog : Form
{
    private readonly UtilityReading? _reading;
    private readonly IUtilityService? _utilityService;
    private readonly IRoomService? _roomService;

    private List<Room> _rooms = new();

    // Xếp hàng hai luồng truy vấn của dialog: lần nạp ban đầu (Load) và sự kiện đổi phòng.
    // Gán cboRoom.DataSource trong lúc nạp làm sự kiện đổi phòng chạy NGAY, nếu không xếp hàng
    // thì hai truy vấn cùng lúc trên một DbContext → SQL Server báo
    // "A second operation was started on this context instance" (SQLite nhanh nên ít khi lộ).
    private readonly SemaphoreSlim _dbLock = new(1, 1);

    /// <summary>Dùng cho Visual Studio Designer (không truy cập database).</summary>
    public UtilityDialog()
    {
        InitializeComponent();
        Dpi.ScaleForm(this);
    }

    public UtilityDialog(UtilityReading? reading, IUtilityService utilityService, IRoomService roomService) : this()
    {
        _reading = reading;
        _utilityService = utilityService;
        _roomService = roomService;

        Text = reading == null ? "Ghi chỉ số điện nước" : $"Sửa chỉ số ({reading.Room?.MaPhongKyHieu} - {reading.KyGhi})";
        AcceptButton = btnOk;
        CancelButton = btnCancel;

        foreach (var tb in new[] { txtDienMoi, txtNuocMoi, txtGiaDien, txtGiaNuoc })
            tb.TextChanged += (_, _) => UpdateTotal();

        Load += async (_, _) => await RunDbAsync(LoadAsync);

        cboRoom.SelectedIndexChanged += async (_, _) => await RunDbAsync(async () =>
        {
            if (_reading != null || cboRoom.SelectedItem is not Room room) return;
            var last = await _utilityService!.GetLatestBeforeAsync(room.MaPhong, txtKy.Text);
            if (last != null)
            {
                txtDienCu.Text = last.ChiSoDienMoi.ToString("0.##");
                txtNuocCu.Text = last.ChiSoNuocMoi.ToString("0.##");
                UpdateTotal();
            }
        });

        btnOk.Click += BtnOk_Click;
        btnCancel.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };
    }

    /// <summary>Chạy một thao tác database của dialog, không cho chồng lên thao tác khác.</summary>
    private async Task RunDbAsync(Func<Task> action)
    {
        await _dbLock.WaitAsync();
        try { await action(); }
        finally { _dbLock.Release(); }
    }

    private async Task LoadAsync()
    {
        try
        {
            _rooms = await _roomService!.GetAllAsync();
            cboRoom.DisplayMember = nameof(Room.MaPhongKyHieu);
            cboRoom.DataSource = _rooms;

            if (_reading != null)
            {
                cboRoom.SelectedItem = _rooms.FirstOrDefault(r => r.MaPhong == _reading.MaPhong);
                txtKy.Text = _reading.KyGhi;
                txtDienCu.Text = _reading.ChiSoDienCu.ToString("0.##");
                txtDienMoi.Text = _reading.ChiSoDienMoi.ToString("0.##");
                txtGiaDien.Text = _reading.DonGiaDien.ToString();
                txtNuocCu.Text = _reading.ChiSoNuocCu.ToString("0.##");
                txtNuocMoi.Text = _reading.ChiSoNuocMoi.ToString("0.##");
                txtGiaNuoc.Text = _reading.DonGiaNuoc.ToString();
            }
            else
            {
                txtKy.Text = DateTime.Now.ToString("yyyy-MM");
                var latest = (await _utilityService!.GetAllAsync()).FirstOrDefault();
                txtGiaDien.Text = latest?.DonGiaDien.ToString() ?? "3500";
                txtGiaNuoc.Text = latest?.DonGiaNuoc.ToString() ?? "15000";
                txtDienCu.Text = "0";
                txtNuocCu.Text = "0";
            }
            UpdateTotal();
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi tải dữ liệu: " + ex.Message);
        }
    }

    private void UpdateTotal()
    {
        if (!decimal.TryParse(txtDienMoi.Text, out var dm)) { lblTien.Text = ""; return; }
        if (!decimal.TryParse(txtDienCu.Text, out var dc)) { lblTien.Text = ""; return; }
        if (!decimal.TryParse(txtGiaDien.Text, out var gd)) { lblTien.Text = ""; return; }
        if (!decimal.TryParse(txtNuocMoi.Text, out var nm)) { lblTien.Text = ""; return; }
        if (!decimal.TryParse(txtNuocCu.Text, out var nc)) { lblTien.Text = ""; return; }
        if (!decimal.TryParse(txtGiaNuoc.Text, out var gn)) { lblTien.Text = ""; return; }
        lblTien.Text = $"Tiền điện: {(dm - dc) * gd:N0} đ   |   Tiền nước: {(nm - nc) * gn:N0} đ";
    }

    private async void BtnOk_Click(object? sender, EventArgs e)
    {
        if (cboRoom.SelectedItem is not Room room)
        {
            Msg.Warn(this, "Vui lòng chọn phòng.", "Thiếu dữ liệu");
            return;
        }
        if (!System.Text.RegularExpressions.Regex.IsMatch(txtKy.Text.Trim(), @"^\d{4}-\d{2}$"))
        {
            Msg.Warn(this, "Kỳ phải theo định dạng yyyy-MM, ví dụ 2026-01.", "Sai định dạng");
            return;
        }
        if (!decimal.TryParse(txtDienCu.Text, out var dc) || !decimal.TryParse(txtDienMoi.Text, out var dm)
            || !decimal.TryParse(txtGiaDien.Text, out var gd) || !decimal.TryParse(txtNuocCu.Text, out var nc)
            || !decimal.TryParse(txtNuocMoi.Text, out var nm) || !decimal.TryParse(txtGiaNuoc.Text, out var gn))
        {
            Msg.Warn(this, "Các chỉ số và đơn giá phải là số.", "Sai định dạng");
            return;
        }
        if (dm < dc || nm < nc)
        {
            Msg.Warn(this, "Chỉ số mới phải lớn hơn hoặc bằng chỉ số cũ.", "Dữ liệu không hợp lệ");
            return;
        }

        var reading = new UtilityReading
        {
            MaGhiChiSo = _reading?.MaGhiChiSo ?? 0,
            MaPhong = room.MaPhong,
            KyGhi = txtKy.Text.Trim(),
            ChiSoDienCu = dc, ChiSoDienMoi = dm, DonGiaDien = gd,
            ChiSoNuocCu = nc, ChiSoNuocMoi = nm, DonGiaNuoc = gn,
        };

        try
        {
            var (ok, message) = _reading == null
                ? await _utilityService!.CreateAsync(reading)
                : await _utilityService!.UpdateAsync(reading);

            Msg.Info(this, message, ok ? "Thành công" : "Không thể lưu");
            if (ok) { DialogResult = DialogResult.OK; Close(); }
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi: " + ex.Message);
        }
    }
}
