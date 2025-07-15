using Microsoft.AspNetCore.Mvc;
using VoucherManager.Interfaces;
using VoucherManager.Mappers;

namespace VoucherManager.Controllers;

public class VoucherController : Controller
{
    private readonly IVoucherRepository _voucherRepository;
    public VoucherController(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }
    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> GetVouchers()
    {
        var vouchers = await _voucherRepository.GetAllVouchersAsync();
        var voucherDtos = vouchers.Select(v => v.ToVoucherDto()).ToList();
        return Json(vouchers);
    }
}
