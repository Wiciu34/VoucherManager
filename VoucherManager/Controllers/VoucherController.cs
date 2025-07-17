using Microsoft.AspNetCore.Mvc;
using VoucherManager.DTOs;
using VoucherManager.Interfaces;
using VoucherManager.Mappers;
using VoucherManager.ViewModels;

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

    [HttpPost]
    public async Task<JsonResult> ActivateVoucherByWorker(ActivationVoucherViewModel activationVoucher)
    {
        if (activationVoucher == null || string.IsNullOrEmpty(activationVoucher.SerialNumber))
        {
            return Json(new { success = false, message = "Brak danych lub numeru seryjnego" });
        }
        try
        {
            await _voucherRepository.ActivateVoucherByBrokerAsync(activationVoucher);
            return Json(new { success = true, message = "Voucher został aktywowany pomyślnie." });
        }
        catch (KeyNotFoundException e)
        {
            return Json(new { success = false, message = e.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }
}
