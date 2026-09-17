using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Extensions;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.ViewModels;

namespace QuanLyNhaTro.Services;

public interface IDashboardService
{
    Task<DashboardViewModel> GetDashboardAsync();
}

public class DashboardService : IDashboardService
{
    private readonly ApplicationDbContext _db;

    public DashboardService(ApplicationDbContext db) => _db = db;

    public async Task<DashboardViewModel> GetDashboardAsync()
    {
        var today = DateTime.Today;
        var limit = today.AddDays(30);
        var vm = new DashboardViewModel();

        // Phòng
        vm.TongSoPhong = await _db.Rooms.CountAsync();
        vm.PhongTrong = await _db.Rooms.CountAsync(r => r.TrangThai == RoomStatus.Trong);
        vm.PhongDaCoc = await _db.Rooms.CountAsync(r => r.TrangThai == RoomStatus.DaCoc);
        vm.PhongDangThue = await _db.Rooms.CountAsync(r => r.TrangThai == RoomStatus.DangThue);
        vm.PhongDangSuaChua = await _db.Rooms.CountAsync(r => r.TrangThai == RoomStatus.DangSuaChua);

        // Người thuê
        vm.NguoiThueDangO = await _db.Tenants.CountAsync(t => t.TrangThai == "Đang thuê");

        // Hóa đơn
        vm.HoaDonChuaThanhToan = await _db.Invoices.CountAsync(i => i.TrangThaiThanhToan == InvoicePaymentStatus.ChuaThanhToan);
        vm.HoaDonDaThanhToan = await _db.Invoices.CountAsync(i => i.TrangThaiThanhToan == InvoicePaymentStatus.DaThanhToan);

        // Tài chính: Doanh thu = tổng hóa đơn Đã thanh toán; Công nợ = tổng hóa đơn Chưa thanh toán
        // (tính phía client để tương thích cả SQLite demo)
        var invoiceMoney = await _db.Invoices.Select(i => new { i.TongTien, i.TrangThaiThanhToan }).ToListAsync();
        vm.DoanhThu = invoiceMoney.Where(i => i.TrangThaiThanhToan == InvoicePaymentStatus.DaThanhToan).Sum(i => i.TongTien);
        vm.CongNo = invoiceMoney.Where(i => i.TrangThaiThanhToan == InvoicePaymentStatus.ChuaThanhToan).Sum(i => i.TongTien);

        // Hợp đồng
        vm.HopDongHieuLuc = await _db.Contracts.CountAsync(c => c.TrangThai != ContractStatus.DaThanhLy && c.NgayKetThuc >= today);
        vm.HopDongSapHetHan = await _db.Contracts
            .CountAsync(c => c.TrangThai != ContractStatus.DaThanhLy && c.NgayKetThuc >= today && c.NgayKetThuc <= limit);

        // Biểu đồ trạng thái phòng
        vm.TrangThaiPhongChart.Add(new ChartItemViewModel("Trống", vm.PhongTrong));
        vm.TrangThaiPhongChart.Add(new ChartItemViewModel("Đã cọc", vm.PhongDaCoc));
        vm.TrangThaiPhongChart.Add(new ChartItemViewModel("Đang thuê", vm.PhongDangThue));
        vm.TrangThaiPhongChart.Add(new ChartItemViewModel("Đang sửa chữa", vm.PhongDangSuaChua));

        // Doanh thu & công nợ theo tháng (12 tháng gần nhất)
        var startKy = today.AddMonths(-11).ToString("yyyy-MM");
        var invoices = await _db.Invoices
            .Where(i => string.Compare(i.KyHoaDon, startKy) >= 0)
            .Select(i => new { i.KyHoaDon, i.TongTien, i.TrangThaiThanhToan })
            .ToListAsync();
        for (var d = today.AddMonths(-11); d <= today; d = d.AddMonths(1))
        {
            var ky = d.ToString("yyyy-MM");
            var inKy = invoices.Where(i => i.KyHoaDon == ky).ToList();
            vm.Thangs.Add(ky);
            vm.DoanhThuChart.Add(new ChartItemViewModel(ky, inKy.Where(i => i.TrangThaiThanhToan == InvoicePaymentStatus.DaThanhToan).Sum(i => i.TongTien)));
            vm.CongNoChart.Add(new ChartItemViewModel(ky, inKy.Where(i => i.TrangThaiThanhToan == InvoicePaymentStatus.ChuaThanhToan).Sum(i => i.TongTien)));
        }

        // Điện nước theo tháng
        var readings = await _db.UtilityReadings
            .Where(u => string.Compare(u.KyGhi, startKy) >= 0)
            .Select(u => new { u.KyGhi, u.ChiSoDienMoi, u.ChiSoDienCu, u.ChiSoNuocMoi, u.ChiSoNuocCu })
            .ToListAsync();
        for (var d = today.AddMonths(-11); d <= today; d = d.AddMonths(1))
        {
            var ky = d.ToString("yyyy-MM");
            var inKy = readings.Where(u => u.KyGhi == ky).ToList();
            vm.DienNuocChart.Add(new ChartItemViewModel(ky, inKy.Sum(u => u.ChiSoDienMoi - u.ChiSoDienCu), inKy.Sum(u => u.ChiSoNuocMoi - u.ChiSoNuocCu)));
        }

        // Xu hướng doanh thu: tháng này vs tháng trước
        var kyNay = today.ToString("yyyy-MM");
        var kyTruoc = today.AddMonths(-1).ToString("yyyy-MM");
        vm.DoanhThuThangNay = vm.DoanhThuChart.FirstOrDefault(c => c.Label == kyNay)?.GiaTri1 ?? 0;
        vm.DoanhThuThangTruoc = vm.DoanhThuChart.FirstOrDefault(c => c.Label == kyTruoc)?.GiaTri1 ?? 0;

        // ===== Cần xử lý: hóa đơn quá hạn/sắp hạn + hợp đồng sắp hết hạn + phòng sửa chữa =====
        var canXuLy = new List<AttentionItemViewModel>();

        var hoaDonCanThu = await _db.Invoices.AsNoTracking().Include(i => i.Room)
            .Where(i => i.TrangThaiThanhToan == InvoicePaymentStatus.ChuaThanhToan)
            .OrderBy(i => i.HanThanhToan)
            .Take(4)
            .ToListAsync();
        foreach (var inv in hoaDonCanThu)
        {
            var conHan = inv.HanThanhToan.HasValue && inv.HanThanhToan.Value.Date < today;
            canXuLy.Add(new AttentionItemViewModel
            {
                Loai = "hoadon",
                MaSo = inv.Room?.MaPhongKyHieu ?? "",
                TieuDe = $"Hóa đơn kỳ {inv.KyHoaDon}",
                MoTa = conHan ? $"Quá hạn thanh toán ({inv.HanThanhToan:dd/MM/yyyy})" : "Chưa thanh toán",
                SoTien = inv.TongTien,
                TrangThai = inv.TrangThaiThanhToan.GetDisplayName(),
                Url = $"/Invoices/Details/{inv.MaHoaDon}"
            });
        }

        var hopDongSapHet = await _db.Contracts.AsNoTracking().Include(c => c.Room)
            .Where(c => c.TrangThai != ContractStatus.DaThanhLy && c.NgayKetThuc >= today && c.NgayKetThuc <= limit)
            .OrderBy(c => c.NgayKetThuc)
            .Take(3)
            .ToListAsync();
        foreach (var c in hopDongSapHet)
        {
            var conSoNgay = (c.NgayKetThuc.Date - today).Days;
            canXuLy.Add(new AttentionItemViewModel
            {
                Loai = "hopdong",
                MaSo = c.Room?.MaPhongKyHieu ?? "",
                TieuDe = "Hợp đồng " + c.MaHopDongKyHieu,
                MoTa = conSoNgay == 0 ? "Hết hạn hôm nay" : $"Hết hạn trong {conSoNgay} ngày ({c.NgayKetThuc:dd/MM/yyyy})",
                TrangThai = "Sắp hết hạn",
                Url = $"/Contracts/Details/{c.MaHopDong}"
            });
        }

        var phongDangSua = await _db.Rooms.AsNoTracking()
            .Where(r => r.TrangThai == RoomStatus.DangSuaChua)
            .OrderBy(r => r.MaPhongKyHieu)
            .Take(2)
            .ToListAsync();
        foreach (var r in phongDangSua)
        {
            canXuLy.Add(new AttentionItemViewModel
            {
                Loai = "phong",
                MaSo = r.MaPhongKyHieu,
                TieuDe = "Phòng " + r.MaPhongKyHieu,
                MoTa = "Đang sửa chữa — theo dõi tiến độ",
                TrangThai = "Đang sửa",
                Url = $"/Rooms/Details/{r.MaPhong}"
            });
        }

        vm.CanXuLy = canXuLy.OrderBy(x => x.Loai == "hoadon" ? 0 : x.Loai == "hopdong" ? 1 : 2).Take(6).ToList();

        return vm;
    }
}

