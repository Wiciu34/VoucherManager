using VoucherManager.DTOs;
using VoucherManager.Models;

namespace VoucherManager.Mappers;

public static class GuestMapper
{
    public static GuestDTO toGuestDTO(this Guest guest)
    {
        return new GuestDTO
        {
            Email = guest.Email,
            PhoneNumber = guest.PhoneNumber
        };
    }
}
