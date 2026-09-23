using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNhaTro.Models;

public class Service
{
    public int MaDichVu { get; set; }

    [Required, StringLength(50)]
    [Display(Name = "Tên dịch vụ")]
    public string TenDichVu { get; set; } = null!;

    [Required, StringLength(20)]
    [Display(Name = "Đơn vị tính")]
    public string DonViTinh { get; set; } = null!;

    [Column(TypeName = "decimal(18,0)")]
    [Range(0, 999_999_999)]
    [Display(Name = "Đơn giá")]
    public decimal DonGia { get; set; }

    [Display(Name = "Trạng thái")]
    public bool TrangThai { get; set; } = true;

    public ICollection<ServiceUsage> ServiceUsages { get; set; } = new List<ServiceUsage>();
}

public class ServiceUsage
{
    public int MaSuDung { get; set; }

    public int MaPhong { get; set; }
    public Room? Room { get; set; }

    public int MaDichVu { get; set; }
    public Service? Service { get; set; }

    // 20 ký tự vì kỳ chốt dịch vụ cuối cùng khi trả phòng có dạng "TRAPHONG-{mã hợp đồng}"
    [Required, StringLength(20)]
    [Display(Name = "Kỳ sử dụng")]
    public string KySuDung { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, 1_000_000, ErrorMessage = "Số lượng phải lớn hơn 0")]
    [Display(Name = "Số lượng")]
    public decimal SoLuong { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    [Display(Name = "Đơn giá áp dụng")]
    public decimal DonGiaApDung { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    [Display(Name = "Thành tiền")]
    public decimal ThanhTien { get; set; }

    [StringLength(500)]
    [Display(Name = "Ghi chú")]
    public string? GhiChu { get; set; }
}
