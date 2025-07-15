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
            SellDate = voucher.SellDate?.ToString("yyyy-MM-dd"),
            ExpirationDate = voucher.ExpirationDate.ToString("yyyy-MM-dd"),
            Resort = voucher.Resort,
            Status = voucher.Status.ToString()
        };
    }
}
