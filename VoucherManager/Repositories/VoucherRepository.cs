using Microsoft.EntityFrameworkCore;
using VoucherManager.Data;
using VoucherManager.Interfaces;
using VoucherManager.Models;
using VoucherManager.ViewModels;

namespace VoucherManager.Repositories;

public class VoucherRepository : IVoucherRepository
{
    private readonly AppDbContext _context;
    private readonly IGuestRepository _guestRepository;
    public VoucherRepository(AppDbContext context, IGuestRepository guestRepository)
    {
        _context = context;
        _guestRepository = guestRepository;
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
            .Include(g => g.Guest)
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.SerialNumber == serialNumber);

        if (voucher == null)
        {
            throw new KeyNotFoundException($"Nie znaleziono vouchera o nr: '{serialNumber}");
        }

        return voucher;
    }
    public async Task UpdateVoucherAsync(ActivationVoucherViewModel viewModel)
    {
        var voucher = await GetVoucherBySerialNumberAsync(viewModel.SerialNumber);

        if (voucher.Status != Status.Nieaktywny)
        {
            throw new InvalidOperationException($"Voucher o statusie {voucher.Status.ToString()} nie może zostać aktywowany");
        }

        voucher.Status = Status.Aktywny;
        voucher.ActivationDate = DateTime.UtcNow;
        voucher.ExpirationDate = DateTime.UtcNow.AddMonths(6);

        if(voucher.SellDate == null)
        {
            voucher.SellDate = DateTime.UtcNow;
        }

        var guest = await _guestRepository.GetGuestByEmailAsync(viewModel.Email, viewModel.PhoneNumber);

        if (guest != null)
        {
            voucher.Guest = guest;
        }
        else
        {
            voucher.Guest = new Guest
            {
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber
            };
        }

        _context.Vouchers.Update(voucher);

        await _context.SaveChangesAsync();
    }

    
}
