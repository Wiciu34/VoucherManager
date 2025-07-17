using VoucherManager.DTOs;
using VoucherManager.Models;

namespace VoucherManager.Mappers;

public static class VoucherMapper
{
    public static VoucherDTO ToVoucherDto(this Voucher voucher)
    {
        if (voucher == null) throw new ArgumentNullException(nameof(voucher));
        return new VoucherDTO
        {
            SerialNumber = voucher.SerialNumber,
            VoucherType = voucher.VoucherType,
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
}
