using Microsoft.Extensions.DependencyInjection;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop.Dialogs;

/// <summary>
/// Dialog ghi nhận / cập nhật sử dụng dịch vụ (tương ứng Services/CreateUsage và EditUsage
/// của bản web). Đơn giá áp dụng được lưu lại tại thời điểm ghi theo đúng nghiệp vụ.
///
/// GIAO DIỆN: ServiceUsageDialog.Designer.cs — mở bằng Visual Studio Designer để chỉnh sửa.
/// File này chỉ chứa phần XỬ LÝ.
/// </summary>
public partial class ServiceUsageDialog : Form
{
    private readonly IServiceService? _serviceService;
    private readonly ServiceUsage? _existing;

    private List<Room> _rooms = new();
    private List<Service> _services = new();

    /// <summary>Dùng cho Visual Studio Designer (không truy cập database).</summary>
    public ServiceUsageDialog()
    {
        InitializeComponent();
        Dpi.ScaleForm(this);
    }

    public ServiceUsageDialog(ServiceUsage? existing, IServiceService serviceService,
        int? presetRoomId = null, string? presetKy = null) : this()
    {
        _existing = existing;
        _serviceService = serviceService;

        Text = existing == null ? "Ghi nhận sử dụng dịch vụ" : "Sửa sử dụng dịch vụ";
        AcceptButton = null;
        CancelButton = btnCancel;

        txtSoLuong.TextChanged += (_, _) => UpdateTotal();
        txtDonGia.TextChanged += (_, _) => UpdateTotal();
        cboService.SelectedIndexChanged += (_, _) =>
        {
            if (cboService.SelectedItem is Service s && _existing == null && string.IsNullOrWhiteSpace(txtDonGia.Text))
                txtDonGia.Text = s.DonGia.ToString();
            UpdateTotal();
        };

        Load += async (_, _) => await LoadAsync(presetRoomId, presetKy);
        btnCancel.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };
        btnOk.Click += async (_, _) => await SaveAsync();
    }

    private async Task LoadAsync(int? presetRoomId, string? presetKy)
    {
        try
        {
            _rooms = await Program.Services.GetRequiredService<IRoomService>().GetAllAsync();
            _services = (await _serviceService!.GetServicesAsync()).Where(s => s.TrangThai).ToList();

            cboRoom.DisplayMember = nameof(Room.MaPhongKyHieu);
            cboRoom.DataSource = _rooms;
            cboService.DisplayMember = nameof(Service.TenDichVu);
            cboService.DataSource = _services;

            if (_existing == null)
            {
                txtKy.Text = presetKy ?? DateTime.Now.ToString("yyyy-MM");
                if (presetRoomId.HasValue) cboRoom.SelectedItem = _rooms.FirstOrDefault(r => r.MaPhong == presetRoomId.Value);
                if (_services.Count == 1) txtDonGia.Text = _services[0].DonGia.ToString();
            }
            else
            {
                cboRoom.SelectedItem = _rooms.FirstOrDefault(r => r.MaPhong == _existing.MaPhong);
                cboService.SelectedItem = _services.FirstOrDefault(s => s.MaDichVu == _existing.MaDichVu);
                cboRoom.Enabled = cboService.Enabled = false;   // nghiệp vụ: chỉ sửa số lượng/đơn giá
                txtKy.Text = _existing.KySuDung;
                txtKy.Enabled = false;
                txtSoLuong.Text = _existing.SoLuong.ToString("0.##");
                txtDonGia.Text = _existing.DonGiaApDung.ToString();
                txtGhiChu.Text = _existing.GhiChu ?? "";
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
        var soLuong = decimal.TryParse(txtSoLuong.Text, out var sl) ? sl : 0;
        var donGia = decimal.TryParse(txtDonGia.Text, out var dg) ? dg : 0;
        lblThanhTien.Text = $"THÀNH TIỀN: {Ui.Money(soLuong * donGia)}";
    }

    private async Task SaveAsync()
    {
        if (cboRoom.SelectedItem is not Room room || cboService.SelectedItem is not Service service)
        {
            Msg.Warn(this, "Vui lòng chọn phòng và dịch vụ.");
            return;
        }
        if (!System.Text.RegularExpressions.Regex.IsMatch(txtKy.Text.Trim(), @"^\d{4}-\d{2}$") &&
            !txtKy.Text.Trim().StartsWith("TRAPHONG-", StringComparison.OrdinalIgnoreCase))
        {
            Msg.Warn(this, "Kỳ phải theo định dạng yyyy-MM, ví dụ 2026-01.");
            return;
        }
        if (!decimal.TryParse(txtSoLuong.Text, out var soLuong) || soLuong <= 0)
        {
            Msg.Warn(this, "Số lượng phải là số lớn hơn 0.");
            return;
        }
        if (!decimal.TryParse(txtDonGia.Text, out var donGia) || donGia < 0)
        {
            Msg.Warn(this, "Đơn giá áp dụng phải là số lớn hơn hoặc bằng 0.");
            return;
        }

        var usage = new ServiceUsage
        {
            MaSuDung = _existing?.MaSuDung ?? 0,
            MaPhong = room.MaPhong,
            MaDichVu = service.MaDichVu,
            KySuDung = txtKy.Text.Trim(),
            SoLuong = soLuong,
            DonGiaApDung = donGia,
            GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim(),
        };

        try
        {
            var result = _existing == null
                ? await _serviceService!.CreateUsageAsync(usage)
                : await _serviceService!.UpdateUsageAsync(usage);

            Msg.Result(this, result);
            if (result.Ok) { DialogResult = DialogResult.OK; Close(); }
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi: " + ex.Message);
        }
    }
}
