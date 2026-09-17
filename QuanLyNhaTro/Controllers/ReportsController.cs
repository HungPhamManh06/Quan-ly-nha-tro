using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTro.Extensions;

namespace QuanLyNhaTro.Controllers;

[Authorize]
public class ReportsController : Controller
{
    private readonly IReportService _reportService;
    private readonly IExportService _exportService;
    private readonly ILogger<ReportsController> _logger;

    public ReportsController(IReportService reportService, IExportService exportService, ILogger<ReportsController> logger)
    {
        _reportService = reportService;
        _exportService = exportService;
        _logger = logger;
    }

    public async Task<IActionResult> Index() => View(await _reportService.GetRoomReportAsync());

    // ---- Báo cáo phòng ----
    public async Task<IActionResult> Rooms() => View(await _reportService.GetRoomReportAsync());

    // ---- Báo cáo người thuê ----
    public async Task<IActionResult> Tenants() => View(await _reportService.GetTenantReportAsync());

    // ---- Báo cáo hợp đồng ----
    public async Task<IActionResult> Contracts(ContractStatus? trangThai, bool? sapHetHan) =>
        View(await _reportService.GetContractReportAsync(trangThai, sapHetHan));

    // ---- Báo cáo hóa đơn ----
    public async Task<IActionResult> Invoices(string? tuKy, string? denKy, InvoicePaymentStatus? trangThai) =>
        View(await _reportService.GetInvoiceReportAsync(tuKy, denKy, trangThai));

    // ---- Báo cáo doanh thu - công nợ ----
    public async Task<IActionResult> Financial(string? tuKy, string? denKy, string? nam)
    {
        ViewBag.Financial = await _reportService.GetFinancialReportAsync(tuKy, denKy);
        ViewBag.DoanhThuThang = await _reportService.GetDoanhThuTheoThangAsync(nam);
        ViewBag.TuKy = tuKy;
        ViewBag.DenKy = denKy;
        ViewBag.Nam = nam ?? DateTime.Today.Year.ToString();
        return View();
    }

    // ---- Báo cáo điện nước ----
    public async Task<IActionResult> Utilities(string? tuKy, string? denKy) =>
        View(await _reportService.GetUtilityReportAsync(tuKy, denKy));

    // ---- Xuất Excel / CSV ----
    public async Task<IActionResult> Export(string type = "rooms", string format = "excel")
    {
        try
        {
            var (sheetName, headers, rows) = await BuildExportDataAsync(type);
            var fileName = $"BaoCao_{type}_{DateTime.Now:yyyyMMdd_HHmmss}" + (format == "csv" ? ".csv" : ".xlsx");

            var bytes = format == "csv"
                ? _exportService.ExportCsv(sheetName, headers, rows)
                : _exportService.ExportExcel(sheetName, headers, rows);

            var contentType = format == "csv" ? "text/csv; charset=utf-8" : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            _logger.LogInformation("Xuất báo cáo {Type} định dạng {Format}", type, format);
            return File(bytes, contentType, fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Lỗi xuất báo cáo {Type}", type);
            TempData["Error"] = "Đã xảy ra lỗi khi xuất báo cáo.";
            return RedirectToAction(nameof(Index));
        }
    }

    private async Task<(string, List<string>, List<List<object>>)> BuildExportDataAsync(string type)
    {
        switch (type.ToLower())
        {
            case "rooms":
            {
                var data = await _reportService.GetRoomReportAsync();
                return ("DanhSachPhong", new List<string> { "Mã phòng", "Tên phòng", "Loại", "Diện tích", "Giá phòng", "Trạng thái" },
                    data.Select(r => new List<object> { r.MaPhongKyHieu, r.TenPhong, r.LoaiPhong ?? "", r.DienTich ?? 0, r.GiaPhong, r.TrangThai.GetDisplayName() }).ToList());
            }
            case "tenants":
            {
                var data = await _reportService.GetTenantReportAsync();
                return ("DanhSachNguoiThue", new List<string> { "Họ tên", "CCCD", "SĐT", "Phòng hiện tại", "Trạng thái" },
                    data.Select(t => new List<object> { t.HoTen, t.CCCD, t.SoDienThoai ?? "", t.PhongHienTai, t.TrangThai }).ToList());
            }
            case "contracts":
            {
                var data = await _reportService.GetContractReportAsync(null, null);
                return ("HopDong", new List<string> { "Mã HĐ", "Phòng", "Người thuê", "Bắt đầu", "Kết thúc", "Giá thuê", "Tiền cọc", "Trạng thái" },
                    data.Select(c => new List<object> { c.MaHopDongKyHieu, c.Room?.MaPhongKyHieu ?? "", c.Tenant?.HoTen ?? "", c.NgayBatDau, c.NgayKetThuc, c.GiaThue, c.TienCoc, c.TrangThai.GetDisplayName() }).ToList());
            }
            case "invoices":
            {
                var data = await _reportService.GetInvoiceReportAsync(null, null, null);
                return ("HoaDon", new List<string> { "Mã HĐ", "Phòng", "Kỳ", "Tiền phòng", "Tiền điện", "Tiền nước", "Tiền DV", "Tổng tiền", "Trạng thái" },
                    data.Select(i => new List<object> { i.MaHoaDonKyHieu, i.Room?.MaPhongKyHieu ?? "", i.KyHoaDon, i.TienPhong, i.TienDien, i.TienNuoc, i.TienDichVu, i.TongTien, i.TrangThaiThanhToan.GetDisplayName() }).ToList());
            }
            case "revenue":
            {
                var data = await _reportService.GetDoanhThuTheoThangAsync(null);
                return ("DoanhThu", new List<string> { "Tháng", "Doanh thu", "Công nợ" },
                    data.Select(d => new List<object> { d.Thang, d.DoanhThu, d.CongNo }).ToList());
            }
            case "utilities":
            {
                var data = await _reportService.GetUtilityReportAsync(null, null);
                return ("DienNuoc", new List<string> { "Kỳ", "Phòng", "Điện cũ", "Điện mới", "Số điện", "Tiền điện", "Nước cũ", "Nước mới", "Số nước", "Tiền nước" },
                    data.Select(u => new List<object> { u.KyGhi, u.MaPhong, u.ChiSoDienCu, u.ChiSoDienMoi, u.SoDienSuDung, u.TienDien, u.ChiSoNuocCu, u.ChiSoNuocMoi, u.SoNuocSuDung, u.TienNuoc }).ToList());
            }
            default:
                return ("BaoCao", new List<string> { "Dữ liệu" }, new List<List<object>>());
        }
    }
}
