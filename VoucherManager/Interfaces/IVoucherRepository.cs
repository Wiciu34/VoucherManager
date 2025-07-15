using VoucherManager.Models;

namespace VoucherManager.Interfaces
{
    public interface IVoucherRepository
    {
        Task<IEnumerable<Voucher>> GetAllVouchersAsync();
        Task<Voucher> GetVoucherBySerialNumberAsync(string serialNumber);

    }
}
