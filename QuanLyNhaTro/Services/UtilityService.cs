using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Services;

public interface IUtilityService
{
    Task<List<UtilityReading>> GetAllAsync(string? search = null, string? ky = null);
    Task<UtilityReading?> GetByIdAsync(int id);
    Task<UtilityReading?> GetByRoomAndKyAsync(int roomId, string ky);
    Task<UtilityReading?> GetLatestBeforeAsync(int roomId, string ky);
    Task<string> GenerateKyAsync();
    Task<(bool Ok, string Message)> CreateAsync(UtilityReading reading);
    Task<(bool Ok, string Message)> UpdateAsync(UtilityReading reading);
    Task<(bool Ok, string Message)> DeleteAsync(int id);
}

public class UtilityService : IUtilityService
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<UtilityService> _logger;

    public UtilityService(ApplicationDbContext db, ILogger<UtilityService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<UtilityReading>> GetAllAsync(string? search = null, string? ky = null)
    {
        var query = _db.UtilityReadings.AsNoTracking().Include(u => u.Room).AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var kw = search.Trim();
            query = query.Where(u => u.Room != null && (u.Room.MaPhongKyHieu.Contains(kw) || u.Room.TenPhong.Contains(kw)));
        }
        if (!string.IsNullOrWhiteSpace(ky)) query = query.Where(u => u.KyGhi == ky);
        return await query.OrderByDescending(u => u.KyGhi).ThenBy(u => u.Room!.MaPhongKyHieu).ToListAsync();
    }

    public Task<UtilityReading?> GetByIdAsync(int id) =>
        _db.UtilityReadings.Include(u => u.Room).FirstOrDefaultAsync(u => u.MaGhiChiSo == id);

    public Task<UtilityReading?> GetByRoomAndKyAsync(int roomId, string ky) =>
        _db.UtilityReadings.FirstOrDefaultAsync(u => u.MaPhong == roomId && u.KyGhi == ky);

    // Lấy bản ghi gần nhất trước kỳ để tự điền chỉ số cũ & đơn giá
    public async Task<UtilityReading?> GetLatestBeforeAsync(int roomId, string ky)
    {
        return await _db.UtilityReadings
            .Where(u => u.MaPhong == roomId && string.Compare(u.KyGhi, ky) < 0)
            .OrderByDescending(u => u.KyGhi)
            .FirstOrDefaultAsync();
    }

    public Task<string> GenerateKyAsync() => Task.FromResult(DateTime.Now.ToString("yyyy-MM"));

    public async Task<(bool Ok, string Message)> CreateAsync(UtilityReading reading)
    {
        // Chỉ số mới >= chỉ số cũ
        if (reading.ChiSoDienMoi < reading.ChiSoDienCu)
            return (false, "Không thể nhập chỉ số điện vì chỉ số mới nhỏ hơn chỉ số cũ.");
        if (reading.ChiSoNuocMoi < reading.ChiSoNuocCu)
            return (false, "Không thể nhập chỉ số nước vì chỉ số mới nhỏ hơn chỉ số cũ.");

        // Mỗi Phòng + Kỳ chỉ có một bản ghi
        if (await _db.UtilityReadings.AnyAsync(u => u.MaPhong == reading.MaPhong && u.KyGhi == reading.KyGhi))
            return (false, $"Phòng này đã có bản ghi điện nước cho kỳ {reading.KyGhi}. Vui lòng sửa bản ghi hiện có.");

        _db.UtilityReadings.Add(reading);
        await _db.SaveChangesAsync();
        _logger.LogInformation("Ghi chỉ số điện nước phòng {MaPhong} kỳ {KyGhi}", reading.MaPhong, reading.KyGhi);
        return (true, $"Ghi chỉ số điện nước kỳ {reading.KyGhi} thành công.");
    }

    public async Task<(bool Ok, string Message)> UpdateAsync(UtilityReading reading)
    {
        if (reading.ChiSoDienMoi < reading.ChiSoDienCu)
            return (false, "Không thể cập nhật vì chỉ số điện mới nhỏ hơn chỉ số cũ.");
        if (reading.ChiSoNuocMoi < reading.ChiSoNuocCu)
            return (false, "Không thể cập nhật vì chỉ số nước mới nhỏ hơn chỉ số cũ.");

        if (await _db.UtilityReadings.AnyAsync(u => u.MaPhong == reading.MaPhong && u.KyGhi == reading.KyGhi && u.MaGhiChiSo != reading.MaGhiChiSo))
            return (false, "Đã tồn tại bản ghi khác cho phòng + kỳ này.");

        var existed = await _db.UtilityReadings.FindAsync(reading.MaGhiChiSo);
        if (existed == null) return (false, "Không tìm thấy bản ghi.");

        existed.ChiSoDienCu = reading.ChiSoDienCu;
        existed.ChiSoDienMoi = reading.ChiSoDienMoi;
        existed.DonGiaDien = reading.DonGiaDien;
        existed.ChiSoNuocCu = reading.ChiSoNuocCu;
        existed.ChiSoNuocMoi = reading.ChiSoNuocMoi;
        existed.DonGiaNuoc = reading.DonGiaNuoc;
        existed.GhiChu = reading.GhiChu;

        await _db.SaveChangesAsync();
        return (true, "Cập nhật chỉ số điện nước thành công.");
    }

    public async Task<(bool Ok, string Message)> DeleteAsync(int id)
    {
        var reading = await _db.UtilityReadings.FindAsync(id);
        if (reading == null) return (false, "Không tìm thấy bản ghi.");

        // Nếu kỳ đã lập hóa đơn thì không cho xóa (bảo vệ hóa đơn lịch sử)
        var daLapHoaDon = await _db.Invoices.AnyAsync(i => i.MaPhong == reading.MaPhong && i.KyHoaDon == reading.KyGhi);
        if (daLapHoaDon)
            return (false, $"Không thể xóa vì kỳ {reading.KyGhi} đã lập hóa đơn. Hãy xóa hóa đơn trước hoặc cập nhật chỉ số.");

        _db.UtilityReadings.Remove(reading);
        await _db.SaveChangesAsync();
        return (true, $"Đã xóa bản ghi điện nước kỳ {reading.KyGhi}.");
    }
}
