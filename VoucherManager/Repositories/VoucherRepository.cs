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
}
