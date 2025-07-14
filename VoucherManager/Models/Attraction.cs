using System.ComponentModel.DataAnnotations;

namespace VoucherManager.Models;

public class Attraction
{
    [Key]
    public int Id { get; set; }
    public required string Content { get; set; }
    public ICollection<Voucher>? Vouchers { get; set; }
}
