using Microsoft.AspNetCore.Mvc;
using VoucherManager.Interfaces;
using VoucherManager.ViewModels;

namespace VoucherManager.Controllers;

public class VoucherActivationController : Controller
{
    private readonly IVoucherRepository _voucherRepository;
    public VoucherActivationController(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ActivateVoucher(ActivationVoucherViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }

        try
        {
            await _voucherRepository.UpdateVoucherAsync(model);
            return RedirectToAction("Index", "Voucher");
        }
        catch(KeyNotFoundException e)
        {
            ModelState.AddModelError("SerialNumber", e.Message);
            return View("Index", model);
        }
        catch(InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View("Index", model);
        }
    } 
}