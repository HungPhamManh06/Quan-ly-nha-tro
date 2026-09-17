using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;

namespace QuanLyNhaTro.Controllers;

[Authorize]
public class RoomsController : Controller
{
    private readonly IRoomService _roomService;
    private readonly IContractService _contractService;
    private readonly ILogger<RoomsController> _logger;

    public RoomsController(IRoomService roomService, IContractService contractService, ILogger<RoomsController> logger)
    {
        _roomService = roomService;
        _contractService = contractService;
        _logger = logger;
    }

    private void SetAlert(bool ok, string message)
    {
        TempData[ok ? "Success" : "Error"] = message;
    }

    // GET: /Rooms
    public async Task<IActionResult> Index(string? search, RoomStatus? trangThai)
    {
        var rooms = await _roomService.GetAllAsync(search, trangThai);
        ViewBag.Search = search;
        ViewBag.TrangThai = trangThai;
        return View(rooms);
    }

    // GET: /Rooms/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var room = await _roomService.GetDetailAsync(id);
        if (room == null) return NotFound();

        var today = DateTime.Today;
        ViewBag.HopDongHienTai = room.Contracts.FirstOrDefault(c => c.TrangThai != ContractStatus.DaThanhLy && c.NgayBatDau <= today && c.NgayKetThuc >= today);
        ViewBag.HoaDonChuaTT = room.Invoices.Where(i => i.TrangThaiThanhToan == InvoicePaymentStatus.ChuaThanhToan).ToList();
        return View(room);
    }

    // GET: /Rooms/Create
    public async Task<IActionResult> Create()
    {
        ViewBag.SuggestedMaPhong = await _roomService.GenerateMaPhongAsync();
        return View(new Room { MaPhongKyHieu = ViewBag.SuggestedMaPhong });
    }

    // POST: /Rooms/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Room room)
    {
        if (!ModelState.IsValid) return View(room);
        var (ok, message) = await _roomService.CreateAsync(room);
        SetAlert(ok, message);
        if (!ok) return View(room);
        return RedirectToAction(nameof(Index));
    }

    // GET: /Rooms/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var room = await _roomService.GetByIdAsync(id);
        if (room == null) return NotFound();
        return View(room);
    }

    // POST: /Rooms/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Room room)
    {
        if (id != room.MaPhong) return NotFound();
        if (!ModelState.IsValid) return View(room);
        var (ok, message) = await _roomService.UpdateAsync(room);
        SetAlert(ok, message);
        if (!ok) return View(room);
        return RedirectToAction(nameof(Index));
    }

    // POST: /Rooms/Delete/5 — confirm dialog đã hiện ở View
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var (ok, message) = await _roomService.DeleteAsync(id);
        SetAlert(ok, message);
        return RedirectToAction(nameof(Index));
    }
}
