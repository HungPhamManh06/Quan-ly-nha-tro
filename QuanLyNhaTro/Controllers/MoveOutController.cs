using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhaTro.Services;
using QuanLyNhaTro.ViewModels;

namespace QuanLyNhaTro.Controllers;

[Authorize]
public class MoveOutController : Controller
{
    private readonly IMoveOutService _moveOutService;
    private readonly ILogger<MoveOutController> _logger;

    public MoveOutController(IMoveOutService moveOutService, ILogger<MoveOutController> logger)
    {
        _moveOutService = moveOutService;
        _logger = logger;
    }

    private void SetAlert(bool ok, string message) => TempData[ok ? "Success" : "Error"] = message;

    // Bước 1: Chọn hợp đồng
    public async Task<IActionResult> Index()
    {
        var contracts = await _moveOutService.GetLiquidatableContractsAsync();
        return View(contracts);
    }

    // Bước 2-6: Wizard chốt điện nước, dịch vụ, công nợ, cọc, quyết toán
    public async Task<IActionResult> Settle(int id)
    {
        var vm = await _moveOutService.BuildWizardAsync(id);
        if (vm == null) return NotFound();
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChotDienNuoc(MoveOutViewModel vm)
    {
        var (ok, message) = await _moveOutService.ChotDienNuocCuoiKyAsync(vm.MaHopDong, vm.ChiSoDienMoi, vm.ChiSoNuocMoi, vm.DonGiaDien, vm.DonGiaNuoc);
        SetAlert(ok, message);
        return RedirectToAction(nameof(Settle), new { id = vm.MaHopDong });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChotDichVu(MoveOutViewModel vm)
    {
        var (ok, message) = await _moveOutService.ChotDichVuCuoiKyAsync(vm.MaHopDong);
        SetAlert(ok, message);
        return RedirectToAction(nameof(Settle), new { id = vm.MaHopDong });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Preview(MoveOutViewModel vm)
    {
        var preview = await _moveOutService.PreviewAsync(vm);
        return Json(preview);
    }

    // Bước 7: Quyết toán + thanh lý hợp đồng + cập nhật phòng (transaction)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Confirm(MoveOutViewModel vm)
    {
        var (ok, message) = await _moveOutService.SettleAsync(vm);
        SetAlert(ok, message);
        if (!ok) return RedirectToAction(nameof(Settle), new { id = vm.MaHopDong });
        return RedirectToAction(nameof(Details), new { id = vm.MaHopDong });
    }

    // Xem kết quả quyết toán
    public async Task<IActionResult> Details(int id)
    {
        var moveOut = await _moveOutService.GetByContractAsync(id);
        if (moveOut == null) return NotFound();
        return View(moveOut);
    }
}
