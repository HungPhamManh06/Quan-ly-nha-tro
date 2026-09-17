using System.ComponentModel.DataAnnotations;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
    [Display(Name = "Tên đăng nhập")]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; } = null!;

    [Display(Name = "Ghi nhớ đăng nhập")]
    public bool RememberMe { get; set; }
}

// Khoản mục cần xử lý trên Dashboard (dữ liệu thật, chỉ đọc)
public class AttentionItemViewModel
{
    public string Loai { get; set; } = "";          // "hoadon" | "hopdong" | "phong"
    public string MaSo { get; set; } = "";          // mã phòng / mã hóa đơn / mã hợp đồng
    public string TieuDe { get; set; } = "";        // dòng đầu hiển thị
    public string MoTa { get; set; } = "";          // dòng phụ
    public decimal SoTien { get; set; }             // nếu có (hóa đơn)
    public string? TrangThai { get; set; }          // nhãn trạng thái
    public string Url { get; set; } = "";           // link xem chi tiết
}

public class ChartItemViewModel
{
    public string Label { get; set; }
    public decimal GiaTri1 { get; set; }
    public decimal GiaTri2 { get; set; }

    public ChartItemViewModel(string label, decimal giaTri1, decimal giaTri2 = 0)
    {
        Label = label;
        GiaTri1 = giaTri1;
        GiaTri2 = giaTri2;
    }
}

public class DashboardViewModel
{
    public int TongSoPhong { get; set; }
    public int PhongTrong { get; set; }
    public int PhongDaCoc { get; set; }
    public int PhongDangThue { get; set; }
    public int PhongDangSuaChua { get; set; }

    public int NguoiThueDangO { get; set; }

    public int HoaDonChuaThanhToan { get; set; }
    public int HoaDonDaThanhToan { get; set; }

    public decimal DoanhThu { get; set; }
    public decimal CongNo { get; set; }

    public int HopDongHieuLuc { get; set; }
    public int HopDongSapHetHan { get; set; }

    public List<ChartItemViewModel> TrangThaiPhongChart { get; set; } = new();
    public List<ChartItemViewModel> DoanhThuChart { get; set; } = new();
    public List<ChartItemViewModel> CongNoChart { get; set; } = new();
    public List<ChartItemViewModel> DienNuocChart { get; set; } = new();
    public List<string> Thangs { get; set; } = new();

    // Doanh thu tháng này và tháng trước để tính xu hướng
    public decimal DoanhThuThangNay { get; set; }
    public decimal DoanhThuThangTruoc { get; set; }

    // Section "Cần xử lý" (tối đa 6 khoản mục ưu tiên)
    public List<AttentionItemViewModel> CanXuLy { get; set; } = new();
}

public class InvoiceCreateViewModel
{
    [Required(ErrorMessage = "Vui lòng chọn phòng")]
    [Display(Name = "Phòng")]
    public int MaPhong { get; set; }

    [Display(Name = "Hợp đồng")]
    public int? MaHopDong { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập kỳ hóa đơn (định dạng yyyy-MM)")]
    [RegularExpression(@"^\d{4}-\d{2}$", ErrorMessage = "Kỳ phải theo định dạng yyyy-MM, ví dụ 2026-01")]
    [Display(Name = "Kỳ hóa đơn")]
    public string KyHoaDon { get; set; } = null!;

    [Display(Name = "Tiền phòng")]
    public decimal TienPhong { get; set; }

    [Display(Name = "Tiền điện")]
    public decimal TienDien { get; set; }

    [Display(Name = "Tiền nước")]
    public decimal TienNuoc { get; set; }

    [Display(Name = "Tiền dịch vụ")]
    public decimal TienDichVu { get; set; }

    [Display(Name = "Tổng tiền")]
    public decimal TongTien => TienPhong + TienDien + TienNuoc + TienDichVu;

    [Display(Name = "Hạn thanh toán")]
    public DateTime? HanThanhToan { get; set; }

    [StringLength(500)]
    [Display(Name = "Ghi chú")]
    public string? GhiChu { get; set; }

    public List<Room> Rooms { get; set; } = new();
    public string? TenPhong { get; set; }
    public string? ChiTietDienNuoc { get; set; }
    public string? ChiTietDichVu { get; set; }
}

public class PaymentViewModel
{
    public int MaHoaDon { get; set; }
    public string MaHoaDonKyHieu { get; set; } = null!;
    public string TenPhong { get; set; } = null!;
    public string KyHoaDon { get; set; } = null!;

