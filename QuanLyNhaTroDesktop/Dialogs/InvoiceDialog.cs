using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTro.ViewModels;
using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop.Dialogs;

/// <summary>
/// Dialog lập hóa đơn: chọn phòng + kỳ → tự lấy tiền phòng (từ hợp đồng),
/// tiền điện nước (từ chỉ số), tiền dịch vụ (từ sử dụng) như bản web.
///
/// GIAO DIỆN: InvoiceDialog.Designer.cs — mở bằng Visual Studio Designer để chỉnh sửa.
/// File này chỉ chứa phần XỬ LÝ.
/// </summary>
public partial class InvoiceDialog : Form
{
    private readonly IInvoiceService? _invoiceService;

    private List<Room> _rooms = new();

    /// <summary>Dùng cho Visual Studio Designer (không truy cập database).</summary>
    public InvoiceDialog()
    {
        InitializeComponent();
        Dpi.ScaleForm(this);
    }

    public InvoiceDialog(Invoice? existing, IInvoiceService invoiceService) : this()
    {
        _invoiceService = invoiceService;

        AcceptButton = btnOk;
        CancelButton = btnCancel;

        foreach (var tb in new[] { txtTienPhong, txtTienDien, txtTienNuoc, txtTienDichVu })
            tb.TextChanged += (_, _) => UpdateTotal();

        Load += async (_, _) => await RunDbAsync(async () =>
        {
            var vm = await _invoiceService.BuildCreateViewModelAsync(null, txtKy.Text);
            _rooms = vm.Rooms;
            cboRoom.DisplayMember = nameof(Room.MaPhongKyHieu);
            cboRoom.DataSource = _rooms;
        });

        cboRoom.SelectedIndexChanged += async (_, _) => await RunDbAsync(ReloadAmountsAsync);
        txtKy.Leave += async (_, _) => await RunDbAsync(ReloadAmountsAsync);

        btnOk.Click += BtnOk_Click;
        btnCancel.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };
    }

    // Xếp hàng các truy vấn của dialog: gán cboRoom.DataSource trong lúc nạp làm sự kiện
    // đổi phòng chạy NGAY, hai truy vấn cùng lúc trên một DbContext sẽ báo lỗi
    // "A second operation was started on this context instance" (chỉ lộ ra trên SQL Server).
    private readonly SemaphoreSlim _dbLock = new(1, 1);

    private async Task RunDbAsync(Func<Task> action)
    {
        await _dbLock.WaitAsync();
        try { await action(); }
        finally { _dbLock.Release(); }
    }

    private async Task ReloadAmountsAsync()
    {
        if (cboRoom.SelectedItem is not Room room) return;
        var vm = await _invoiceService!.BuildCreateViewModelAsync(room.MaPhong, txtKy.Text.Trim());
        txtTienPhong.Text = vm.TienPhong.ToString();
        txtTienDien.Text = vm.TienDien.ToString();
        txtTienNuoc.Text = vm.TienNuoc.ToString();
        txtTienDichVu.Text = vm.TienDichVu.ToString();
        lblChiTiet.Text = (vm.ChiTietDienNuoc ?? "") + "\n" + (vm.ChiTietDichVu ?? "");
        UpdateTotal();
    }

    private void UpdateTotal()
    {
        decimal p = decimal.TryParse(txtTienPhong.Text, out var v1) ? v1 : 0;
        decimal d = decimal.TryParse(txtTienDien.Text, out var v2) ? v2 : 0;
        decimal n = decimal.TryParse(txtTienNuoc.Text, out var v3) ? v3 : 0;
        decimal s = decimal.TryParse(txtTienDichVu.Text, out var v4) ? v4 : 0;
        lblTong.Text = $"TỔNG TIỀN: {(p + d + n + s):N0} đ";
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

        var vm = new InvoiceCreateViewModel
        {
            MaPhong = room.MaPhong,
            KyHoaDon = txtKy.Text.Trim(),
            TienPhong = decimal.TryParse(txtTienPhong.Text, out var v1) ? v1 : 0,
            TienDien = decimal.TryParse(txtTienDien.Text, out var v2) ? v2 : 0,
            TienNuoc = decimal.TryParse(txtTienNuoc.Text, out var v3) ? v3 : 0,
            TienDichVu = decimal.TryParse(txtTienDichVu.Text, out var v4) ? v4 : 0,
            HanThanhToan = dtpHan.Value.Date,
            GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim(),
        };

        try
        {
            var (ok, message) = await _invoiceService!.CreateAsync(vm);
            Msg.Info(this, message, ok ? "Thành công" : "Không thể lập");
            if (ok) { DialogResult = DialogResult.OK; Close(); }
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi: " + ex.Message);
        }
    }
}
