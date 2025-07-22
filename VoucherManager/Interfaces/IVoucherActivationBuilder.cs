using VoucherManager.Data;
using VoucherManager.Models;

namespace VoucherManager.Interfaces;

public interface IVoucherActivationBuilder
{
    IVoucherActivationBuilder SetSellDate(DateTime? date);
    IVoucherActivationBuilder SetActivationDate();
    IVoucherActivationBuilder SetExpirationDate();
    IVoucherActivationBuilder SetRealizationDate();
    IVoucherActivationBuilder SetStatus();
    IVoucherActivationBuilder SetVoucher(Voucher voucher);
    Voucher Build();
}
