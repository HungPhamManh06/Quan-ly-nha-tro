using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaTro.Models;

public class Tenant
{
    public int MaNguoiThue { get; set; }

    [Required(ErrorMessage = "Họ tên không được để trống")]
    [StringLength(100)]
    [Display(Name = "Họ tên")]
    public string HoTen { get; set; } = null!;

    [Required(ErrorMessage = "CCCD không được để trống")]
    [StringLength(12, MinimumLength = 9, ErrorMessage = "CCCD phải có 9-12 ký tự")]
    [RegularExpression(@"^\d{9,12}$", ErrorMessage = "CCCD chỉ gồm chữ số")]
    [Display(Name = "CCCD")]
    public string CCCD { get; set; } = null!;

    [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
    [StringLength(15)]
    [Display(Name = "Số điện thoại")]
    public string? SoDienThoai { get; set; }

    [StringLength(200)]
    [Display(Name = "Quê quán")]
    public string? QueQuan { get; set; }

    [StringLength(20)]
    [Display(Name = "Trạng thái")]
    public string TrangThai { get; set; } = "Đang thuê";

    [StringLength(500)]
    [Display(Name = "Ghi chú")]
    public string? GhiChu { get; set; }

    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
