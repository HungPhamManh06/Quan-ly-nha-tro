using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyNhaTro.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger) => _logger = logger;

    [AllowAnonymous]
    public IActionResult Index() => RedirectToAction("Index", "Dashboard");

    [AllowAnonymous]
    public IActionResult AccessDenied() => View();

    [AllowAnonymous]
    public IActionResult Error()
    {
        // Không hiển thị stack trace cho người dùng cuối
        return View();
    }

    [AllowAnonymous]
    public IActionResult Error404() => View();
}
