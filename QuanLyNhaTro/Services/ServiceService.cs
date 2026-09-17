using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Services;

public interface IServiceService
{
    Task<List<Service>> GetServicesAsync(string? search = null);
    Task<Service?> GetServiceByIdAsync(int id);
    Task<(bool Ok, string Message)> CreateServiceAsync(Service service);
    Task<(bool Ok, string Message)> UpdateServiceAsync(Service service);
    Task<(bool Ok, string Message)> DeleteServiceAsync(int id);

    Task<List<ServiceUsage>> GetUsagesAsync(string? ky = null, int? roomId = null);
    Task<ServiceUsage?> GetUsageByIdAsync(int id);
    Task<string> GenerateKySuDungAsync();
    Task<(bool Ok, string Message)> CreateUsageAsync(ServiceUsage usage);
    Task<(bool Ok, string Message)> UpdateUsageAsync(ServiceUsage usage);
    Task<(bool Ok, string Message)> DeleteUsageAsync(int id);
}

public class ServiceService : IServiceService
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<ServiceService> _logger;

    public ServiceService(ApplicationDbContext db, ILogger<ServiceService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<Service>> GetServicesAsync(string? search = null)
    {
        var query = _db.Services.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(s => s.TenDichVu.Contains(search.Trim()));
        return await query.OrderBy(s => s.TenDichVu).ToListAsync();
    }

    public Task<Service?> GetServiceByIdAsync(int id) => _db.Services.FirstOrDefaultAsync(s => s.MaDichVu == id);

    public async Task<(bool Ok, string Message)> CreateServiceAsync(Service service)
    {
        if (await _db.Services.AnyAsync(s => s.TenDichVu == service.TenDichVu))
            return (false, $"Dịch vụ {service.TenDichVu} đã tồn tại.");
        _db.Services.Add(service);
        await _db.SaveChangesAsync();
        return (true, $"Thêm dịch vụ {service.TenDichVu} thành công.");
    }

    public async Task<(bool Ok, string Message)> UpdateServiceAsync(Service service)
    {
        var existed = await _db.Services.FindAsync(service.MaDichVu);
        if (existed == null) return (false, "Không tìm thấy dịch vụ.");
        if (await _db.Services.AnyAsync(s => s.TenDichVu == service.TenDichVu && s.MaDichVu != service.MaDichVu))
            return (false, $"Dịch vụ {service.TenDichVu} đã tồn tại.");

        existed.TenDichVu = service.TenDichVu;
        existed.DonViTinh = service.DonViTinh;
        existed.DonGia = service.DonGia;
        existed.TrangThai = service.TrangThai;
        await _db.SaveChangesAsync();
        return (true, $"Cập nhật dịch vụ {service.TenDichVu} thành công.");
    }

    public async Task<(bool Ok, string Message)> DeleteServiceAsync(int id)
    {
        var service = await _db.Services.FindAsync(id);
        if (service == null) return (false, "Không tìm thấy dịch vụ.");

        // Đã phát sinh lịch sử sử dụng thì không xóa cứng, chỉ ngưng hoạt động
        if (await _db.ServiceUsages.AnyAsync(su => su.MaDichVu == id))
        {
            service.TrangThai = false;
            await _db.SaveChangesAsync();
            return (true, $"Dịch vụ {service.TenDichVu} đã phát sinh lịch sử nên được ngưng hoạt động thay vì xóa.");
        }

        _db.Services.Remove(service);
        await _db.SaveChangesAsync();
        return (true, $"Đã xóa dịch vụ {service.TenDichVu}.");
    }

    public async Task<List<ServiceUsage>> GetUsagesAsync(string? ky = null, int? roomId = null)
    {
        var query = _db.ServiceUsages.AsNoTracking()
            .Include(su => su.Room)
            .Include(su => su.Service)
            .AsQueryable();
        if (!string.IsNullOrWhiteSpace(ky)) query = query.Where(su => su.KySuDung == ky);
        if (roomId.HasValue) query = query.Where(su => su.MaPhong == roomId);
        return await query.OrderByDescending(su => su.KySuDung).ThenBy(su => su.Room!.MaPhongKyHieu).ToListAsync();
    }

    public Task<ServiceUsage?> GetUsageByIdAsync(int id) =>
        _db.ServiceUsages.Include(su => su.Room).Include(su => su.Service).FirstOrDefaultAsync(su => su.MaSuDung == id);

    public Task<string> GenerateKySuDungAsync() => Task.FromResult(DateTime.Now.ToString("yyyy-MM"));

    public async Task<(bool Ok, string Message)> CreateUsageAsync(ServiceUsage usage)
    {
        var service = await _db.Services.FindAsync(usage.MaDichVu);
        if (service == null) return (false, "Không tìm thấy dịch vụ.");

        // Mỗi Phòng + Dịch vụ + Kỳ chỉ có một bản ghi
        if (await _db.ServiceUsages.AnyAsync(su => su.MaPhong == usage.MaPhong && su.MaDichVu == usage.MaDichVu && su.KySuDung == usage.KySuDung))
            return (false, $"Phòng này đã sử dụng dịch vụ {service.TenDichVu} trong kỳ {usage.KySuDung}.");

        // Phải lưu đơn giá áp dụng tại thời điểm ghi (không phụ thuộc giá hiện tại)
        if (usage.DonGiaApDung <= 0) usage.DonGiaApDung = service.DonGia;
        usage.ThanhTien = usage.SoLuong * usage.DonGiaApDung;

        _db.ServiceUsages.Add(usage);
        await _db.SaveChangesAsync();
        _logger.LogInformation("Ghi sử dụng dịch vụ {DichVu} phòng {MaPhong} kỳ {Ky}", service.TenDichVu, usage.MaPhong, usage.KySuDung);
        return (true, $"Ghi sử dụng dịch vụ {service.TenDichVu} kỳ {usage.KySuDung} thành công.");
    }

    public async Task<(bool Ok, string Message)> UpdateUsageAsync(ServiceUsage usage)
    {
        if (await _db.ServiceUsages.AnyAsync(su => su.MaPhong == usage.MaPhong && su.MaDichVu == usage.MaDichVu && su.KySuDung == usage.KySuDung && su.MaSuDung != usage.MaSuDung))
            return (false, "Đã tồn tại bản ghi khác cho phòng + dịch vụ + kỳ này.");

        var existed = await _db.ServiceUsages.FindAsync(usage.MaSuDung);
        if (existed == null) return (false, "Không tìm thấy bản ghi.");

        existed.SoLuong = usage.SoLuong;
        existed.DonGiaApDung = usage.DonGiaApDung;
        existed.ThanhTien = usage.SoLuong * usage.DonGiaApDung;
        existed.GhiChu = usage.GhiChu;
        await _db.SaveChangesAsync();
        return (true, "Cập nhật sử dụng dịch vụ thành công.");
    }

    public async Task<(bool Ok, string Message)> DeleteUsageAsync(int id)
    {
        var usage = await _db.ServiceUsages.FindAsync(id);
        if (usage == null) return (false, "Không tìm thấy bản ghi.");

        var daLapHoaDon = await _db.Invoices.AnyAsync(i => i.MaPhong == usage.MaPhong && i.KyHoaDon == usage.KySuDung);
        if (daLapHoaDon)
            return (false, $"Không thể xóa vì kỳ {usage.KySuDung} đã lập hóa đơn. Hãy xóa hóa đơn trước hoặc cập nhật số lượng.");

        _db.ServiceUsages.Remove(usage);
        await _db.SaveChangesAsync();
        return (true, $"Đã xóa bản ghi sử dụng dịch vụ kỳ {usage.KySuDung}.");
    }
}
