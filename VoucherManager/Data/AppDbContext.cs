using Microsoft.EntityFrameworkCore;
using VoucherManager.Models;

namespace VoucherManager.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }
    public DbSet<Voucher> Vouchers { get; set; }
    public DbSet<Attraction> Attractions { get; set; }
    public DbSet<Guest> Guest { get; set; }
}
