using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop.Dialogs;

/// <summary>
/// Dialog gia hạn hợp đồng (tương ứng Contracts/Renew của bản web).
/// Nghiệp vụ kiểm tra chồng lấn thời gian và giá trị hợp lệ nằm trong ContractService.RenewAsync.
///
/// GIAO DIỆN: RenewContractDialog.Designer.cs — mở bằng Visual Studio Designer để chỉnh sửa.
/// File này chỉ chứa phần XỬ LÝ.
/// </summary>
public partial class RenewContractDialog : Form
{
    private readonly IContractService? _contractService;
    private readonly int _contractId;

    /// <summary>Dùng cho Visual Studio Designer (không truy cập database).</summary>
    public RenewContractDialog()
    {
        InitializeComponent();
        Dpi.ScaleForm(this);
    }

    public RenewContractDialog(Contract contract, IContractService contractService) : this()
    {
        _contractService = contractService;
        _contractId = contract.MaHopDong;

        Text = $"Gia hạn hợp đồng {contract.MaHopDongKyHieu}";
        lblTitle.Text = $"GIA HẠN HỢP ĐỒNG {contract.MaHopDongKyHieu}";
        lblContractInfo.Text =
            $"Phòng: {contract.Room?.MaPhongKyHieu} — Người thuê: {contract.Tenant?.HoTen}\n" +
            $"Thời hạn hiện tại: {contract.NgayBatDau:dd/MM/yyyy} → {contract.NgayKetThuc:dd/MM/yyyy}\n" +
            $"Giá thuê hiện tại: {Ui.Money(contract.GiaThue)}/tháng — Tiền cọc: {Ui.Money(contract.TienCoc)}";
        lblGoiY.Text = "Ghi chú: hợp đồng chỉ được gia hạn dài hơn ngày kết thúc hiện tại.";

        AcceptButton = btnOk;
        CancelButton = btnCancel;

        dtpNgayKetThucMoi.MinDate = contract.NgayKetThuc.AddDays(1);
        dtpNgayKetThucMoi.Value = contract.NgayKetThuc.AddYears(1);
        txtGiaThueMoi.Text = contract.GiaThue.ToString();
        txtTienCocMoi.Text = contract.TienCoc.ToString();

        btnCancel.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };
        btnOk.Click += async (_, _) => await SaveAsync();
    }

    private async Task SaveAsync()
    {
        if (!decimal.TryParse(txtGiaThueMoi.Text, out var giaThueMoi) || giaThueMoi <= 0)
        {
            Msg.Warn(this, "Giá thuê mới phải là số lớn hơn 0.");
            return;
        }
        if (!decimal.TryParse(txtTienCocMoi.Text, out var tienCocMoi) || tienCocMoi < 0)
        {
            Msg.Warn(this, "Tiền cọc mới phải là số lớn hơn hoặc bằng 0.");
            return;
        }

        try
        {
            var result = await _contractService!.RenewAsync(_contractId,
                dtpNgayKetThucMoi.Value.Date, giaThueMoi, tienCocMoi,
                string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim());

            Msg.Result(this, result);
            if (result.Ok) { DialogResult = DialogResult.OK; Close(); }
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi: " + ex.Message);
        }
    }
}
