using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;

namespace QuanLyNhaTro.Controllers;

[Authorize]
public class UtilitiesController : Controller
{
    private readonly IUtilityService _utilityService;
    private readonly IRoomService _roomService;

    public UtilitiesController(IUtilityService utilityService, IRoomService roomService)
    {
        _utilityService = utilityService;
        _roomService = roomService;
    }

    private void SetAlert(bool ok, string message) => TempData[ok ? "Success" : "Error"] = message;

    public async Task<IActionResult> Index(string? search, string? ky)
    {
        var readings = await _utilityService.GetAllAsync(search, ky);
        ViewBag.Search = search;
        ViewBag.Ky = ky;
        ViewBag.KyList = new SelectList(await GetKyListAsync(), ky);
        return View(readings);
    }

    // GET: /Utilities/Create?roomId=1&ky=2026-01
    public async Task<IActionResult> Create(int? roomId, string? ky)
    {
        var reading = new UtilityReading
        {
            MaPhong = roomId ?? 0,
            KyGhi = ky ?? await _utilityService.GenerateKyAsync()
        };

        // Tự điền chỉ số cũ từ kỳ gần nhất & đơn giá mặc định
        if (roomId.HasValue && roomId.Value > 0)
        {
            var latest = await _utilityService.GetLatestBeforeAsync(roomId.Value, reading.KyGhi);
            if (latest != null)
            {
                reading.ChiSoDienCu = latest.ChiSoDienMoi;
                reading.ChiSoNuocCu = latest.ChiSoNuocMoi;
                reading.DonGiaDien = latest.DonGiaDien;
                reading.DonGiaNuoc = latest.DonGiaNuoc;
            }
        }
        await LoadRoomsAsync();
        return View(reading);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UtilityReading reading)
    {
        if (reading.ChiSoDienMoi < reading.ChiSoDienCu)
            ModelState.AddModelError(nameof(reading.ChiSoDienMoi), "Chỉ số điện mới phải lớn hơn hoặc bằng chỉ số cũ.");
        if (reading.ChiSoNuocMoi < reading.ChiSoNuocCu)
            ModelState.AddModelError(nameof(reading.ChiSoNuocMoi), "Chỉ số nước mới phải lớn hơn hoặc bằng chỉ số cũ.");
        if (!ModelState.IsValid)
        {
            await LoadRoomsAsync();
            return View(reading);
        }

        var (ok, message) = await _utilityService.CreateAsync(reading);
        SetAlert(ok, message);
        if (!ok)
        {
            await LoadRoomsAsync();
            return View(reading);
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var reading = await _utilityService.GetByIdAsync(id);
        if (reading == null) return NotFound();
        return View(reading);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UtilityReading reading)
    {
        if (id != reading.MaGhiChiSo) return NotFound();
        if (reading.ChiSoDienMoi < reading.ChiSoDienCu)
            ModelState.AddModelError(nameof(reading.ChiSoDienMoi), "Chỉ số điện mới phải lớn hơn hoặc bằng chỉ số cũ.");
        if (reading.ChiSoNuocMoi < reading.ChiSoNuocCu)
            ModelState.AddModelError(nameof(reading.ChiSoNuocMoi), "Chỉ số nước mới phải lớn hơn hoặc bằng chỉ số cũ.");
        if (!ModelState.IsValid) return View(reading);

        var (ok, message) = await _utilityService.UpdateAsync(reading);
        SetAlert(ok, message);
        if (!ok) return View(reading);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var (ok, message) = await _utilityService.DeleteAsync(id);
        SetAlert(ok, message);
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadRoomsAsync()
    {
        var rooms = await _roomService.GetAllAsync();
        ViewBag.Rooms = new SelectList(rooms.Select(r => new { r.MaPhong, Display = $"{r.MaPhongKyHieu} - {r.TenPhong}" }), "MaPhong", "Display");
    }

    private async Task<List<string>> GetKyListAsync()
    {
        var all = await _utilityService.GetAllAsync();
        return all.Select(u => u.KyGhi).Distinct().OrderByDescending(k => k).ToList();
    }
}
