using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNhaTro.Models;

public class MoveOut
{
    public int MaTraPhong { get; set; }

    public int MaHopDong { get; set; }
    public Contract? Contract { get; set; }

    [Display(Name = "Ngày trả phòng")]
    public DateTime NgayTraPhong { get; set; } = DateTime.Now;

    [Column(TypeName = "decimal(18,0)")]
    [Display(Name = "Công nợ trước trả phòng")]
    public decimal CongNoTruocTraPhong { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    [Display(Name = "Tiền phát sinh cuối kỳ")]
    public decimal TienPhatSinhCuoiKy { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    [Display(Name = "Khoản phải thu cuối cùng")]
    public decimal KhoanPhaiThuCuoiCung { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    [Display(Name = "Tiền cọc ban đầu")]
    public decimal TienCocBanDau { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    [Display(Name = "Tiền khấu trừ cọc")]
    public decimal TienKhauTruCoc { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    [Display(Name = "Tiền hoàn cọc")]
    public decimal TienHoanCoc { get; set; }

    [Column(TypeName = "decimal(18,0)")]
    [Display(Name = "Tiền phải trả thêm")]
    public decimal TienPhaiTraThem { get; set; }

    [Display(Name = "Đã quyết toán")]
    public bool DaQuyetToan { get; set; }

    [Display(Name = "Phòng sau trả")]
    public RoomStatus TrangThaiPhongSauTra { get; set; } = RoomStatus.Trong;

    [StringLength(500)]
    [Display(Name = "Ghi chú")]
    public string? GhiChu { get; set; }

}
