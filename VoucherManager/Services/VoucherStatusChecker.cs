
using Microsoft.EntityFrameworkCore;
using VoucherManager.Data;

namespace VoucherManager.Services;

public class VoucherStatusChecker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public VoucherStatusChecker(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var now = DateTime.UtcNow;

                var expiredVouchers = await db.Vouchers
                    .Where(v => (v.Status == Status.Aktywowany || v.Status == Status.Nieaktywny) && v.ExpirationDate.AddDays(7) < now)
                    .ToListAsync(stoppingToken);

                foreach (var voucher in expiredVouchers)
                {
                    voucher.Status = Status.Niezrealizowany;
                }

                if (expiredVouchers.Any())
                {
                    await db.SaveChangesAsync(stoppingToken);
                }
            }
            await Task.Delay(TimeSpan.FromHours(12), stoppingToken);
        }
    }
}
