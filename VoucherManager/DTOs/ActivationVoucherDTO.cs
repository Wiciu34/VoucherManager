using System.ComponentModel.DataAnnotations;

namespace VoucherManager.DTOs;

public class ActivationVoucherDTO
{
    [Required]
    public required string InvoiceNumber { get; set; }
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    [Phone]
    public required string PhoneNumber { get; set; }
}
