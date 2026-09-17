using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;

namespace QuanLyNhaTro.Controllers;

[Authorize]
public class TenantsController : Controller
{
    private readonly ITenantService _tenantService;
    private readonly ILogger<TenantsController> _logger;

    public TenantsController(ITenantService tenantService, ILogger<TenantsController> logger)
    {
        _tenantService = tenantService;
        _logger = logger;
    }

    private void SetAlert(bool ok, string message) => TempData[ok ? "Success" : "Error"] = message;

    public async Task<IActionResult> Index(string? search)
    {
        var tenants = await _tenantService.GetAllAsync(search);
        ViewBag.Search = search;
        return View(tenants);
    }

    public async Task<IActionResult> Details(int id)
    {
        var tenant = await _tenantService.GetDetailAsync(id);
        if (tenant == null) return NotFound();
        ViewBag.HopDongHienTai = await _tenantService.GetActiveContractAsync(id);
        return View(tenant);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Tenant tenant)
    {
        if (!ModelState.IsValid) return View(tenant);
        var (ok, message) = await _tenantService.CreateAsync(tenant);
        SetAlert(ok, message);
        if (!ok) return View(tenant);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var tenant = await _tenantService.GetByIdAsync(id);
        if (tenant == null) return NotFound();
        return View(tenant);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Tenant tenant)
    {
        if (id != tenant.MaNguoiThue) return NotFound();
        if (!ModelState.IsValid) return View(tenant);
        var (ok, message) = await _tenantService.UpdateAsync(tenant);
        SetAlert(ok, message);
        if (!ok) return View(tenant);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var (ok, message) = await _tenantService.DeleteAsync(id);
        SetAlert(ok, message);
        return RedirectToAction(nameof(Index));
    }
}
