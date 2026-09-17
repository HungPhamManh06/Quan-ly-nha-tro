using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;

namespace QuanLyNhaTro.Controllers;

[Authorize]
public class ServicesController : Controller
{
    private readonly IServiceService _serviceService;
    private readonly IRoomService _roomService;

    public ServicesController(IServiceService serviceService, IRoomService roomService)
    {
        _serviceService = serviceService;
        _roomService = roomService;
    }

    private void SetAlert(bool ok, string message) => TempData[ok ? "Success" : "Error"] = message;

    // GET: /Services — trang gộp: danh sách dịch vụ + bản ghi sử dụng
    public async Task<IActionResult> Index(string? search, string? ky)
    {
        var services = await _serviceService.GetServicesAsync(search);
        var kySelected = ky ?? await _serviceService.GenerateKySuDungAsync();
        var usages = await _serviceService.GetUsagesAsync(kySelected);
        ViewBag.Search = search;
        ViewBag.Ky = kySelected;
        ViewBag.Services = services;
        ViewBag.Usages = usages;
        return View();
    }

    // ---- Dịch vụ ----
    public IActionResult CreateService() => View(new Service { TrangThai = true });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateService(Service service)
    {
        if (!ModelState.IsValid) return View(service);
        var (ok, message) = await _serviceService.CreateServiceAsync(service);
        SetAlert(ok, message);
        if (!ok) return View(service);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> EditService(int id)
    {
        var service = await _serviceService.GetServiceByIdAsync(id);
        if (service == null) return NotFound();
        return View(service);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditService(int id, Service service)
    {
        if (id != service.MaDichVu) return NotFound();
        if (!ModelState.IsValid) return View(service);
        var (ok, message) = await _serviceService.UpdateServiceAsync(service);
        SetAlert(ok, message);
        if (!ok) return View(service);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteService(int id)
    {
        var (ok, message) = await _serviceService.DeleteServiceAsync(id);
        SetAlert(ok, message);
        return RedirectToAction(nameof(Index));
    }

    // ---- Sử dụng dịch vụ ----
    public async Task<IActionResult> CreateUsage(string? ky, int? roomId)
    {
        await LoadUsageDropdownsAsync();
        return View(new ServiceUsage { KySuDung = ky ?? await _serviceService.GenerateKySuDungAsync(), MaPhong = roomId ?? 0 });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUsage(ServiceUsage usage)
    {
        if (!ModelState.IsValid)
        {
            await LoadUsageDropdownsAsync();
            return View(usage);
        }
        var (ok, message) = await _serviceService.CreateUsageAsync(usage);
        SetAlert(ok, message);
        if (!ok)
        {
            await LoadUsageDropdownsAsync();
            return View(usage);
        }
        return RedirectToAction(nameof(Index), new { ky = usage.KySuDung });
    }

    public async Task<IActionResult> EditUsage(int id)
    {
        var usage = await _serviceService.GetUsageByIdAsync(id);
        if (usage == null) return NotFound();
        return View(usage);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUsage(int id, ServiceUsage usage)
    {
        if (id != usage.MaSuDung) return NotFound();
        if (!ModelState.IsValid) return View(usage);
        var (ok, message) = await _serviceService.UpdateUsageAsync(usage);
        SetAlert(ok, message);
        if (!ok) return View(usage);
        return RedirectToAction(nameof(Index), new { ky = usage.KySuDung });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUsage(int id)
    {
        var usage = await _serviceService.GetUsageByIdAsync(id);
        var (ok, message) = await _serviceService.DeleteUsageAsync(id);
        SetAlert(ok, message);
        return RedirectToAction(nameof(Index), new { ky = usage?.KySuDung });
    }

    private async Task LoadUsageDropdownsAsync()
    {
        var rooms = await _roomService.GetAllAsync();
        var services = await _serviceService.GetServicesAsync();
        ViewBag.Rooms = new SelectList(rooms.Select(r => new { r.MaPhong, Display = $"{r.MaPhongKyHieu} - {r.TenPhong}" }), "MaPhong", "Display");
        ViewBag.DichVus = new SelectList(services.Where(s => s.TrangThai), "MaDichVu", "TenDichVu");
    }
}
