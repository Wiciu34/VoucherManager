using VoucherManager.Models;

namespace VoucherManager.Interfaces;

public interface IGuestRepository
{
    Task<Guest> GetGuestByEmailAsync(string email, string phoneNumber);
    Task<Guest> AddGuestAsync(Guest guest);
}
