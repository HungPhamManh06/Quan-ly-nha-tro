using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNhaTro.Models;

public class Room
{
    public int MaPhong { get; set; }

    [Required(ErrorMessage = "Mã phòng không được để trống")]
    [StringLength(20)]
    [Display(Name = "Mã phòng")]
    public string MaPhongKyHieu { get; set; } = null!; // mã hiển thị, vd P101

    [Required(ErrorMessage = "Tên phòng không được để trống")]
    [StringLength(50)]
    [Display(Name = "Tên phòng")]
    public string TenPhong { get; set; } = null!;

    [Column(TypeName = "decimal(10,2)")]
    [Display(Name = "Diện tích (m²)")]
    public decimal? DienTich { get; set; }

    [StringLength(50)]
    [Display(Name = "Loại phòng")]
    public string? LoaiPhong { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    [Range(0, 999_999_999, ErrorMessage = "Giá phòng phải lớn hơn hoặc bằng 0")]
    [Display(Name = "Giá phòng")]
    public decimal GiaPhong { get; set; }

    [Display(Name = "Trạng thái")]
    public RoomStatus TrangThai { get; set; } = RoomStatus.Trong;

    [StringLength(500)]
    [Display(Name = "Ghi chú")]
    public string? GhiChu { get; set; }

    public bool DaXoa { get; set; }

    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    public ICollection<UtilityReading> UtilityReadings { get; set; } = new List<UtilityReading>();
    public ICollection<ServiceUsage> ServiceUsages { get; set; } = new List<ServiceUsage>();
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
