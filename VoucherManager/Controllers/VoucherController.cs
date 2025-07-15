using Microsoft.AspNetCore.Mvc;
using VoucherManager.Interfaces;

namespace VoucherManager.Controllers;

public class VoucherController : Controller
{
    private readonly IVoucherRepository _voucherRepository;
    public VoucherController(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }
    public async Task<IActionResult> Index()
    {
        var vouchers = await _voucherRepository.GetAllVouchersAsync();

        return View(vouchers);
    }
}
