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
    public async Task<JsonResult> GetVouchers()
    {
        var vouchers = await _voucherRepository.GetAllVouchersAsync();
        var vouchersDto = vouchers.Select(v => v.ToVoucherDto()).ToList();
        return Json(new {data = vouchersDto});
    }

    [HttpGet("Details/{serialNumber}")]
    public async Task<IActionResult> Details(string serialNumber)
    {
        if (string.IsNullOrEmpty(serialNumber))
        {
            return BadRequest("Serial number is required.");
        }

        var voucher = await _voucherRepository.GetVoucherBySerialNumberAsync(serialNumber);

        if (voucher == null)
        {
            return NotFound("Voucher not found.");
        }

        var voucherDto = voucher.ToVoucherDto();

        return View(voucherDto);
    }
}
