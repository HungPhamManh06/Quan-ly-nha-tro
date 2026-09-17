using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;
using QuanLyNhaTro.ViewModels;

namespace QuanLyNhaTro.Controllers;

[Authorize]
public class PaymentsController : Controller
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService) => _paymentService = paymentService;

    private void SetAlert(bool ok, string message) => TempData[ok ? "Success" : "Error"] = message;

    public async Task<IActionResult> Index()
    {
        var payments = await _paymentService.GetAllAsync();
        return View(payments);
    }

    // GET: /Payments/Pay/5 — form ghi nhận thanh toán cho hóa đơn
    public async Task<IActionResult> Pay(int id)
    {
        try
        {
            var vm = await _paymentService.BuildPaymentViewModelAsync(id);
            if (vm.TrangThaiThanhToan == InvoicePaymentStatus.DaThanhToan)
            {
                SetAlert(false, "Hóa đơn này đã được thanh toán. Mỗi hóa đơn chỉ được thanh toán một lần duy nhất.");
                return RedirectToAction(nameof(Details), "Invoices", new { id });
            }
            return View(vm);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    // POST: /Payments/Pay — Use Case: Ghi nhận thanh toán
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Pay(PaymentViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var (ok, message) = await _paymentService.CreateAsync(vm);
        SetAlert(ok, message);
        if (!ok) return View(vm);
        return RedirectToAction(nameof(Details), new { id = vm.MaHoaDon });
    }

    public async Task<IActionResult> Details(int id)
    {
        var payment = await _paymentService.GetDetailAsync(id);
        if (payment == null) return NotFound();
        return View(payment);
    }
}
