using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Services;

public interface IRoomService
{
    Task<List<Room>> GetAllAsync(string? search = null, RoomStatus? trangThai = null);
    Task<Room?> GetByIdAsync(int id);
    Task<Room?> GetDetailAsync(int id);
    Task<bool> HasBusinessDataAsync(int id);
    Task<string> GenerateMaPhongAsync();
    Task<(bool Ok, string Message)> CreateAsync(Room room);
    Task<(bool Ok, string Message)> UpdateAsync(Room room);
    Task<(bool Ok, string Message)> DeleteAsync(int id);
}

public class RoomService : IRoomService
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<RoomService> _logger;

    public RoomService(ApplicationDbContext db, ILogger<RoomService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<Room>> GetAllAsync(string? search = null, RoomStatus? trangThai = null)
    {
        var query = _db.Rooms.AsNoTracking()
            .Include(r => r.Contracts).ThenInclude(c => c.Tenant)
            .Where(r => !r.DaXoa);
        if (!string.IsNullOrWhiteSpace(search))
        {
            var kw = search.Trim();
            query = query.Where(r => r.MaPhongKyHieu.Contains(kw) || r.TenPhong.Contains(kw));
        }
        if (trangThai.HasValue) query = query.Where(r => r.TrangThai == trangThai.Value);
        return await query.OrderBy(r => r.MaPhongKyHieu).ToListAsync();
    }

    public Task<Room?> GetByIdAsync(int id) => _db.Rooms.FirstOrDefaultAsync(r => r.MaPhong == id);

    public async Task<Room?> GetDetailAsync(int id)
    {
        var room = await _db.Rooms
            .Include(r => r.Contracts).ThenInclude(c => c.Tenant)
            .Include(r => r.UtilityReadings)
            .Include(r => r.ServiceUsages).ThenInclude(su => su.Service)
            .Include(r => r.Invoices)
            .FirstOrDefaultAsync(r => r.MaPhong == id);
        return room;
    }

    public async Task<bool> HasBusinessDataAsync(int id)
    {
        var hasContract = await _db.Contracts.AnyAsync(c => c.MaPhong == id);
        var hasUtility = await _db.UtilityReadings.AnyAsync(u => u.MaPhong == id);
        var hasUsage = await _db.ServiceUsages.AnyAsync(su => su.MaPhong == id);
        var hasInvoice = await _db.Invoices.AnyAsync(i => i.MaPhong == id);
        return hasContract || hasUtility || hasUsage || hasInvoice;
    }

    public async Task<string> GenerateMaPhongAsync()
    {
        var max = await _db.Rooms.IgnoreQueryFilters()
            .Where(r => r.MaPhongKyHieu.StartsWith("P"))
            .OrderByDescending(r => r.MaPhongKyHieu)
            .Select(r => r.MaPhongKyHieu)
            .FirstOrDefaultAsync();
        if (max != null && int.TryParse(max.Substring(1), out var n)) return $"P{n + 1:000}";
        return "P001";
    }

    public async Task<(bool Ok, string Message)> CreateAsync(Room room)
    {
        var duplicated = await _db.Rooms.AnyAsync(r => r.MaPhongKyHieu == room.MaPhongKyHieu && !r.DaXoa);
        if (duplicated) return (false, $"Mã phòng {room.MaPhongKyHieu} đã tồn tại.");

        room.TrangThai = RoomStatus.Trong;
        _db.Rooms.Add(room);
        await _db.SaveChangesAsync();
        _logger.LogInformation("Thêm phòng mới {MaPhong}", room.MaPhongKyHieu);
        return (true, $"Thêm phòng {room.TenPhong} thành công.");
    }

    public async Task<(bool Ok, string Message)> UpdateAsync(Room room)
    {
        var duplicated = await _db.Rooms.AnyAsync(r => r.MaPhongKyHieu == room.MaPhongKyHieu && r.MaPhong != room.MaPhong && !r.DaXoa);
        if (duplicated) return (false, $"Mã phòng {room.MaPhongKyHieu} đã tồn tại ở phòng khác.");

        var existed = await _db.Rooms.FindAsync(room.MaPhong);
        if (existed == null) return (false, "Không tìm thấy phòng.");

        existed.MaPhongKyHieu = room.MaPhongKyHieu;
        existed.TenPhong = room.TenPhong;
        existed.DienTich = room.DienTich;
        existed.LoaiPhong = room.LoaiPhong;
        existed.GiaPhong = room.GiaPhong;
        existed.GhiChu = room.GhiChu;

        await _db.SaveChangesAsync();
        return (true, $"Cập nhật phòng {room.TenPhong} thành công.");
    }

    public async Task<(bool Ok, string Message)> DeleteAsync(int id)
    {
        var room = await _db.Rooms.FindAsync(id);
        if (room == null) return (false, "Không tìm thấy phòng.");

        // Quy tắc xóa: phòng đã phát sinh dữ liệu nghiệp vụ thì KHÔNG xóa vật lý
        if (await HasBusinessDataAsync(id))
        {
            room.DaXoa = true;
            await _db.SaveChangesAsync();
            _logger.LogInformation("Phòng {MaPhong} đã có lịch sử nghiệp vụ, chuyển sang ngừng sử dụng", room.MaPhongKyHieu);
            return (true, $"Phòng {room.MaPhongKyHieu} đã phát sinh dữ liệu nghiệp vụ nên được chuyển sang trạng thái Ngừng sử dụng (giữ nguyên lịch sử).");
        }

        _db.Rooms.Remove(room);
        await _db.SaveChangesAsync();
        return (true, $"Đã xóa phòng {room.MaPhongKyHieu}.");
    }
}
