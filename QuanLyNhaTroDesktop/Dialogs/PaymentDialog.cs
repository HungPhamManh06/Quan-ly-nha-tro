using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTro.ViewModels;
using QuanLyNhaTroDesktop.Helpers;

namespace QuanLyNhaTroDesktop.Dialogs;

/// <summary>
/// Dialog thanh toán hóa đơn: mỗi hóa đơn chỉ thanh toán MỘT lần.
/// Trả thiếu → từ chối; trả thừa → ghi nhận đúng tổng, hoàn lại phần thừa (xử lý trong PaymentService).
///
/// GIAO DIỆN: PaymentDialog.Designer.cs — mở bằng Visual Studio Designer để chỉnh sửa.
/// File này chỉ chứa phần XỬ LÝ.
/// </summary>
public partial class PaymentDialog : Form
{
    private readonly IPaymentService? _paymentService;
    private readonly Invoice? _invoice;

    /// <summary>Dùng cho Visual Studio Designer (không truy cập database).</summary>
    public PaymentDialog()
    {
        InitializeComponent();
        Dpi.ScaleForm(this);
    }

    public PaymentDialog(IPaymentService paymentService, Invoice invoice) : this()
    {
        _paymentService = paymentService;
        _invoice = invoice;

        lblInfo.Text = $"Hóa đơn: {invoice.MaHoaDonKyHieu}\n" +
                       $"Phòng: {invoice.Room?.MaPhongKyHieu} — Kỳ: {invoice.KyHoaDon}\n" +
                       $"TỔNG TIỀN: {Ui.Money(invoice.TongTien)}";

        txtSoTien.Text = invoice.TongTien.ToString();   // gợi ý trả đúng tổng
        cboPhuongThuc.SelectedIndex = 0;

        AcceptButton = btnOk;
        CancelButton = btnCancel;

        btnOk.Click += BtnOk_Click;
        btnCancel.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };
    }

    private async void BtnOk_Click(object? sender, EventArgs e)
    {
        if (!decimal.TryParse(txtSoTien.Text, out var soTien) || soTien <= 0)
        {
            Msg.Warn(this, "Số tiền phải là số lớn hơn 0.", "Sai định dạng");
            return;
        }

        var vm = new PaymentViewModel
        {
            MaHoaDon = _invoice!.MaHoaDon,
            MaHoaDonKyHieu = _invoice.MaHoaDonKyHieu,
            TenPhong = _invoice.Room?.MaPhongKyHieu ?? "",
            KyHoaDon = _invoice.KyHoaDon,
            TongTien = _invoice.TongTien,
            SoTien = soTien,
            PhuongThuc = cboPhuongThuc.SelectedIndex == 1 ? PaymentMethod.ChuyenKhoan : PaymentMethod.TienMat,
            GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim(),
        };

        try
        {
            var (ok, message) = await _paymentService!.CreateAsync(vm);
            Msg.Info(this, message, ok ? "Thành công" : "Không thể thanh toán");
            if (ok) { DialogResult = DialogResult.OK; Close(); }
        }
        catch (Exception ex)
        {
            Msg.Error(this, "Lỗi: " + ex.Message);
        }
    }
}
