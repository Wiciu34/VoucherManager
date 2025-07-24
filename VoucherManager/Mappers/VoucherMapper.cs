using VoucherManager.DTOs;
using VoucherManager.Models;
using VoucherManager.ViewModels;

namespace VoucherManager.Mappers;

public static class VoucherMapper
{
    public static VoucherDTO ToVoucherDto(this Voucher voucher)
    {
        if (voucher == null) throw new ArgumentNullException(nameof(voucher));
        return new VoucherDTO
        {
            SerialNumber = voucher.SerialNumber,
            VoucherType = voucher.VoucherType.ToString(),
            Amount = voucher.Amount,
            SellDate = voucher.SellDate?.ToString("dd-MM-yyyy"),
            ExpirationDate = voucher.ExpirationDate.ToString("dd-MM-yyyy"),
            ActivationDate = voucher.ActivationDate?.ToString("dd-MM-yyyy"),
            RealizationDate = voucher.RealizationDate?.ToString("dd-MM-yyyy"),
            Resort = voucher.Resort,
            Status = voucher.Status.ToString(),
            Attractions = voucher.Attractions?.Select(a => new AttractionDTO
            {
                Content = a.Content
            }).ToList(),
            Guest = voucher.Guest?.toGuestDTO()
        };
    }

    public static CreateEditVoucherViewModel ToCreateEditVoucherViewModel(this Voucher voucher)
    {
        if (voucher == null) throw new ArgumentNullException(nameof(voucher));
        return new CreateEditVoucherViewModel
        {
            SerialNumber = voucher.SerialNumber,
            VoucherType = voucher.VoucherType,
            Amount = voucher.Amount,
            SellDate = voucher.SellDate,
            ActivationDate = voucher.ActivationDate,
            ExpirationDate = voucher.ExpirationDate,
            RealizationDate = voucher.RealizationDate,
            Resort = voucher.Resort,
            Status = voucher.Status,
            InovoiceNumber = voucher.InovoiceNumber,
            GuestId = voucher.GuestId,
        };
    }

    public static Voucher ToVoucher(this CreateEditVoucherViewModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));
        return new Voucher
        {
            SerialNumber = model.SerialNumber,
            VoucherType = model.VoucherType,
            Amount = model.Amount,
            SellDate = model.SellDate,
            ActivationDate = model.ActivationDate,
            ExpirationDate = model.ExpirationDate,
            RealizationDate = model.RealizationDate,
            Resort = model.Resort,
            Status = model.Status,
            InovoiceNumber = model.InovoiceNumber,
            GuestId = model.GuestId
        };
    }
}
