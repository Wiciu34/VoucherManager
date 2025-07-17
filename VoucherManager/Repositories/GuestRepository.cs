using Microsoft.EntityFrameworkCore;
using VoucherManager.Data;
using VoucherManager.Interfaces;
using VoucherManager.Models;

namespace VoucherManager.Repositories;

public class GuestRepository : IGuestRepository
{
    private readonly AppDbContext _context;
    public GuestRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Guest> GetGuestByEmailAsync(string email, string phoneNumber)
    {
        // Fix: Ensure the query is awaited on the database context
        var guest = await _context.Guest
            .FirstOrDefaultAsync(g => g.Email == email && g.PhoneNumber == phoneNumber);
        
        if (guest == null) 
        {
            return null;
        }

        return guest;
    }

    public Task<Guest> AddGuestAsync(Guest guest)
    {
        throw new NotImplementedException();
    }


}
