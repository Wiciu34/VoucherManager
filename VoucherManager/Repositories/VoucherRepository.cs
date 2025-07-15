using Microsoft.EntityFrameworkCore;
using VoucherManager.Data;
using VoucherManager.Interfaces;
using VoucherManager.Models;

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
            .FirstOrDefaultAsync(v => v.SerialNumber == serialNumber);

        if (voucher == null)
        {
            throw new KeyNotFoundException($"Voucher with serial number '{serialNumber}' not found.");
        }

        return voucher;
    }
}
