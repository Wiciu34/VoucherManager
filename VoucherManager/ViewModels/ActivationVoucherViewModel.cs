using System.ComponentModel.DataAnnotations;

namespace VoucherManager.ViewModels;

public class ActivationVoucherViewModel
{
    [Required(ErrorMessage = "Numer voucheru jest wymagany")]
    [Display(Name = "Numer voucheru")]
    public required string SerialNumber { get; set; }
    [Required(ErrorMessage = "Adres e-mail jest wymagany")]
    [EmailAddress(ErrorMessage = "Podany adres e-mail jest nieprawidłowy")]
    [Display(Name = "Adres e-mail")]
    public required string Email { get; set; }
    [Required(ErrorMessage = "Numer telefonu jest wymagany")]
    [Phone(ErrorMessage = "Podany numer telefonu jest nieprawidłowy")]
    [Display(Name = "Numer telefonu")]
    public required string PhoneNumber { get; set; }
    public string? InvoiceNumber { get; set; }
}
