using VoucherManager.Data;
using VoucherManager.Models;

namespace VoucherManager.Interfaces;

public interface IVoucherActivationBuilder
{
    IVoucherActivationBuilder SetSellDate(DateTime? date);
    IVoucherActivationBuilder SetActivationDate();
    IVoucherActivationBuilder SetExpirationDate();
    IVoucherActivationBuilder SetRealizationDate(DateTime date);
    IVoucherActivationBuilder SetStatus(Status status);
    IVoucherActivationBuilder SetGuest(Guest guest);
    IVoucherActivationBuilder SetInvoiceNumber(string invoiceNumber);
    void SetVoucher(Voucher voucher);
    Voucher Build();
}
