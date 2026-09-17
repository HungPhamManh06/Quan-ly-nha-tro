using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.ViewModels;

namespace QuanLyNhaTro.Services;

public interface IInvoiceService
{
    Task<List<Invoice>> GetAllAsync(string? search = null, string? ky = null, InvoicePaymentStatus? status = null);
    Task<Invoice?> GetByIdAsync(int id);
    Task<Invoice?> GetDetailAsync(int id);
    Task<Invoice?> GetByRoomAndKyAsync(int roomId, string ky);
    Task<InvoiceCreateViewModel> BuildCreateViewModelAsync(int? roomId, string? ky, bool reload = false);
    Task<(bool Ok, string Message)> CreateAsync(InvoiceCreateViewModel vm);
    Task<(bool Ok, string Message)> DeleteAsync(int id);
}

public class InvoiceService : IInvoiceService
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<InvoiceService> _logger;

    public InvoiceService(ApplicationDbContext db, ILogger<InvoiceService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<Invoice>> GetAllAsync(string? search = null, string? ky = null, InvoicePaymentStatus? status = null)
    {
        var query = _db.Invoices.AsNoTracking().Include(i => i.Room).AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var kw = search.Trim();
            query = query.Where(i => i.MaHoaDonKyHieu.Contains(kw) || i.Room != null && i.Room.MaPhongKyHieu.Contains(kw));
        }
        if (!string.IsNullOrWhiteSpace(ky)) query = query.Where(i => i.KyHoaDon == ky);
        if (status.HasValue) query = query.Where(i => i.TrangThaiThanhToan == status.Value);
        return await query.OrderByDescending(i => i.KyHoaDon).ThenBy(i => i.Room!.MaPhongKyHieu).ToListAsync();
    }

    public Task<Invoice?> GetByIdAsync(int id) => _db.Invoices.FirstOrDefaultAsync(i => i.MaHoaDon == id);

    public Task<Invoice?> GetDetailAsync(int id) =>
        _db.Invoices.Include(i => i.Room).Include(i => i.Contract).Include(i => i.Payment).Include(i => i.Room!.Contracts).ThenInclude(c => c.Tenant)
            .FirstOrDefaultAsync(i => i.MaHoaDon == id);

    public Task<Invoice?> GetByRoomAndKyAsync(int roomId, string ky) =>
        _db.Invoices.FirstOrDefaultAsync(i => i.MaPhong == roomId && i.KyHoaDon == ky);

    public async Task<InvoiceCreateViewModel> BuildCreateViewModelAsync(int? roomId, string? ky, bool reload = false)
    {
        ky ??= DateTime.Now.ToString("yyyy-MM");
        var rooms = await _db.Rooms.AsNoTracking().Where(r => !r.DaXoa).OrderBy(r => r.MaPhongKyHieu).ToListAsync();
        var vm = new InvoiceCreateViewModel { KyHoaDon = ky, MaPhong = roomId ?? 0, Rooms = rooms };

        if (roomId.HasValue && roomId.Value > 0)
        {
            var room = rooms.FirstOrDefault(r => r.MaPhong == roomId.Value);
            if (room != null)
            {
                vm.TenPhong = room.TenPhong;

                // Hợp đồng còn hiệu lực trong kỳ để lấy giá thuê
                var (monthStart, monthEnd) = KyRange(ky);
                var contract = await _db.Contracts
                    .FirstOrDefaultAsync(c => c.MaPhong == room.MaPhong && c.TrangThai != ContractStatus.DaThanhLy && c.NgayBatDau <= monthEnd && c.NgayKetThuc >= monthStart);
                if (contract != null)
                {
                    vm.MaHopDong = contract.MaHopDong;
                    vm.TienPhong = contract.GiaThue;
                }

                // Điện nước của kỳ
                var reading = await _db.UtilityReadings.FirstOrDefaultAsync(u => u.MaPhong == room.MaPhong && u.KyGhi == ky);
                if (reading != null)
                {
                    vm.TienDien = reading.TienDien;
                    vm.TienNuoc = reading.TienNuoc;
                    vm.ChiTietDienNuoc = $"Điện: {reading.SoDienSuDung:N0} kW × {reading.DonGiaDien:N0} đ = {reading.TienDien:N0} đ | Nước: {reading.SoNuocSuDung:N0} m³ × {reading.DonGiaNuoc:N0} đ = {reading.TienNuoc:N0} đ";
                }

                // Dịch vụ của kỳ
                var usages = await _db.ServiceUsages.Include(su => su.Service).Where(su => su.MaPhong == room.MaPhong && su.KySuDung == ky).ToListAsync();
                vm.TienDichVu = usages.Sum(su => su.ThanhTien);
                if (usages.Count > 0)
                {
                    vm.ChiTietDichVu = string.Join("; ", usages.Select(su => $"{su.Service!.TenDichVu}: {su.SoLuong:N0} {su.Service.DonViTinh} × {su.DonGiaApDung:N0} đ = {su.ThanhTien:N0} đ"));
                }
            }
        }
        return vm;
    }

    public static (DateTime start, DateTime end) KyRange(string ky)
    {
        var parts = ky.Split('-');
        var year = int.Parse(parts[0]);
        var month = int.Parse(parts[1]);
        var start = new DateTime(year, month, 1);
        return (start, start.AddMonths(1).AddDays(-1));
    }

    public async Task<(bool Ok, string Message)> CreateAsync(InvoiceCreateViewModel vm)
    {
        // Một phòng chỉ có một hóa đơn cho một kỳ
        if (await _db.Invoices.AnyAsync(i => i.MaPhong == vm.MaPhong && i.KyHoaDon == vm.KyHoaDon))
            return (false, $"Phòng này đã có hóa đơn cho kỳ {vm.KyHoaDon}. Không thể lập hóa đơn trùng.");

        var room = await _db.Rooms.FindAsync(vm.MaPhong);
        if (room == null) return (false, "Không tìm thấy phòng.");

        var invoice = new Invoice
        {
            MaPhong = vm.MaPhong,
            MaHopDong = vm.MaHopDong,
            KyHoaDon = vm.KyHoaDon,
            NgayLap = DateTime.Now,
            TienPhong = vm.TienPhong,
            TienDien = vm.TienDien,
            TienNuoc = vm.TienNuoc,
            TienDichVu = vm.TienDichVu,
            HanThanhToan = vm.HanThanhToan,
            GhiChu = vm.GhiChu,
            TrangThaiThanhToan = InvoicePaymentStatus.ChuaThanhToan
        };

        // Tổng tiền = tiền phòng + điện + nước + dịch vụ (tính lại phía server, không tin client)
        invoice.TongTien = invoice.TienPhong + invoice.TienDien + invoice.TienNuoc + invoice.TienDichVu;
        invoice.MaHoaDonKyHieu = await GenerateMaHoaDonAsync(vm.KyHoaDon);

        _db.Invoices.Add(invoice);
        await _db.SaveChangesAsync();
        _logger.LogInformation("Lập hóa đơn {MaHoaDon} phòng {Room} kỳ {Ky}", invoice.MaHoaDonKyHieu, room.MaPhongKyHieu, invoice.KyHoaDon);
        return (true, $"Lập hóa đơn {invoice.MaHoaDonKyHieu} thành công. Tổng tiền: {invoice.TongTien:N0} đ.");
    }

    private async Task<string> GenerateMaHoaDonAsync(string ky)
    {
        var kyCompact = ky.Replace("-", "");
        var count = await _db.Invoices.CountAsync(i => i.KyHoaDon == ky);
        string code;
        do
        {
            code = $"HD{kyCompact}-{count + 1:000}";
            count++;
        } while (await _db.Invoices.AnyAsync(i => i.MaHoaDonKyHieu == code));
        return code;
    }

    public async Task<(bool Ok, string Message)> DeleteAsync(int id)
    {
        var invoice = await _db.Invoices.FindAsync(id);
        if (invoice == null) return (false, "Không tìm thấy hóa đơn.");
        if (invoice.TrangThaiThanhToan == InvoicePaymentStatus.DaThanhToan)
            return (false, "Không thể xóa hóa đơn đã thanh toán (bảo vệ dữ liệu tài chính).");

        _db.Invoices.Remove(invoice);
        await _db.SaveChangesAsync();
        return (true, $"Đã xóa hóa đơn kỳ {invoice.KyHoaDon}.");
    }
}
