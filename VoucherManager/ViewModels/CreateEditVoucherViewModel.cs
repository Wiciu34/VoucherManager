using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using VoucherManager.Data;
using VoucherManager.Helpers;

namespace VoucherManager.ViewModels;

public class CreateEditVoucherViewModel
{
    [Display(Name = "Numer seryjny vouchera")]
    [Required(ErrorMessage = "Numer seryjny jest wymagany.")]
    [StringLength(50, ErrorMessage = "Numer seryjny nie może przekraczać 50 znaków.")]
    public string SerialNumber { get; set; } = null!;

    [Display(Name = "Typ voucher")]
    [Required(ErrorMessage = "Typ vouchera jest wymagany.")]
    public VoucherType VoucherType { get; set; }

    [Display(Name = "Kwota")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Kwota musi być większa od zera.")]
    public decimal? Amount { get; set; }

    [Display(Name = "Data sprzedaży")]
    [DataType(DataType.Date)]
    public DateTime? SellDate { get; set; }

    [Display(Name = "Data aktywacji")]
    [DataType(DataType.Date)]
    public DateTime? ActivationDate { get; set; }

    [Display(Name = "Data ważności")]
    [Required(ErrorMessage = "Data ważności jest wymagana.")]
    [DataType(DataType.Date)]
    public DateTime ExpirationDate { get; set; }

    [Display(Name = "Data realizacji")]
    [DataType(DataType.Date)]
    public DateTime? RealizationDate { get; set; }

    [Display(Name = "Ośrodek")]
    [Required(ErrorMessage = "Ośrodek jest wymagany.")]
    [StringLength(100, ErrorMessage = "Nazwa ośrodka nie może przekraczać 100 znaków.")]
    public string Resort { get; set; } = null!;

    [Required(ErrorMessage = "Status jest wymagany.")]
    public Status Status { get; set; }

    [Display(Name = "Numer faktury")]
    [StringLength(50, ErrorMessage = "Numer faktury nie może przekraczać 50 znaków.")]
    public string? InovoiceNumber { get; set; }
    public int? GuestId { get; set; }
    public List<SelectListItem> StatusList { get; set; } = Status.Nieaktywny.ToSelectList().ToList();
    public List<SelectListItem> VoucherTypeList { get; set; } = VoucherType.Pobytowy.ToSelectList().ToList();
}
