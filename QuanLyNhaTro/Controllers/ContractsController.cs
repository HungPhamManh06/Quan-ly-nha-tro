using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTro.Extensions;
using QuanLyNhaTro.ViewModels;

namespace QuanLyNhaTro.Controllers;

[Authorize]
public class ContractsController : Controller
{
    private readonly IContractService _contractService;
    private readonly IRoomService _roomService;
    private readonly ITenantService _tenantService;
    private readonly ILogger<ContractsController> _logger;

    public ContractsController(IContractService contractService, IRoomService roomService, ITenantService tenantService, ILogger<ContractsController> logger)
    {
        _contractService = contractService;
        _roomService = roomService;
        _tenantService = tenantService;
        _logger = logger;
    }

    private void SetAlert(bool ok, string message) => TempData[ok ? "Success" : "Error"] = message;

    public async Task<IActionResult> Index(string? search, ContractStatus? trangThai, bool? sapHetHan)
    {
        var contracts = await _contractService.GetAllAsync(search, status: trangThai, sapHetHan: sapHetHan);
        ViewBag.Search = search;
        ViewBag.TrangThai = trangThai;
        ViewBag.SapHetHan = sapHetHan;
        return View(contracts);
    }

    public async Task<IActionResult> Details(int id)
    {
        var contract = await _contractService.GetDetailAsync(id);
        if (contract == null) return NotFound();
        return View(contract);
    }

    public async Task<IActionResult> Create(int? roomId)
    {
        await LoadDropdownsAsync();
        var contract = new Contract
        {
            MaHopDongKyHieu = await _contractService.GenerateMaHopDongAsync(),
            NgayBatDau = DateTime.Today,
            NgayKetThuc = DateTime.Today.AddYears(1),
            MaPhong = roomId ?? 0
        };
        return View(contract);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Contract contract)
    {
        // Validation client + server
        if (contract.NgayKetThuc <= contract.NgayBatDau)
            ModelState.AddModelError(nameof(contract.NgayKetThuc), "Ngày kết thúc phải sau ngày bắt đầu.");
        if (!ModelState.IsValid)
        {
            await LoadDropdownsAsync();
            return View(contract);
        }

        var (ok, message) = await _contractService.CreateAsync(contract);
        SetAlert(ok, message);
        if (!ok)
        {
            await LoadDropdownsAsync();
            return View(contract);
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: /Contracts/Renew/5 — Gia hạn hợp đồng
    public async Task<IActionResult> Renew(int id)
    {
        var contract = await _contractService.GetByIdAsync(id);
        if (contract == null) return NotFound();
        if (contract.TrangThai == ContractStatus.DaThanhLy)
        {
            SetAlert(false, "Hợp đồng đã thanh lý, không thể gia hạn.");
            return RedirectToAction(nameof(Index));
        }

        var vm = new RenewContractViewModel
        {
            MaHopDong = contract.MaHopDong,
            MaHopDongKyHieu = contract.MaHopDongKyHieu,
            TenPhong = contract.Room?.MaPhongKyHieu ?? "",
            TenNguoiThue = contract.Tenant?.HoTen ?? "",
            NgayBatDau = contract.NgayBatDau,
            NgayKetThuc = contract.NgayKetThuc,
            GiaThue = contract.GiaThue,
            TienCoc = contract.TienCoc,
            NgayKetThucMoi = contract.NgayKetThuc.AddYears(1)
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Renew(RenewContractViewModel vm)
    {
        if (vm.NgayKetThucMoi <= vm.NgayKetThuc)
            ModelState.AddModelError(nameof(vm.NgayKetThucMoi), "Ngày kết thúc mới phải sau ngày kết thúc hiện tại.");
        if (vm.GiaThueMoi.HasValue && vm.GiaThueMoi.Value <= 0)
            ModelState.AddModelError(nameof(vm.GiaThueMoi), "Giá thuê mới phải lớn hơn 0.");
        if (!ModelState.IsValid) return View(vm);

        var (ok, message) = await _contractService.RenewAsync(vm.MaHopDong, vm.NgayKetThucMoi, vm.GiaThueMoi, vm.TienCocMoi, vm.GhiChu);
        SetAlert(ok, message);
        if (!ok) return View(vm);
        return RedirectToAction(nameof(Details), new { id = vm.MaHopDong });
    }

    // POST: /Contracts/Liquidate/5 — Thanh lý hợp đồng (có confirm dialog ở View)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Liquidate(int id, string? ghiChu)
    {
        var (ok, message) = await _contractService.LiquidateAsync(id, ghiChu);
        SetAlert(ok, message);
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadDropdownsAsync()
    {
        var rooms = await _roomService.GetAllAsync();
        var tenants = await _tenantService.GetAllAsync();
        ViewBag.Rooms = new SelectList(rooms.Select(r => new { r.MaPhong, Display = $"{r.MaPhongKyHieu} - {r.TenPhong} ({r.TrangThai.GetDisplayName()})" }),
            "MaPhong", "Display");
        ViewBag.Tenants = new SelectList(tenants.Select(t => new { t.Tenant.MaNguoiThue, Display = $"{t.Tenant.HoTen} - {t.Tenant.CCCD}" }),
            "MaNguoiThue", "Display");
    }
}
