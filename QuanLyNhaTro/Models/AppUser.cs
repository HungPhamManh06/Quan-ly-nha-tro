using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaTro.Models;

/// <summary>Tài khoản Chủ trọ (người dùng duy nhất của hệ thống).</summary>
public class AppUser : IdentityUser
{
    [StringLength(150)]
    [Display(Name = "Họ tên")]
    public string? HoTen { get; set; }
}
