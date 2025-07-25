using Microsoft.AspNetCore.Mvc;
using VoucherManager.Data;
using VoucherManager.Interfaces;
using VoucherManager.Models;
using VoucherManager.ViewModels;

namespace VoucherManager.Controllers;

public class VoucherActivationController : Controller
{
    private readonly IVoucherRepository _voucherRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IVoucherActivationBuilder _voucherActivationBuilder;
    public VoucherActivationController(IVoucherRepository voucherRepository, IGuestRepository guestRepository, IVoucherActivationBuilder voucherActivationBuilder)
    {
        _voucherRepository = voucherRepository;
        _guestRepository = guestRepository;
        _voucherActivationBuilder = voucherActivationBuilder;
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
            var voucher = await _voucherRepository.GetVoucherBySerialNumberAsync(model.SerialNumber);

            if (voucher.Status != Status.Nieaktywny)
            {
                ModelState.AddModelError("SerialNumber", $"Voucher o statusie {voucher.Status.ToString()} nie może zostać aktywowany");
                return View("Index", model);
            }

            if (voucher.ExpirationDate < DateTime.UtcNow.AddDays(-7))
            {
                throw new InvalidOperationException("Czas na aktywacje vouchera minął ponad 7 dni temu");
            }

            var guest = await _guestRepository.GetGuestByEmailAsync(model.Email, model.PhoneNumber);
            
            if (guest == null) guest = new Guest { Email = model.Email, PhoneNumber = model.PhoneNumber };

            _voucherActivationBuilder.SetVoucher(voucher);

            voucher = _voucherActivationBuilder
                .SetActivationDate()
                .SetExpirationDate()
                .SetStatus(Status.Aktywowany)
                .SetGuest(guest)
                .Build();

            await _voucherRepository.UpdateVoucherAsync(voucher);
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