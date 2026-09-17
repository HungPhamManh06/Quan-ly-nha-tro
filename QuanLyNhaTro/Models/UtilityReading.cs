using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNhaTro.Models;

public class UtilityReading
{
    public int MaGhiChiSo { get; set; }

    public int MaPhong { get; set; }
    public Room? Room { get; set; }

    [Required, StringLength(7)]
    [Display(Name = "Kỳ ghi")]
    public string KyGhi { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Chỉ số điện cũ")]
    public decimal ChiSoDienCu { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Range(0, 99_999_999, ErrorMessage = "Chỉ số điện mới phải lớn hơn hoặc bằng chỉ số cũ")]
    [Display(Name = "Chỉ số điện mới")]
    public decimal ChiSoDienMoi { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    [Display(Name = "Đơn giá điện")]
    public decimal DonGiaDien { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Chỉ số nước cũ")]
    public decimal ChiSoNuocCu { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Range(0, 99_999_999, ErrorMessage = "Chỉ số nước mới phải lớn hơn hoặc bằng chỉ số cũ")]
    [Display(Name = "Chỉ số nước mới")]
    public decimal ChiSoNuocMoi { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    [Display(Name = "Đơn giá nước")]
    public decimal DonGiaNuoc { get; set; }

    [StringLength(500)]
    [Display(Name = "Ghi chú")]
    public string? GhiChu { get; set; }

    [NotMapped]
    [Display(Name = "Số điện sử dụng")]
    public decimal SoDienSuDung => ChiSoDienMoi - ChiSoDienCu;

    [NotMapped]
    [Display(Name = "Số nước sử dụng")]
    public decimal SoNuocSuDung => ChiSoNuocMoi - ChiSoNuocCu;

    [NotMapped]
    [Display(Name = "Tiền điện")]
    public decimal TienDien => SoDienSuDung * DonGiaDien;

    [NotMapped]
    [Display(Name = "Tiền nước")]
    public decimal TienNuoc => SoNuocSuDung * DonGiaNuoc;
}
