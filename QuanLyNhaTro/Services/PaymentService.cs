using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.ViewModels;

namespace QuanLyNhaTro.Services;

public interface IPaymentService
{
    Task<List<Payment>> GetAllAsync();
    Task<Payment?> GetDetailAsync(int id);
    Task<PaymentViewModel> BuildPaymentViewModelAsync(int invoiceId);
    Task<(bool Ok, string Message)> CreateAsync(PaymentViewModel vm);
}

public class PaymentService : IPaymentService
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(ApplicationDbContext db, ILogger<PaymentService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public Task<List<Payment>> GetAllAsync() =>
        _db.Payments.AsNoTracking().Include(p => p.Invoice).ThenInclude(i => i!.Room)
            .OrderByDescending(p => p.NgayThanhToan).ToListAsync();

    public Task<Payment?> GetDetailAsync(int id) =>
        _db.Payments.Include(p => p.Invoice).ThenInclude(i => i!.Room).FirstOrDefaultAsync(p => p.MaThanhToan == id);

    public async Task<PaymentViewModel> BuildPaymentViewModelAsync(int invoiceId)
    {
        var invoice = await _db.Invoices.Include(i => i.Room).FirstOrDefaultAsync(i => i.MaHoaDon == invoiceId)
            ?? throw new InvalidOperationException("Không tìm thấy hóa đơn.");
        return new PaymentViewModel
        {
            MaHoaDon = invoice.MaHoaDon,
            MaHoaDonKyHieu = invoice.MaHoaDonKyHieu,
            TenPhong = invoice.Room?.MaPhongKyHieu ?? "",
            KyHoaDon = invoice.KyHoaDon,
            TongTien = invoice.TongTien,
            TrangThaiThanhToan = invoice.TrangThaiThanhToan
        };
    }

    public async Task<(bool Ok, string Message)> CreateAsync(PaymentViewModel vm)
    {
        var invoice = await _db.Invoices.Include(i => i.Room).FirstOrDefaultAsync(i => i.MaHoaDon == vm.MaHoaDon);
        if (invoice == null) return (false, "Không tìm thấy hóa đơn.");

        // Một hóa đơn chỉ được thanh toán MỘT lần duy nhất
        if (invoice.TrangThaiThanhToan == InvoicePaymentStatus.DaThanhToan || await _db.Payments.AnyAsync(p => p.MaHoaDon == vm.MaHoaDon))
            return (false, "Hóa đơn này đã được thanh toán. Mỗi hóa đơn chỉ được thanh toán một lần duy nhất.");

        var tongTien = invoice.TongTien;
        var soTien = vm.SoTien;

        // Thanh toán thiếu → không chấp nhận, không tạo payment
        if (soTien < tongTien)
            return (false, $"Không thể thanh toán vì số tiền chưa đủ. Cần {tongTien:N0} đ nhưng chỉ nhận {soTien:N0} đ (hóa đơn vẫn ở trạng thái Chưa thanh toán).");

        // Thanh toán thừa → chỉ ghi nhận phần đúng bằng tổng tiền, phần thừa hoàn lại
        var ghiNhan = Math.Min(soTien, tongTien);
        var hoanLai = soTien - ghiNhan;

        // Transaction: tạo Payment + cập nhật trạng thái hóa đơn phải nguyên vẹn
        await using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            var payment = new Payment
            {
                MaHoaDon = invoice.MaHoaDon,
                SoTien = ghiNhan,
                NgayThanhToan = vm.NgayThanhToan ?? DateTime.Now,
                PhuongThuc = vm.PhuongThuc,
                GhiChu = hoanLai > 0 ? $"{(vm.GhiChu == null ? "" : vm.GhiChu + " ")}(Khách trả {soTien:N0} đ, hoàn lại {hoanLai:N0} đ)".Trim() : vm.GhiChu
            };
            _db.Payments.Add(payment);

            invoice.TrangThaiThanhToan = InvoicePaymentStatus.DaThanhToan;
            invoice.NgayThanhToan = payment.NgayThanhToan;

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            _logger.LogInformation("Ghi nhận thanh toán hóa đơn {MaHoaDon}, số tiền {SoTien}", invoice.MaHoaDonKyHieu, ghiNhan);
            var msg = $"Thanh toán thành công {ghiNhan:N0} đ cho hóa đơn {invoice.MaHoaDonKyHieu}.";
            if (hoanLai > 0) msg += $" Hoàn lại khách {hoanLai:N0} đ.";
            return (true, msg);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Lỗi khi ghi nhận thanh toán hóa đơn {MaHoaDon}", vm.MaHoaDon);
            return (false, "Đã xảy ra lỗi khi lưu thanh toán. Thao tác đã được hoàn nguyên.");
        }
    }
}
