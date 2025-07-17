using VoucherManager.Data;

namespace VoucherManager.DTOs;

public class VoucherDTO
{
    public required string SerialNumber { get; set; }
    public required string VoucherType { get; set; }
    public Decimal? Amount { get; set; }
    public string? SellDate { get; set; }
    public required string ExpirationDate { get; set; }
    public string? ActivationDate { get; set; }
    public string? RealizationDate { get; set; }
    public required string Resort { get; set; }
    public required string Status { get; set; }
    public IEnumerable<AttractionDTO>? Attractions { get; set; }
    public GuestDTO? Guest { get; set; }

}
