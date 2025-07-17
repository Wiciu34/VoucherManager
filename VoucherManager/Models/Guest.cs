using System.ComponentModel.DataAnnotations;

namespace VoucherManager.Models;

public class Guest
{
    [Key]
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public IEnumerable<Voucher>? Vouchers { get; set; }
}
