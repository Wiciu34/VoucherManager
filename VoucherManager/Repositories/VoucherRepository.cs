using Microsoft.EntityFrameworkCore;
using VoucherManager.Data;
using VoucherManager.Interfaces;
using VoucherManager.Models;
using VoucherManager.ViewModels;

namespace VoucherManager.Repositories;

public class VoucherRepository : IVoucherRepository
{
    private readonly AppDbContext _context;
    public VoucherRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Voucher>> GetAllVouchersAsync()
    {
        var vouchers = await _context.Vouchers.ToListAsync();
        return vouchers;
    }
    public async Task<Voucher> GetVoucherBySerialNumberAsync(string serialNumber)
    {
        if (string.IsNullOrEmpty(serialNumber))
        {
            throw new ArgumentException("Serial number cannot be null or empty.", nameof(serialNumber));
        }
        
        var voucher = await _context.Vouchers
            .Include(a => a.Attractions)
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.SerialNumber == serialNumber);

        if (voucher == null)
        {
            throw new KeyNotFoundException($"Voucher with serial number '{serialNumber}' not found.");
        }

        return voucher;
    }
    public async Task ActivateVoucherByBrokerAsync(ActivationVoucherViewModel viewModel)
    {
        var voucher = _context.Vouchers
            .FirstOrDefault(v => v.SerialNumber == viewModel.SerialNumber);

        if (voucher == null)
        {
            throw new KeyNotFoundException($"Nie znaleziono vouchera o nr: '{viewModel.SerialNumber}");
        }

        if (voucher.Status != Status.Nieaktywny)
        {
            throw new InvalidOperationException($"Voucher o statusie {voucher.Status.ToString()} nie może zostać aktywowany");
        }

        voucher.Status = Status.Aktywny;
        //voucher.ActivationDate = DateTime.UtcNow;
        voucher.ExpirationDate = DateTime.UtcNow.AddMonths(6);
        _context.Vouchers.Update(voucher);

        await _context.SaveChangesAsync();
    }
}
