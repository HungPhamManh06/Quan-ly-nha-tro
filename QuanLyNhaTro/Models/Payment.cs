using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNhaTro.Models;

public class Payment
{
    public int MaThanhToan { get; set; }

    public int MaHoaDon { get; set; }
    public Invoice? Invoice { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    [Display(Name = "Số tiền")]
    public decimal SoTien { get; set; }

    [Display(Name = "Ngày thanh toán")]
    public DateTime NgayThanhToan { get; set; } = DateTime.Now;

    [Display(Name = "Phương thức")]
    public PaymentMethod PhuongThuc { get; set; } = PaymentMethod.TienMat;

    [StringLength(500)]
    [Display(Name = "Ghi chú")]
    public string? GhiChu { get; set; }
}
