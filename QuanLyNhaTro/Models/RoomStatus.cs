using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaTro.Models;

public enum RoomStatus
{
    [Display(Name = "Trống")] Trong = 1,
    [Display(Name = "Đã cọc")] DaCoc = 2,
    [Display(Name = "Đang thuê")] DangThue = 3,
    [Display(Name = "Đang sửa chữa")] DangSuaChua = 4
}