    [Display(Name = "Tổng tiền hóa đơn")]
    public decimal TongTien { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số tiền thanh toán")]
    [Range(0.01, 999_999_999_999, ErrorMessage = "Số tiền phải lớn hơn 0")]
    [Display(Name = "Số tiền khách trả")]
    public decimal SoTien { get; set; }

    [Display(Name = "Ngày thanh toán")]
    public DateTime? NgayThanhToan { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn phương thức")]
    [Display(Name = "Phương thức")]
    public PaymentMethod PhuongThuc { get; set; } = PaymentMethod.TienMat;

    [StringLength(500)]
    [Display(Name = "Ghi chú")]
    public string? GhiChu { get; set; }

    public InvoicePaymentStatus TrangThaiThanhToan { get; set; }
}

public class InvoiceItemViewModel
{
    public int MaHoaDon { get; set; }
    public string MaHoaDonKyHieu { get; set; } = null!;
    public string KyHoaDon { get; set; } = null!;
    public decimal TongTien { get; set; }
}

public class ServiceUsageItemViewModel
{
    public int MaSuDung { get; set; }
    public string TenDichVu { get; set; } = null!;
    public decimal SoLuong { get; set; }
    public string DonViTinh { get; set; } = null!;
    public decimal DonGiaApDung { get; set; }
    public decimal ThanhTien { get; set; }
    public bool DaTinhVaoQuyetToan { get; set; }
}

public class MoveOutViewModel
{
    public int MaHopDong { get; set; }
    public string TenPhong { get; set; } = null!;
    public string TenNguoiThue { get; set; } = null!;
    public decimal GiaThue { get; set; }
    public decimal TienCoc { get; set; }
    public DateTime NgayBatDau { get; set; }
    public DateTime NgayKetThuc { get; set; }

    [Display(Name = "Ngày trả phòng")]
    public DateTime NgayTraPhong { get; set; } = DateTime.Today;

    public decimal CongNoTruocTraPhong { get; set; }
    public List<InvoiceItemViewModel> HoaDonChuaThanhToan { get; set; } = new();

    // Chốt điện nước cuối kỳ
    public string? KyDienNuocCuoi { get; set; }
    public decimal ChiSoDienCu { get; set; }
    public decimal ChiSoDienMoi { get; set; }
    public decimal DonGiaDien { get; set; }
    public decimal ChiSoNuocCu { get; set; }
    public decimal ChiSoNuocMoi { get; set; }
    public decimal DonGiaNuoc { get; set; }

    // Chốt dịch vụ cuối kỳ
    public string? KyDichVuCuoi { get; set; }
    public List<ServiceUsageItemViewModel> DichVuCuoiKy { get; set; } = new();

    [Display(Name = "Tiền phát sinh cuối kỳ khác")]
    public decimal TienPhatSinhCuoiKy { get; set; }

    [Display(Name = "Trạng thái phòng sau trả")]
    public RoomStatus TrangThaiPhongSauTra { get; set; } = RoomStatus.Trong;

    [StringLength(500)]
    [Display(Name = "Ghi chú")]
    public string? GhiChu { get; set; }
}

public class MoveOutPreviewViewModel
{
    public decimal CongNoTruocTraPhong { get; set; }
    public decimal TienPhatSinhCuoiKy { get; set; }
    public decimal KhoanPhaiThuCuoiCung { get; set; }
    public decimal TienCocBanDau { get; set; }
    public decimal TienKhauTruCoc { get; set; }
    public decimal TienHoanCoc { get; set; }
    public decimal TienPhaiTraThem { get; set; }
}

public class RenewContractViewModel
{
    public int MaHopDong { get; set; }
    public string MaHopDongKyHieu { get; set; } = null!;
    public string TenPhong { get; set; } = null!;
    public string TenNguoiThue { get; set; } = null!;
    public DateTime NgayBatDau { get; set; }
    public DateTime NgayKetThuc { get; set; }
    public decimal GiaThue { get; set; }
    public decimal TienCoc { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn ngày kết thúc mới")]
    [Display(Name = "Ngày kết thúc mới")]
    public DateTime NgayKetThucMoi { get; set; }

    [Display(Name = "Giá thuê mới (nếu thay đổi)")]
    public decimal? GiaThueMoi { get; set; }

    [Display(Name = "Tiền cọc mới (nếu thay đổi)")]
    public decimal? TienCocMoi { get; set; }

    [StringLength(500)]
    [Display(Name = "Ghi chú")]
    public string? GhiChu { get; set; }
}

// ---- Báo cáo ----
public class TenantReportItem
{
    public int MaNguoiThue { get; set; }
    public string HoTen { get; set; } = null!;
    public string CCCD { get; set; } = null!;
    public string? SoDienThoai { get; set; }
    public string TrangThai { get; set; } = null!;
    public string PhongHienTai { get; set; } = "";
}

public class UtilityReportItem
{
    public string KyGhi { get; set; } = null!;
    public string MaPhong { get; set; } = null!;
    public decimal ChiSoDienCu { get; set; }
    public decimal ChiSoDienMoi { get; set; }
    public decimal SoDienSuDung { get; set; }
    public decimal TienDien { get; set; }
    public decimal ChiSoNuocCu { get; set; }
    public decimal ChiSoNuocMoi { get; set; }
    public decimal SoNuocSuDung { get; set; }
    public decimal TienNuoc { get; set; }
}

public class DoanhThuThangItem
{
    public string Thang { get; set; } = null!;
    public decimal DoanhThu { get; set; }
    public decimal CongNo { get; set; }
}

public class ReportFinancialViewModel
{
    public string? TuKy { get; set; }
    public string? DenKy { get; set; }
    public decimal TongDoanhThu { get; set; }
    public decimal TongCongNo { get; set; }
    public int SoHoaDonDaTT { get; set; }
    public int SoHoaDonChuaTT { get; set; }
}
