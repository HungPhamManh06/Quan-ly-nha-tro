using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNhaTro.Models;

public class Invoice
{
    public int MaHoaDon { get; set; }

    [Required, StringLength(30)]
    [Display(Name = "Mã hóa đơn")]
    public string MaHoaDonKyHieu { get; set; } = null!;

    public int MaPhong { get; set; }
    public Room? Room { get; set; }

    public int? MaHopDong { get; set; }
    public Contract? Contract { get; set; }

    [Required, StringLength(7)]
    [Display(Name = "Kỳ hóa đơn")]
    public string KyHoaDon { get; set; } = null!;

    [Display(Name = "Ngày lập")]
    public DateTime NgayLap { get; set; } = DateTime.Now;

    [Column(TypeName = "decimal(18,0)")]
    [Display(Name = "Tiền phòng")]
    public decimal TienPhong { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    [Display(Name = "Tiền điện")]
    public decimal TienDien { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    [Display(Name = "Tiền nước")]
    public decimal TienNuoc { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    [Display(Name = "Tiền dịch vụ")]
    public decimal TienDichVu { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    [Display(Name = "Tổng tiền")]
    public decimal TongTien { get; set; }

    [Display(Name = "Hạn thanh toán")]
    public DateTime? HanThanhToan { get; set; }

    [Display(Name = "Ngày thanh toán")]
    public DateTime? NgayThanhToan { get; set; }

    [Display(Name = "Trạng thái")]
    public InvoicePaymentStatus TrangThaiThanhToan { get; set; } = InvoicePaymentStatus.ChuaThanhToan;

    [StringLength(500)]
    [Display(Name = "Ghi chú")]
    public string? GhiChu { get; set; }

    public Payment? Payment { get; set; }
}
