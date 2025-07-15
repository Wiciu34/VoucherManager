using System.ComponentModel.DataAnnotations;
using System.Data;
using VoucherManager.Data;

namespace VoucherManager.Models;

public class Voucher
{
    [Key]
    public required string SerialNumber { get; set; }
    public required string VoucherType { get; set; }
    public Decimal? Amount { get; set; }
    public DateTime? SellDate { get; set; }
    public required DateTime ExpirationDate { get; set; }
    public required string Resort { get; set; }
    public required Status Status { get; set; }
    public ICollection<Attraction>? Attractions { get; set; }

}
