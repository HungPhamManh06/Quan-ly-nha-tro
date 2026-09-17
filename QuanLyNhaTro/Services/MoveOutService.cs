using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.ViewModels;

namespace QuanLyNhaTro.Services;

public interface IMoveOutService
{
    Task<List<Contract>> GetLiquidatableContractsAsync();
    Task<MoveOutViewModel?> BuildWizardAsync(int contractId);
    Task<(bool Ok, string Message)> ChotDienNuocCuoiKyAsync(int contractId, decimal dienMoi, decimal nuocMoi, decimal donGiaDien, decimal donGiaNuoc);
    Task<(bool Ok, string Message)> ChotDichVuCuoiKyAsync(int contractId);
    Task<MoveOutPreviewViewModel> PreviewAsync(MoveOutViewModel vm);
    Task<(bool Ok, string Message)> SettleAsync(MoveOutViewModel vm);
    Task<MoveOut?> GetByContractAsync(int contractId);
}

public class MoveOutService : IMoveOutService
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<MoveOutService> _logger;

    public MoveOutService(ApplicationDbContext db, ILogger<MoveOutService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<Contract>> GetLiquidatableContractsAsync()
    {
        var today = DateTime.Today;
        return await _db.Contracts.AsNoTracking()
            .Include(c => c.Room).Include(c => c.Tenant)
            .Where(c => c.TrangThai != ContractStatus.DaThanhLy && c.MoveOut == null)
            .OrderBy(c => c.Room!.MaPhongKyHieu)
            .ToListAsync();
    }

    public async Task<MoveOutViewModel?> BuildWizardAsync(int contractId)
    {
        var contract = await _db.Contracts
            .Include(c => c.Room).Include(c => c.Tenant)
            .FirstOrDefaultAsync(c => c.MaHopDong == contractId);
        if (contract == null) return null;

        var vm = new MoveOutViewModel
        {
            MaHopDong = contract.MaHopDong,
            TenPhong = contract.Room?.MaPhongKyHieu ?? "",
            TenNguoiThue = contract.Tenant?.HoTen ?? "",
            GiaThue = contract.GiaThue,
            TienCoc = contract.TienCoc,
            NgayBatDau = contract.NgayBatDau,
            NgayKetThuc = contract.NgayKetThuc,
            NgayTraPhong = DateTime.Today
        };

        // Công nợ: các hóa đơn CHƯA thanh toán của phòng
        var hoaDonChuaTT = await _db.Invoices.Include(i => i.Room)
            .Where(i => i.MaPhong == contract.MaPhong && i.TrangThaiThanhToan == InvoicePaymentStatus.ChuaThanhToan)
            .OrderBy(i => i.KyHoaDon).ToListAsync();
        vm.HoaDonChuaThanhToan = hoaDonChuaTT.Select(i => new InvoiceItemViewModel
        {
            MaHoaDon = i.MaHoaDon,
            MaHoaDonKyHieu = i.MaHoaDonKyHieu,
            KyHoaDon = i.KyHoaDon,
            TongTien = i.TongTien
        }).ToList();
        vm.CongNoTruocTraPhong = hoaDonChuaTT.Sum(i => i.TongTien);

        // Chốt điện nước cuối kỳ: lấy kỳ lớn nhất
        var latestReading = await _db.UtilityReadings
            .Where(u => u.MaPhong == contract.MaPhong)
            .OrderByDescending(u => u.KyGhi).FirstOrDefaultAsync();
        if (latestReading != null)
        {
            vm.KyDienNuocCuoi = latestReading.KyGhi;
            vm.ChiSoDienCu = latestReading.ChiSoDienMoi; // chỉ số mới kỳ trước = chỉ số cũ kỳ chốt
            vm.ChiSoNuocCu = latestReading.ChiSoNuocMoi;
            vm.DonGiaDien = latestReading.DonGiaDien;
            vm.DonGiaNuoc = latestReading.DonGiaNuoc;
        }

        // Chốt dịch vụ cuối kỳ
        var latestUsageKy = await _db.ServiceUsages
            .Where(su => su.MaPhong == contract.MaPhong)
            .OrderByDescending(su => su.KySuDung)
            .Select(su => su.KySuDung).FirstOrDefaultAsync();
        if (latestUsageKy != null)
        {
            var usages = await _db.ServiceUsages.Include(su => su.Service)
                .Where(su => su.MaPhong == contract.MaPhong && su.KySuDung == latestUsageKy).ToListAsync();
            vm.KyDichVuCuoi = latestUsageKy;
            vm.DichVuCuoiKy = usages.Select(su => new ServiceUsageItemViewModel
            {
                MaSuDung = su.MaSuDung,
                TenDichVu = su.Service?.TenDichVu ?? "",
                SoLuong = su.SoLuong,
                DonViTinh = su.Service?.DonViTinh ?? "",
                DonGiaApDung = su.DonGiaApDung,
                ThanhTien = su.ThanhTien,
                DaTinhVaoQuyetToan = true
            }).ToList();
        }

        return vm;
    }

    public async Task<(bool Ok, string Message)> ChotDienNuocCuoiKyAsync(int contractId, decimal dienMoi, decimal nuocMoi, decimal donGiaDien, decimal donGiaNuoc)
    {
        var contract = await _db.Contracts.FirstOrDefaultAsync(c => c.MaHopDong == contractId);
        if (contract == null) return (false, "Không tìm thấy hợp đồng.");
        if (dienMoi < 0 || nuocMoi < 0) return (false, "Chỉ số phải lớn hơn hoặc bằng 0.");

        var ky = $"TRAPHONG-{contract.MaHopDong}";
        var latest = await _db.UtilityReadings.Where(u => u.MaPhong == contract.MaPhong).OrderByDescending(u => u.KyGhi).FirstOrDefaultAsync();

        // Chỉ số mới >= chỉ số cũ
        if (latest != null)
        {
            if (dienMoi < latest.ChiSoDienMoi) return (false, "Không thể chốt vì chỉ số điện mới nhỏ hơn chỉ số cũ.");
            if (nuocMoi < latest.ChiSoNuocMoi) return (false, "Không thể chốt vì chỉ số nước mới nhỏ hơn chỉ số cũ.");
        }

        // Mỗi phòng + kỳ một bản ghi: nếu kỳ chốt đã tồn tại thì cập nhật
        var existed = await _db.UtilityReadings.FirstOrDefaultAsync(u => u.MaPhong == contract.MaPhong && u.KyGhi == ky);
        if (existed != null)
        {
            existed.ChiSoDienCu = latest?.ChiSoDienMoi ?? 0;
            existed.ChiSoDienMoi = dienMoi;
            existed.DonGiaDien = donGiaDien;
            existed.ChiSoNuocCu = latest?.ChiSoNuocMoi ?? 0;
            existed.ChiSoNuocMoi = nuocMoi;
            existed.DonGiaNuoc = donGiaNuoc;
            existed.GhiChu = "Chốt chỉ số khi trả phòng";
        }
        else
        {
            _db.UtilityReadings.Add(new UtilityReading
            {
                MaPhong = contract.MaPhong,
                KyGhi = ky,
                ChiSoDienCu = latest?.ChiSoDienMoi ?? 0,
                ChiSoDienMoi = dienMoi,
                DonGiaDien = donGiaDien,
                ChiSoNuocCu = latest?.ChiSoNuocMoi ?? 0,
                ChiSoNuocMoi = nuocMoi,
                DonGiaNuoc = donGiaNuoc,
                GhiChu = "Chốt chỉ số khi trả phòng"
            });
        }
        await _db.SaveChangesAsync();
        return (true, "Đã chốt chỉ số điện nước cuối kỳ.");
    }

    public async Task<(bool Ok, string Message)> ChotDichVuCuoiKyAsync(int contractId)
    {
        var contract = await _db.Contracts.FirstOrDefaultAsync(c => c.MaHopDong == contractId);
        if (contract == null) return (false, "Không tìm thấy hợp đồng.");
        var ky = $"TRAPHONG-{contract.MaHopDong}";
        var count = await _db.ServiceUsages.CountAsync(su => su.MaPhong == contract.MaPhong && su.KySuDung == ky);
        return (true, $"Kỳ chốt dịch vụ '{ky}' hiện có {count} bản ghi sử dụng dịch vụ.");
    }

    public Task<MoveOutPreviewViewModel> PreviewAsync(MoveOutViewModel vm)
    {
        var tienPhatSinh = vm.TienPhatSinhCuoiKy;
        var khoanPhaiThu = vm.CongNoTruocTraPhong + tienPhatSinh;

        // Công thức đặc tả:
        // TienKhauTruCoc = MIN(TienCocBanDau, KhoanPhaiThuCuoiCung)
        // TienHoanCoc    = MAX(0, TienCocBanDau - KhoanPhaiThuCuoiCung)
        // TienPhaiTraThem= MAX(0, KhoanPhaiThuCuoiCung - TienCocBanDau)
        var khauTru = Math.Min(vm.TienCoc, khoanPhaiThu);
        var hoanCoc = Math.Max(0, vm.TienCoc - khoanPhaiThu);
        var traThem = Math.Max(0, khoanPhaiThu - vm.TienCoc);

        return Task.FromResult(new MoveOutPreviewViewModel
        {
            CongNoTruocTraPhong = vm.CongNoTruocTraPhong,
            TienPhatSinhCuoiKy = tienPhatSinh,
            KhoanPhaiThuCuoiCung = khoanPhaiThu,
            TienCocBanDau = vm.TienCoc,
            TienKhauTruCoc = khauTru,
            TienHoanCoc = hoanCoc,
            TienPhaiTraThem = traThem
        });
    }

    public async Task<(bool Ok, string Message)> SettleAsync(MoveOutViewModel vm)
    {
        var contract = await _db.Contracts
            .Include(c => c.Room).Include(c => c.Tenant)
            .FirstOrDefaultAsync(c => c.MaHopDong == vm.MaHopDong);
        if (contract == null) return (false, "Không tìm thấy hợp đồng.");

        // Quyết toán trả phòng CHỈ MỘT lần duy nhất
        if (contract.TrangThai == ContractStatus.DaThanhLy || await _db.MoveOuts.AnyAsync(m => m.MaHopDong == vm.MaHopDong))
            return (false, "Hợp đồng này đã được quyết toán trả phòng trước đó.");

        var room = contract.Room!;

        // Tính lại toàn bộ số tiền phía server từ dữ liệu DB, không tin client
        var hoaDonChuaTT = await _db.Invoices
            .Where(i => i.MaPhong == room.MaPhong && i.TrangThaiThanhToan == InvoicePaymentStatus.ChuaThanhToan)
            .Select(i => i.TongTien).ToListAsync();
        var congNo = hoaDonChuaTT.Sum();

        var kyChot = $"TRAPHONG-{contract.MaHopDong}";
        var readingChot = await _db.UtilityReadings.FirstOrDefaultAsync(u => u.MaPhong == room.MaPhong && u.KyGhi == kyChot);
        var tienDienNuocChot = (readingChot?.TienDien ?? 0) + (readingChot?.TienNuoc ?? 0);

        var dichVuChotList = await _db.ServiceUsages
            .Where(su => su.MaPhong == room.MaPhong && su.KySuDung == kyChot)
            .Select(su => su.ThanhTien).ToListAsync();
        var dichVuChot = dichVuChotList.Sum();

        var tienPhatSinh = tienDienNuocChot + dichVuChot;
        var khoanPhaiThu = congNo + tienPhatSinh;
        var tienCoc = contract.TienCoc;

        var khauTru = Math.Min(tienCoc, khoanPhaiThu);
        var hoanCoc = Math.Max(0, tienCoc - khoanPhaiThu);
        var traThem = Math.Max(0, khoanPhaiThu - tienCoc);

        // Transaction: MoveOut + thanh lý hợp đồng + cập nhật phòng phải nguyên vẹn
        await using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            var moveOut = new MoveOut
            {
                MaHopDong = contract.MaHopDong,
                NgayTraPhong = vm.NgayTraPhong,
                CongNoTruocTraPhong = congNo,
                TienPhatSinhCuoiKy = tienPhatSinh,
                KhoanPhaiThuCuoiCung = khoanPhaiThu,
                TienCocBanDau = tienCoc,
                TienKhauTruCoc = khauTru,
                TienHoanCoc = hoanCoc,
                TienPhaiTraThem = traThem,
                DaQuyetToan = true,
                TrangThaiPhongSauTra = vm.TrangThaiPhongSauTra,
                GhiChu = vm.GhiChu
            };
            _db.MoveOuts.Add(moveOut);

            // Thanh lý hợp đồng
            contract.TrangThai = ContractStatus.DaThanhLy;
            _db.ContractHistories.Add(new ContractHistory
            {
                MaHopDong = contract.MaHopDong,
                HanhDong = "Thanh lý (trả phòng)",
                NoiDung = $"Quyết toán trả phòng ngày {vm.NgayTraPhong:dd/MM/yyyy}. Khoản phải thu cuối cùng {khoanPhaiThu:N0} đ.",
                ThoiGian = DateTime.Now
            });

            // Cập nhật trạng thái phòng
            room.TrangThai = vm.TrangThaiPhongSauTra;

            // Người thuê hết phòng hiện tại
            if (contract.Tenant != null) contract.Tenant.TrangThai = "Đã trả phòng";

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            _logger.LogInformation("Quyết toán trả phòng hợp đồng {MaHopDong}, hoàn cọc {HoanCoc}, phải trả thêm {TraThem}",
                contract.MaHopDongKyHieu, hoanCoc, traThem);

            var msg = $"Trả phòng thành công. Khoản phải thu: {khoanPhaiThu:N0} đ (công nợ {congNo:N0} + phát sinh {tienPhatSinh:N0}). ";
            if (hoanCoc > 0) msg += $"Hoàn lại cọc {hoanCoc:N0} đ.";
            else if (traThem > 0) msg += $"Người thuê phải trả thêm {traThem:N0} đ.";
            else msg += "Tiền cọc đã khấu trừ đủ khoản phải thu.";
            return (true, msg);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Lỗi quyết toán trả phòng hợp đồng {MaHopDong}", vm.MaHopDong);
            return (false, "Đã xảy ra lỗi khi quyết toán trả phòng. Thao tác đã được hoàn nguyên.");
        }
    }

    public Task<MoveOut?> GetByContractAsync(int contractId) =>
        _db.MoveOuts.Include(m => m.Contract).FirstOrDefaultAsync(m => m.MaHopDong == contractId);
}
