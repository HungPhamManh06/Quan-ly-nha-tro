using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Services;

public interface ITenantService
{
    Task<List<TenantListItem>> GetAllAsync(string? search = null);
    Task<Tenant?> GetByIdAsync(int id);
    Task<Tenant?> GetDetailAsync(int id);
    Task<Contract?> GetActiveContractAsync(int tenantId);
    Task<string> GenerateTrangThaiAsync(int tenantId);
    Task<(bool Ok, string Message)> CreateAsync(Tenant tenant);
    Task<(bool Ok, string Message)> UpdateAsync(Tenant tenant);
    Task<(bool Ok, string Message)> DeleteAsync(int id);
}

public class TenantListItem
{
    public Tenant Tenant { get; set; } = null!;
    public string? PhongHienTai { get; set; }
    public int? MaPhongHienTai { get; set; }
}

public class TenantService : ITenantService
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<TenantService> _logger;

    public TenantService(ApplicationDbContext db, ILogger<TenantService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<TenantListItem>> GetAllAsync(string? search = null)
    {
        var query = _db.Tenants.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var kw = search.Trim();
            query = query.Where(t => t.HoTen.Contains(kw) || t.CCCD.Contains(kw) || (t.SoDienThoai != null && t.SoDienThoai.Contains(kw)));
        }
        var tenants = await query.OrderBy(t => t.HoTen).ToListAsync();
        var today = DateTime.Today;

        // Phòng hiện tại xác định từ hợp đồng còn hiệu lực (không lưu độc lập tại Tenant)
        var activeContracts = await _db.Contracts.AsNoTracking()
            .Include(c => c.Room)
            .Where(c => c.TrangThai != ContractStatus.DaThanhLy && c.NgayBatDau <= today && c.NgayKetThuc >= today)
            .ToDictionaryAsync(c => c.MaNguoiThue, c => c);

        var result = tenants.Select(t =>
        {
            activeContracts.TryGetValue(t.MaNguoiThue, out var contract);
            return new TenantListItem
            {
                Tenant = t,
                PhongHienTai = contract?.Room?.MaPhongKyHieu,
                MaPhongHienTai = contract?.MaPhong
            };
        }).ToList();
        return result;
    }

    public Task<Tenant?> GetByIdAsync(int id) => _db.Tenants.FirstOrDefaultAsync(t => t.MaNguoiThue == id);

    public Task<Tenant?> GetDetailAsync(int id) =>
        _db.Tenants.Include(t => t.Contracts).ThenInclude(c => c.Room)
            .Include(t => t.Contracts).ThenInclude(c => c.Invoices)
            .FirstOrDefaultAsync(t => t.MaNguoiThue == id);

    public async Task<Contract?> GetActiveContractAsync(int tenantId)
    {
        var today = DateTime.Today;
        return await _db.Contracts.Include(c => c.Room)
            .Where(c => c.MaNguoiThue == tenantId && c.TrangThai != ContractStatus.DaThanhLy && c.NgayBatDau <= today && c.NgayKetThuc >= today)
            .OrderByDescending(c => c.NgayBatDau)
            .FirstOrDefaultAsync();
    }

    public async Task<string> GenerateTrangThaiAsync(int tenantId)
    {
        var active = await GetActiveContractAsync(tenantId);
        return active != null ? "Đang thuê" : "Chưa thuê";
    }

    public async Task<(bool Ok, string Message)> CreateAsync(Tenant tenant)
    {
        if (await _db.Tenants.AnyAsync(t => t.CCCD == tenant.CCCD))
            return (false, $"CCCD {tenant.CCCD} đã tồn tại (mỗi người thuê có CCCD duy nhất).");

        tenant.TrangThai = "Chưa thuê";
        _db.Tenants.Add(tenant);
        await _db.SaveChangesAsync();
        _logger.LogInformation("Thêm người thuê mới {HoTen}", tenant.HoTen);
        return (true, $"Thêm người thuê {tenant.HoTen} thành công.");
    }

    public async Task<(bool Ok, string Message)> UpdateAsync(Tenant tenant)
    {
        if (await _db.Tenants.AnyAsync(t => t.CCCD == tenant.CCCD && t.MaNguoiThue != tenant.MaNguoiThue))
            return (false, $"CCCD {tenant.CCCD} đã tồn tại ở người thuê khác.");

        var existed = await _db.Tenants.FindAsync(tenant.MaNguoiThue);
        if (existed == null) return (false, "Không tìm thấy người thuê.");

        existed.HoTen = tenant.HoTen;
        existed.CCCD = tenant.CCCD;
        existed.SoDienThoai = tenant.SoDienThoai;
        existed.QueQuan = tenant.QueQuan;
        existed.GhiChu = tenant.GhiChu;
        await _db.SaveChangesAsync();
        return (true, $"Cập nhật người thuê {tenant.HoTen} thành công.");
    }

    public async Task<(bool Ok, string Message)> DeleteAsync(int id)
    {
        var tenant = await _db.Tenants.FindAsync(id);
        if (tenant == null) return (false, "Không tìm thấy người thuê.");

        // Đã phát sinh lịch sử (hợp đồng / hóa đơn / thanh toán) thì không xóa cứng
        var hasContract = await _db.Contracts.AnyAsync(c => c.MaNguoiThue == id);
        var hasInvoice = await _db.Invoices.AnyAsync(i => i.Contract != null && i.Contract.MaNguoiThue == id);
        if (hasContract || hasInvoice)
        {
            _logger.LogInformation("Chặn xóa người thuê {HoTen} vì đã có lịch sử", tenant.HoTen);
            return (false, $"Không thể xóa người thuê {tenant.HoTen} vì đã phát sinh lịch sử hợp đồng. Có thể cập nhật trạng thái trong ghi chú.");
        }

        _db.Tenants.Remove(tenant);
        await _db.SaveChangesAsync();
        return (true, $"Đã xóa người thuê {tenant.HoTen}.");
    }
}
