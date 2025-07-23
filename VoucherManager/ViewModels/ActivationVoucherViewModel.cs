using System.ComponentModel.DataAnnotations;

namespace VoucherManager.ViewModels;

public class ActivationVoucherViewModel
{
    [Required(ErrorMessage = "Numer voucheru jest wymagany")]
    [Display(Name = "Numer voucheru")]
    public string SerialNumber { get; set; } = null!;
    [Required(ErrorMessage = "Adres e-mail jest wymagany")]
    [EmailAddress(ErrorMessage = "Podany adres e-mail jest nieprawidłowy")]
    [Display(Name = "Adres e-mail")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "Numer telefonu jest wymagany")]
    [RegularExpression(@"^\d{9}$", ErrorMessage = "Numer telefonu musi zawierać dokładnie 9 cyfr.")]
    [Display(Name = "Numer telefonu")]
    public string PhoneNumber { get; set; } = null!;
    public string? InvoiceNumber { get; set; } = null!;
}
