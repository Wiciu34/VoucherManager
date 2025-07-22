using Microsoft.AspNetCore.Mvc;
using VoucherManager.Data;
using VoucherManager.DTOs;
using VoucherManager.Interfaces;
using VoucherManager.Mappers;
using VoucherManager.Models;
using VoucherManager.ViewModels;

namespace VoucherManager.Controllers;

public class VoucherController : Controller
{
    private readonly IVoucherRepository _voucherRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IVoucherActivationBuilder _voucherActivationBuilder;
    public VoucherController(IVoucherRepository voucherRepository, IGuestRepository guestRepository, IVoucherActivationBuilder voucherActivationBuilder)
    {
        _voucherRepository = voucherRepository;
        _guestRepository = guestRepository;
        _voucherActivationBuilder = voucherActivationBuilder;
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
    [HttpGet]
    public async Task<JsonResult> GetVoucher(string serialNumber)
    {
        var voucher = await _voucherRepository.GetVoucherBySerialNumberAsync(serialNumber);
        return Json(new {data = voucher.ToVoucherDto()});
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
            var voucher = await _voucherRepository.GetVoucherBySerialNumberAsync(activationVoucher.SerialNumber);

            if (voucher.Status != Status.Nieaktywny)
            {
                throw new InvalidOperationException($"Voucher o statusie {voucher.Status.ToString()} nie może zostać aktywowany");
            }

            var guest = await _guestRepository.GetGuestByEmailAsync(activationVoucher.Email, activationVoucher.PhoneNumber);
            if (guest == null) guest = new Guest { Email = activationVoucher.Email, PhoneNumber = activationVoucher.PhoneNumber };

            _voucherActivationBuilder.SetVoucher(voucher);

            voucher = _voucherActivationBuilder
                .SetActivationDate()
                .SetExpirationDate()
                .SetStatus(Status.Aktywny)
                .SetSellDate(null)
                .SetGuest(guest)
                .SetInvoiceNumber(activationVoucher.InvoiceNumber)
                .Build();

            await _voucherRepository.UpdateVoucherAsync(voucher);

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
