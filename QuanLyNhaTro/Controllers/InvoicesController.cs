using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTro.ViewModels;

namespace QuanLyNhaTro.Controllers;

[Authorize]
public class InvoicesController : Controller
{
    private readonly IInvoiceService _invoiceService;

    public InvoicesController(IInvoiceService invoiceService) => _invoiceService = invoiceService;

    private void SetAlert(bool ok, string message) => TempData[ok ? "Success" : "Error"] = message;

    public async Task<IActionResult> Index(string? search, string? ky, InvoicePaymentStatus? trangThai)
    {
        var invoices = await _invoiceService.GetAllAsync(search, ky, trangThai);
        ViewBag.Search = search;
        ViewBag.Ky = ky;
        ViewBag.TrangThai = trangThai;
        var allKy = (await _invoiceService.GetAllAsync()).Select(i => i.KyHoaDon).Distinct().OrderByDescending(k => k).ToList();
        ViewBag.KyList = new SelectList(allKy, ky);
        return View(invoices);
    }

    public async Task<IActionResult> Details(int id)
    {
        var invoice = await _invoiceService.GetDetailAsync(id);
        if (invoice == null) return NotFound();
        return View(invoice);
    }

    // GET: /Invoices/Create?roomId=1&ky=2026-01 — tự gộp tiền phòng + điện + nước + dịch vụ
    public async Task<IActionResult> Create(int? roomId, string? ky, bool reload = false)
    {
        var vm = await _invoiceService.BuildCreateViewModelAsync(roomId, ky, reload);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(InvoiceCreateViewModel vm)
    {
        if (vm.MaPhong <= 0) ModelState.AddModelError(nameof(vm.MaPhong), "Vui lòng chọn phòng.");
        if (!ModelState.IsValid)
        {
            var rebuild = await _invoiceService.BuildCreateViewModelAsync(vm.MaPhong > 0 ? vm.MaPhong : null, vm.KyHoaDon);
            rebuild.KyHoaDon = vm.KyHoaDon;
            rebuild.TienPhong = vm.TienPhong;
            rebuild.TienDien = vm.TienDien;
            rebuild.TienNuoc = vm.TienNuoc;
            rebuild.TienDichVu = vm.TienDichVu;
            rebuild.HanThanhToan = vm.HanThanhToan;
            rebuild.GhiChu = vm.GhiChu;
            return View(rebuild);
        }

        var (ok, message) = await _invoiceService.CreateAsync(vm);
        SetAlert(ok, message);
        if (!ok)
        {
            var rebuild = await _invoiceService.BuildCreateViewModelAsync(vm.MaPhong, vm.KyHoaDon);
            rebuild.TienPhong = vm.TienPhong;
            rebuild.TienDien = vm.TienDien;
            rebuild.TienNuoc = vm.TienNuoc;
            rebuild.TienDichVu = vm.TienDichVu;
            rebuild.HanThanhToan = vm.HanThanhToan;
            rebuild.GhiChu = vm.GhiChu;
            return View(rebuild);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var (ok, message) = await _invoiceService.DeleteAsync(id);
        SetAlert(ok, message);
        return RedirectToAction(nameof(Index));
    }
}
