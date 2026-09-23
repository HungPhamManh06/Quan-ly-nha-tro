using Microsoft.Extensions.DependencyInjection;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop.Dialogs;

/// <summary>
/// Dialog thêm/sửa người thuê (truyền tenant = null để thêm mới).
/// GIAO DIỆN: TenantDialog.Designer.cs — mở bằng Visual Studio Designer để chỉnh sửa.
/// </summary>
public partial class TenantDialog : Form
{
    private readonly Tenant? _tenant;

    /// <summary>
    /// Constructor KHÔNG THAM SỐ: bắt buộc phải có để Visual Studio Designer mở được form này
    /// (Designer tạo form trong tiến trình riêng, không có database).
    /// </summary>
    public TenantDialog()
    {
        InitializeComponent();
        Dpi.ScaleForm(this);
    }

    public TenantDialog(Tenant? tenant) : this()
    {
        _tenant = tenant;

        Text = tenant == null ? "Thêm người thuê mới" : $"Sửa người thuê {tenant.HoTen}";
        AcceptButton = btnOk;
        CancelButton = btnCancel;

        if (_tenant != null)
        {
            txtHoTen.Text = _tenant.HoTen;
            txtCCCD.Text = _tenant.CCCD;
            txtDienThoai.Text = _tenant.SoDienThoai ?? "";
            txtQueQuan.Text = _tenant.QueQuan ?? "";
            txtGhiChu.Text = _tenant.GhiChu ?? "";
        }

        btnOk.Click += BtnOk_Click;
    }

    private async void BtnOk_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtCCCD.Text))
        {
            Msg.Warn(this, "Họ tên và CCCD không được để trống.", "Thiếu dữ liệu");
            return;
        }
        if (!System.Text.RegularExpressions.Regex.IsMatch(txtCCCD.Text.Trim(), @"^\d{9,12}$"))
        {
            Msg.Warn(this, "CCCD phải gồm 9–12 chữ số.", "Sai định dạng");
            return;
        }

        var tenant = new Tenant
        {
            MaNguoiThue = _tenant?.MaNguoiThue ?? 0,
            HoTen = txtHoTen.Text.Trim(),
            CCCD = txtCCCD.Text.Trim(),
            SoDienThoai = string.IsNullOrWhiteSpace(txtDienThoai.Text) ? null : txtDienThoai.Text.Trim(),
            QueQuan = string.IsNullOrWhiteSpace(txtQueQuan.Text) ? null : txtQueQuan.Text.Trim(),
            GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim(),
        };

        try
        {
            var tenantService = Program.Services.GetRequiredService<ITenantService>();
            var result = _tenant == null
                ? await tenantService.CreateAsync(tenant)
                : await tenantService.UpdateAsync(tenant);

            Msg.Result(this, result);
            if (result.Ok) { DialogResult = DialogResult.OK; Close(); }
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi: " + ex.Message);
        }
    }
}
