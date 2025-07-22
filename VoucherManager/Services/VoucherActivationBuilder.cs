using VoucherManager.Data;
using VoucherManager.Interfaces;
using VoucherManager.Models;

namespace VoucherManager.Services;

public class VoucherActivationBuilder : IVoucherActivationBuilder
{
    private Voucher _voucher;
    public Voucher Build()
    {
        return _voucher;
    }

    public IVoucherActivationBuilder SetActivationDate()
    {
        _voucher.ActivationDate = DateTime.UtcNow;
        return this;
    }

    public IVoucherActivationBuilder SetExpirationDate()
    {
        _voucher.ExpirationDate = DateTime.UtcNow.AddMonths(6);
        return this;
    }

    public IVoucherActivationBuilder SetGuest(Guest guest)
    {
        _voucher.Guest = guest;
        return this;
    }

    public IVoucherActivationBuilder SetInvoiceNumber(string invoiceNumber)
    {
        _voucher.InovoiceNumber = invoiceNumber ?? throw new ArgumentNullException(nameof(invoiceNumber), "Numer faktury nie może być pusty.");
        return this;
    }

    public IVoucherActivationBuilder SetRealizationDate()
    {
        throw new NotImplementedException();
    }

    public IVoucherActivationBuilder SetSellDate(DateTime? date)
    {
        if (date != null) _voucher.SellDate = date;
        else _voucher.SellDate = DateTime.UtcNow;
        return this;
    }

    public IVoucherActivationBuilder SetStatus(Status status)
    {
        _voucher.Status = status;
        return this;
    }

    public void SetVoucher(Voucher voucher)
    {
        _voucher = voucher ?? throw new ArgumentNullException(nameof(voucher), "Voucher nie może mieć wartości null.");
    }
}
