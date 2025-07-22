using VoucherManager.Models;
using VoucherManager.ViewModels;

namespace VoucherManager.Interfaces
{
    public interface IVoucherRepository
    {
        Task<IEnumerable<Voucher>> GetAllVouchersAsync();
        Task<Voucher> GetVoucherBySerialNumberAsync(string serialNumber);
        Task UpdateVoucherAsync(Voucher voucher);

    }
}
