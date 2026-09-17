using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNhaTro.Models;

public class Contract
{
    public int MaHopDong { get; set; }

    [Required, StringLength(20)]
    [Display(Name = "Mã hợp đồng")]
    public string MaHopDongKyHieu { get; set; } = null!;

    [Display(Name = "Phòng")]
    public int MaPhong { get; set; }
    public Room? Room { get; set; }

    [Display(Name = "Người thuê")]
    public int MaNguoiThue { get; set; }
    public Tenant? Tenant { get; set; }

    [Display(Name = "Ngày bắt đầu")]
    public DateTime NgayBatDau { get; set; }

    [Display(Name = "Ngày kết thúc")]
    public DateTime NgayKetThuc { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    [Range(1, 999_999_999, ErrorMessage = "Giá thuê phải lớn hơn 0")]
    [Display(Name = "Giá thuê")]
    public decimal GiaThue { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    [Range(0, 999_999_999, ErrorMessage = "Tiền cọc phải lớn hơn hoặc bằng 0")]
    [Display(Name = "Tiền cọc")]
    public decimal TienCoc { get; set; }

    [Display(Name = "Trạng thái")]
    public ContractStatus TrangThai { get; set; } = ContractStatus.HieuLuc;

    [StringLength(500)]
    [Display(Name = "Ghi chú")]
    public string? GhiChu { get; set; }

    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    public MoveOut? MoveOut { get; set; }
    public ICollection<ContractHistory> Histories { get; set; } = new List<ContractHistory>();

    public bool ConHieuLuc(DateTime ngay) => TrangThai != ContractStatus.DaThanhLy && ngay >= NgayBatDau && ngay <= NgayKetThuc;
}
