using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Services;

public interface IContractService
{
    Task<List<Contract>> GetAllAsync(string? search = null, int? roomId = null, int? tenantId = null, ContractStatus? status = null, bool? sapHetHan = null);
    Task<Contract?> GetByIdAsync(int id);
    Task<Contract?> GetDetailAsync(int id);
    Task<List<Contract>> GetActiveContractsAsync();
    Task<string> GenerateMaHopDongAsync();
    Task<bool> IsOverlappingAsync(int roomId, DateTime ngayBatDau, DateTime ngayKetThuc, int? excludeId = null);
    Task<(bool Ok, string Message)> CreateAsync(Contract contract);
    Task<(bool Ok, string Message)> RenewAsync(int id, DateTime ngayKetThucMoi, decimal? giaThueMoi, decimal? tienCocMoi, string? ghiChu);
    Task<(bool Ok, string Message)> LiquidateAsync(int id, string? ghiChu);
    int SoNgaySapHetHan { get; }
}

public class ContractService : IContractService
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<ContractService> _logger;

    public const int NguongSapHetHan = 30; // cảnh báo trước 30 ngày

    public ContractService(ApplicationDbContext db, ILogger<ContractService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public int SoNgaySapHetHan => NguongSapHetHan;

    public async Task<List<Contract>> GetAllAsync(string? search = null, int? roomId = null, int? tenantId = null, ContractStatus? status = null, bool? sapHetHan = null)
    {
        var query = _db.Contracts.AsNoTracking()
            .Include(c => c.Room)
            .Include(c => c.Tenant)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var kw = search.Trim();
            query = query.Where(c => c.MaHopDongKyHieu.Contains(kw)
                || c.Room != null && c.Room.MaPhongKyHieu.Contains(kw)
                || c.Tenant != null && c.Tenant.HoTen.Contains(kw));
        }
        if (roomId.HasValue) query = query.Where(c => c.MaPhong == roomId);
        if (tenantId.HasValue) query = query.Where(c => c.MaNguoiThue == tenantId);
        if (sapHetHan == true)
        {
            var limit = DateTime.Today.AddDays(NguongSapHetHan);
            query = query.Where(c => c.TrangThai != ContractStatus.DaThanhLy && c.NgayKetThuc <= limit);
        }
        else if (status.HasValue)
        {
            if (status.Value == ContractStatus.SapHetHan)
            {
                var limit = DateTime.Today.AddDays(NguongSapHetHan);
                query = query.Where(c => c.TrangThai != ContractStatus.DaThanhLy && c.NgayKetThuc <= limit && c.NgayKetThuc >= DateTime.Today);
            }
            else
            {
                query = query.Where(c => c.TrangThai == status.Value);
            }
        }

        return await query.OrderByDescending(c => c.NgayBatDau).ToListAsync();
    }

    public Task<Contract?> GetByIdAsync(int id) => _db.Contracts.Include(c => c.Room).Include(c => c.Tenant).FirstOrDefaultAsync(c => c.MaHopDong == id);

    public async Task<Contract?> GetDetailAsync(int id) =>
        await _db.Contracts
            .Include(c => c.Room)
            .Include(c => c.Tenant)
            .Include(c => c.Invoices)
            .Include(c => c.Histories)
            .Include(c => c.MoveOut)
            .FirstOrDefaultAsync(c => c.MaHopDong == id);

    public async Task<List<Contract>> GetActiveContractsAsync()
    {
        var today = DateTime.Today;
        return await _db.Contracts.AsNoTracking()
            .Include(c => c.Room)
            .Include(c => c.Tenant)
            .Where(c => c.TrangThai != ContractStatus.DaThanhLy && c.NgayBatDau <= today && c.NgayKetThuc >= today)
            .OrderBy(c => c.Room!.MaPhongKyHieu)
            .ToListAsync();
    }

    public async Task<string> GenerateMaHopDongAsync()
    {
        var count = await _db.Contracts.CountAsync();
        string code;
        do
        {
            code = $"HD{count + 1:0000}";
            count++;
        } while (await _db.Contracts.AnyAsync(c => c.MaHopDongKyHieu == code));
        return code;
    }

    public async Task<bool> IsOverlappingAsync(int roomId, DateTime ngayBatDau, DateTime ngayKetThuc, int? excludeId = null)
    {
        // Một phòng không được có hai hợp đồng còn hiệu lực trong cùng khoảng thời gian
        return await _db.Contracts.AnyAsync(c =>
            c.MaPhong == roomId
            && c.TrangThai != ContractStatus.DaThanhLy
            && (excludeId == null || c.MaHopDong != excludeId)
            && c.NgayBatDau <= ngayKetThuc
            && c.NgayKetThuc >= ngayBatDau);
    }

    public async Task<(bool Ok, string Message)> CreateAsync(Contract contract)
    {
        // Validation server-side bắt buộc theo đặc tả
        if (contract.NgayKetThuc <= contract.NgayBatDau)
            return (false, "Ngày kết thúc phải sau ngày bắt đầu.");
        if (contract.GiaThue <= 0) return (false, "Giá thuê phải lớn hơn 0.");
        if (contract.TienCoc < 0) return (false, "Tiền cọc phải lớn hơn hoặc bằng 0.");

        if (await IsOverlappingAsync(contract.MaPhong, contract.NgayBatDau, contract.NgayKetThuc))
            return (false, "Không thể lập hợp đồng vì phòng đã có hợp đồng còn hiệu lực chồng lên khoảng thời gian này.");

        if (await _db.Contracts.AnyAsync(c => c.MaHopDongKyHieu == contract.MaHopDongKyHieu))
            return (false, $"Mã hợp đồng {contract.MaHopDongKyHieu} đã tồn tại.");

        var room = await _db.Rooms.FindAsync(contract.MaPhong);
        if (room == null) return (false, "Không tìm thấy phòng.");
        var tenant = await _db.Tenants.FindAsync(contract.MaNguoiThue);
        if (tenant == null) return (false, "Không tìm thấy người thuê.");

        if (string.IsNullOrWhiteSpace(contract.MaHopDongKyHieu))
            contract.MaHopDongKyHieu = await GenerateMaHopDongAsync();

        contract.TrangThai = ContractStatus.HieuLuc;
        _db.Contracts.Add(contract);

        // Cập nhật trạng thái phòng: đã bắt đầu thuê → Đang thuê, chưa đến ngày → Đã cọc
        room.TrangThai = contract.NgayBatDau <= DateTime.Today ? RoomStatus.DangThue : RoomStatus.DaCoc;
        tenant.TrangThai = "Đang thuê";

        _db.ContractHistories.Add(new ContractHistory
        {
            MaHopDong = contract.MaHopDong,
            HanhDong = "Tạo mới",
            NoiDung = $"Lập hợp đồng thuê phòng {room.MaPhongKyHieu}, thời hạn {contract.NgayBatDau:dd/MM/yyyy} - {contract.NgayKetThuc:dd/MM/yyyy}.",
            ThoiGian = DateTime.Now
        });

        await _db.SaveChangesAsync();
        _logger.LogInformation("Lập hợp đồng {MaHopDong} phòng {Room}", contract.MaHopDongKyHieu, room.MaPhongKyHieu);
        return (true, $"Lập hợp đồng {contract.MaHopDongKyHieu} thành công.");
    }

    public async Task<(bool Ok, string Message)> RenewAsync(int id, DateTime ngayKetThucMoi, decimal? giaThueMoi, decimal? tienCocMoi, string? ghiChu)
    {
        var contract = await _db.Contracts.Include(c => c.Room).Include(c => c.Tenant).FirstOrDefaultAsync(c => c.MaHopDong == id);
        if (contract == null) return (false, "Không tìm thấy hợp đồng.");
        if (contract.TrangThai == ContractStatus.DaThanhLy) return (false, "Hợp đồng đã thanh lý, không thể gia hạn.");
        if (ngayKetThucMoi <= contract.NgayKetThuc) return (false, "Ngày kết thúc mới phải sau ngày kết thúc hiện tại.");
        if (giaThueMoi.HasValue && giaThueMoi.Value <= 0) return (false, "Giá thuê mới phải lớn hơn 0.");
        if (tienCocMoi.HasValue && tienCocMoi.Value < 0) return (false, "Tiền cọc mới phải lớn hơn hoặc bằng 0.");

        // Hạn mới không được chồng lên hợp đồng khác của cùng phòng
        if (await IsOverlappingAsync(contract.MaPhong, contract.NgayBatDau, ngayKetThucMoi, contract.MaHopDong))
            return (false, "Khoảng thời gian gia hạn bị chồng lấn với hợp đồng khác của phòng này.");

        var noiDung = $"Gia hạn hợp đồng đến {ngayKetThucMoi:dd/MM/yyyy}";
        if (giaThueMoi.HasValue && giaThueMoi.Value != contract.GiaThue)
        {
            noiDung += $", giá thuê mới {giaThueMoi.Value:N0} đ/tháng";
            contract.GiaThue = giaThueMoi.Value;
        }
        if (tienCocMoi.HasValue && tienCocMoi.Value != contract.TienCoc)
        {
            noiDung += $", tiền cọc mới {tienCocMoi.Value:N0} đ";
            contract.TienCoc = tienCocMoi.Value;
        }
        noiDung += ".";

        contract.NgayKetThuc = ngayKetThucMoi;
        if (!string.IsNullOrWhiteSpace(ghiChu))
            contract.GhiChu = ghiChu;

        _db.ContractHistories.Add(new ContractHistory
        {
            MaHopDong = contract.MaHopDong,
            HanhDong = "Gia hạn",
            NoiDung = noiDung,
            ThoiGian = DateTime.Now
        });

        await _db.SaveChangesAsync();
        _logger.LogInformation("Gia hạn hợp đồng {MaHopDong}", contract.MaHopDongKyHieu);
        return (true, $"Gia hạn hợp đồng {contract.MaHopDongKyHieu} đến {ngayKetThucMoi:dd/MM/yyyy} thành công.");
    }

    public async Task<(bool Ok, string Message)> LiquidateAsync(int id, string? ghiChu)
    {
        var contract = await _db.Contracts.Include(c => c.Room).Include(c => c.Tenant).FirstOrDefaultAsync(c => c.MaHopDong == id);
        if (contract == null) return (false, "Không tìm thấy hợp đồng.");
        if (contract.TrangThai == ContractStatus.DaThanhLy) return (false, "Hợp đồng đã được thanh lý trước đó.");

        var room = contract.Room!;

        // Kiểm tra công nợ trước khi thanh lý
        var conNo = await _db.Invoices.AnyAsync(i => i.MaPhong == room.MaPhong && i.TrangThaiThanhToan == InvoicePaymentStatus.ChuaThanhToan);
        if (conNo)
            return (false, "Không thể thanh lý vì phòng còn hóa đơn chưa thanh toán. Hãy ghi nhận thanh toán hoặc quyết toán trả phòng trước.");

        contract.TrangThai = ContractStatus.DaThanhLy;
        if (!string.IsNullOrWhiteSpace(ghiChu)) contract.GhiChu = ghiChu;

        _db.ContractHistories.Add(new ContractHistory
        {
            MaHopDong = contract.MaHopDong,
            HanhDong = "Thanh lý",
            NoiDung = $"Thanh lý hợp đồng ngày {DateTime.Today:dd/MM/yyyy}.",
            ThoiGian = DateTime.Now
        });

        // Phòng về Trống nếu không còn hợp đồng hiệu lực nào khác
        var today = DateTime.Today;
        var conHieuLuc = await _db.Contracts.AnyAsync(c => c.MaPhong == room.MaPhong && c.MaHopDong != contract.MaHopDong && c.TrangThai != ContractStatus.DaThanhLy && c.NgayKetThuc >= today);
        if (!conHieuLuc) room.TrangThai = RoomStatus.Trong;

        var tenantConHD = await _db.Contracts.AnyAsync(c => c.MaNguoiThue == contract.MaNguoiThue && c.MaHopDong != contract.MaHopDong && c.TrangThai != ContractStatus.DaThanhLy);
        if (!tenantConHD && contract.Tenant != null) contract.Tenant.TrangThai = "Đã trả phòng";

        await _db.SaveChangesAsync();
        _logger.LogInformation("Thanh lý hợp đồng {MaHopDong}", contract.MaHopDongKyHieu);
        return (true, $"Thanh lý hợp đồng {contract.MaHopDongKyHieu} thành công.");
    }
}