public interface IReportService
{
    Task<List<Room>> GetRoomReportAsync();
    Task<List<TenantReportItem>> GetTenantReportAsync();
    Task<List<Contract>> GetContractReportAsync(ContractStatus? status, bool? sapHetHan);
    Task<List<Invoice>> GetInvoiceReportAsync(string? tuKy, string? denKy, InvoicePaymentStatus? status);
    Task<ReportFinancialViewModel> GetFinancialReportAsync(string? tuKy, string? denKy);
    Task<List<UtilityReportItem>> GetUtilityReportAsync(string? tuKy, string? denKy);
    Task<List<DoanhThuThangItem>> GetDoanhThuTheoThangAsync(string? nam);
}

public class ReportService : IReportService
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<ReportService> _logger;
    private readonly IContractService _contractService;

    public ReportService(ApplicationDbContext db, ILoggerFactory loggerFactory)
    {
        _db = db;
        _logger = loggerFactory.CreateLogger<ReportService>();
        _contractService = new ContractService(db, loggerFactory.CreateLogger<ContractService>());
    }

    public Task<List<Room>> GetRoomReportAsync() =>
        _db.Rooms.AsNoTracking().OrderBy(r => r.MaPhongKyHieu).ToListAsync();

    public async Task<List<TenantReportItem>> GetTenantReportAsync()
    {
        var today = DateTime.Today;
        var tenants = await _db.Tenants.AsNoTracking().OrderBy(t => t.HoTen).ToListAsync();
        var active = await _db.Contracts.AsNoTracking().Include(c => c.Room)
            .Where(c => c.TrangThai != ContractStatus.DaThanhLy && c.NgayBatDau <= today && c.NgayKetThuc >= today)
            .ToDictionaryAsync(c => c.MaNguoiThue, c => c.Room != null ? c.Room.MaPhongKyHieu : "");
        return tenants.Select(t => new TenantReportItem
        {
            MaNguoiThue = t.MaNguoiThue,
            HoTen = t.HoTen,
            CCCD = t.CCCD,
            SoDienThoai = t.SoDienThoai,
            TrangThai = t.TrangThai,
            PhongHienTai = active.TryGetValue(t.MaNguoiThue, out var phong) ? phong : ""
        }).ToList();
    }

    public async Task<List<Contract>> GetContractReportAsync(ContractStatus? status, bool? sapHetHan) =>
        await _contractService.GetAllAsync(status: status, sapHetHan: sapHetHan);

    public async Task<List<Invoice>> GetInvoiceReportAsync(string? tuKy, string? denKy, InvoicePaymentStatus? status)
    {
        var query = _db.Invoices.AsNoTracking().Include(i => i.Room).AsQueryable();
        if (!string.IsNullOrWhiteSpace(tuKy)) query = query.Where(i => string.Compare(i.KyHoaDon, tuKy) >= 0);
        if (!string.IsNullOrWhiteSpace(denKy)) query = query.Where(i => string.Compare(i.KyHoaDon, denKy) <= 0);
        if (status.HasValue) query = query.Where(i => i.TrangThaiThanhToan == status.Value);
        return await query.OrderBy(i => i.KyHoaDon).ToListAsync();
    }

    public async Task<ReportFinancialViewModel> GetFinancialReportAsync(string? tuKy, string? denKy)
    {
        var query = _db.Invoices.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(tuKy)) query = query.Where(i => string.Compare(i.KyHoaDon, tuKy) >= 0);
        if (!string.IsNullOrWhiteSpace(denKy)) query = query.Where(i => string.Compare(i.KyHoaDon, denKy) <= 0);

        var invoices = await query.ToListAsync();
        return new ReportFinancialViewModel
        {
            TuKy = tuKy, DenKy = denKy,
            TongDoanhThu = invoices.Where(i => i.TrangThaiThanhToan == InvoicePaymentStatus.DaThanhToan).Sum(i => i.TongTien),
            TongCongNo = invoices.Where(i => i.TrangThaiThanhToan == InvoicePaymentStatus.ChuaThanhToan).Sum(i => i.TongTien),
            SoHoaDonDaTT = invoices.Count(i => i.TrangThaiThanhToan == InvoicePaymentStatus.DaThanhToan),
            SoHoaDonChuaTT = invoices.Count(i => i.TrangThaiThanhToan == InvoicePaymentStatus.ChuaThanhToan)
        };
    }

    public async Task<List<UtilityReportItem>> GetUtilityReportAsync(string? tuKy, string? denKy)
    {
        var query = _db.UtilityReadings.AsNoTracking().Include(u => u.Room).AsQueryable();
        if (!string.IsNullOrWhiteSpace(tuKy)) query = query.Where(u => string.Compare(u.KyGhi, tuKy) >= 0);
        if (!string.IsNullOrWhiteSpace(denKy)) query = query.Where(u => string.Compare(u.KyGhi, denKy) <= 0);
        var readings = await query.OrderBy(u => u.KyGhi).ToListAsync();
        return readings.Select(u => new UtilityReportItem
        {
            KyGhi = u.KyGhi,
            MaPhong = u.Room?.MaPhongKyHieu ?? "",
            ChiSoDienCu = u.ChiSoDienCu,
            ChiSoDienMoi = u.ChiSoDienMoi,
            SoDienSuDung = u.SoDienSuDung,
            TienDien = u.TienDien,
            ChiSoNuocCu = u.ChiSoNuocCu,
            ChiSoNuocMoi = u.ChiSoNuocMoi,
            SoNuocSuDung = u.SoNuocSuDung,
            TienNuoc = u.TienNuoc
        }).ToList();
    }

    public async Task<List<DoanhThuThangItem>> GetDoanhThuTheoThangAsync(string? nam)
    {
        var year = string.IsNullOrWhiteSpace(nam) ? DateTime.Today.Year : int.Parse(nam);
        var kyPrefix = $"{year}-";
        var invoices = await _db.Invoices.AsNoTracking()
            .Where(i => i.KyHoaDon.StartsWith(kyPrefix))
            .ToListAsync();
        var result = new List<DoanhThuThangItem>();
        for (var m = 1; m <= 12; m++)
        {
            var ky = $"{year}-{m:00}";
            var inKy = invoices.Where(i => i.KyHoaDon == ky).ToList();
            result.Add(new DoanhThuThangItem
            {
                Thang = $"Tháng {m:00}",
                DoanhThu = inKy.Where(i => i.TrangThaiThanhToan == InvoicePaymentStatus.DaThanhToan).Sum(i => i.TongTien),
                CongNo = inKy.Where(i => i.TrangThaiThanhToan == InvoicePaymentStatus.ChuaThanhToan).Sum(i => i.TongTien)
            });
        }
        return result;
    }
}
