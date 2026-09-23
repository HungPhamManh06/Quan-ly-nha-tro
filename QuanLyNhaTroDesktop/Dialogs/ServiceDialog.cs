using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop.Dialogs;

/// <summary>
/// Dialog thêm/sửa dịch vụ (Internet, gửi xe, vệ sinh...) — service = null là thêm mới.
///
/// GIAO DIỆN: ServiceDialog.Designer.cs — mở bằng Visual Studio Designer để chỉnh sửa.
/// File này chỉ chứa phần XỬ LÝ.
/// </summary>
public partial class ServiceDialog : Form
{
    private readonly Service? _service;
    private readonly IServiceService? _serviceService;

    /// <summary>Dùng cho Visual Studio Designer (không truy cập database).</summary>
    public ServiceDialog()
    {
        InitializeComponent();
        Dpi.ScaleForm(this);
    }

    public ServiceDialog(Service? service, IServiceService serviceService) : this()
    {
        _service = service;
        _serviceService = serviceService;

        Text = service == null ? "Thêm dịch vụ mới" : $"Sửa dịch vụ: {service.TenDichVu}";
        AcceptButton = btnOk;
        CancelButton = btnCancel;

        if (_service != null)
        {
            txtTen.Text = _service.TenDichVu;
            txtDonVi.Text = _service.DonViTinh;
            txtDonGia.Text = _service.DonGia.ToString();
            chkApDung.Checked = _service.TrangThai;
        }

        btnOk.Click += BtnOk_Click;
        btnCancel.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };
    }

    private async void BtnOk_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtTen.Text) || string.IsNullOrWhiteSpace(txtDonVi.Text))
        {
            Msg.Warn(this, "Tên dịch vụ và đơn vị tính không được để trống.", "Thiếu dữ liệu");
            return;
        }
        if (!decimal.TryParse(txtDonGia.Text, out var gia) || gia < 0)
        {
            Msg.Warn(this, "Đơn giá phải là số lớn hơn hoặc bằng 0.", "Sai định dạng");
            return;
        }

        var service = new Service
        {
            MaDichVu = _service?.MaDichVu ?? 0,
            TenDichVu = txtTen.Text.Trim(),
            DonViTinh = txtDonVi.Text.Trim(),
            DonGia = gia,
            TrangThai = chkApDung.Checked,
        };

        try
        {
            var (ok, message) = _service == null
                ? await _serviceService!.CreateServiceAsync(service)
                : await _serviceService!.UpdateServiceAsync(service);

            Msg.Info(this, message, ok ? "Thành công" : "Không thể lưu");
            if (ok) { DialogResult = DialogResult.OK; Close(); }
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi: " + ex.Message);
        }
    }
}
