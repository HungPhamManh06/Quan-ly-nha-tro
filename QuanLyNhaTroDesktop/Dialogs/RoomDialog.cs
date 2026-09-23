using Microsoft.Extensions.DependencyInjection;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop.Dialogs;

/// <summary>
/// Dialog thêm/sửa phòng (truyền room = null để thêm mới).
/// GIAO DIỆN: RoomDialog.Designer.cs — mở bằng Visual Studio Designer để chỉnh sửa.
/// </summary>
public partial class RoomDialog : Form
{
    private readonly Room? _room;

    /// <summary>
    /// Constructor KHÔNG THAM SỐ: bắt buộc phải có để Visual Studio Designer mở được form này
    /// (Designer tạo form trong tiến trình riêng, không có database).
    /// </summary>
    public RoomDialog()
    {
        InitializeComponent();
        Dpi.ScaleForm(this);
    }

    public RoomDialog(Room? room) : this()
    {
        _room = room;

        Text = room == null ? "Thêm phòng mới" : $"Sửa phòng {room.MaPhongKyHieu}";
        AcceptButton = btnOk;
        CancelButton = btnCancel;

        if (_room != null)
        {
            txtMa.Text = _room.MaPhongKyHieu;
            txtTen.Text = _room.TenPhong;
            txtLoai.Text = _room.LoaiPhong ?? "";
            txtDienTich.Text = _room.DienTich?.ToString() ?? "";
            txtGia.Text = _room.GiaPhong.ToString();
            txtGhiChu.Text = _room.GhiChu ?? "";
        }
        else
        {
            Load += async (_, _) =>
            {
                var roomService = Program.Services.GetRequiredService<IRoomService>();
                txtMa.Text = await roomService.GenerateMaPhongAsync();
            };
        }

        btnOk.Click += BtnOk_Click;
    }

    /// <summary>Tô màu nút chính (giữ lại cho các dialog đang chuyển dần sang Designer).</summary>
    internal static void StylePrimary(Button b) => Ui.StylePrimary(b);

    private async void BtnOk_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtMa.Text) || string.IsNullOrWhiteSpace(txtTen.Text))
        {
            Msg.Warn(this, "Mã phòng và tên phòng không được để trống.", "Thiếu dữ liệu");
            return;
        }
        if (!decimal.TryParse(txtGia.Text, out var gia) || gia < 0)
        {
            Msg.Warn(this, "Giá phòng phải là số lớn hơn hoặc bằng 0.", "Sai định dạng");
            return;
        }
        decimal? dienTich = null;
        if (!string.IsNullOrWhiteSpace(txtDienTich.Text))
        {
            if (!decimal.TryParse(txtDienTich.Text, out var dt) || dt <= 0)
            {
                Msg.Warn(this, "Diện tích phải là số lớn hơn 0 (hoặc bỏ trống).", "Sai định dạng");
                return;
            }
            dienTich = dt;
        }

        var room = new Room
        {
            MaPhong = _room?.MaPhong ?? 0,
            MaPhongKyHieu = txtMa.Text.Trim(),
            TenPhong = txtTen.Text.Trim(),
            LoaiPhong = string.IsNullOrWhiteSpace(txtLoai.Text) ? null : txtLoai.Text.Trim(),
            DienTich = dienTich,
            GiaPhong = gia,
            GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim(),
        };

        try
        {
            var roomService = Program.Services.GetRequiredService<IRoomService>();
            var result = _room == null
                ? await roomService.CreateAsync(room)
                : await roomService.UpdateAsync(room);

            Msg.Result(this, result);
            if (result.Ok) { DialogResult = DialogResult.OK; Close(); }
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi: " + ex.Message);
        }
    }
}
