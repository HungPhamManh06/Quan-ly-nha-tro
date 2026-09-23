using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTroDesktop.Helpers;

/// <summary>
/// Xác thực người dùng cho bản desktop: dùng PasswordHasher của ASP.NET Core Identity
/// để kiểm tra mật khẩu khớp với hash lưu trong bảng Users.
/// </summary>
public static class PasswordHelper
{
    public static async Task<bool> CheckPasswordAsync(string userName, string password)
    {
        var db = Program.Services.GetRequiredService<ApplicationDbContext>();
        var hasher = new PasswordHasher<AppUser>();

        var user = await db.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        if (user == null) return false;

        var result = hasher.VerifyHashedPassword(user, user.PasswordHash ?? "", password);
        return result == PasswordVerificationResult.Success
            || result == PasswordVerificationResult.SuccessRehashNeeded;
    }
}
