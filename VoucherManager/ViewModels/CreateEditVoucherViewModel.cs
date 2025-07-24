using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using VoucherManager.Data;
using VoucherManager.Helpers;

namespace VoucherManager.ViewModels;

public class CreateEditVoucherViewModel
{
    [Required(ErrorMessage = "Numer seryjny jest wymagany.")]
    [StringLength(50, ErrorMessage = "Numer seryjny nie może przekraczać 50 znaków.")]
    public string SerialNumber { get; set; } = null!;

    [Required(ErrorMessage = "Typ vouchera jest wymagany.")]
    public VoucherType VoucherType { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Kwota musi być większa od zera.")]
    public decimal? Amount { get; set; }

    [DataType(DataType.Date)]
    public DateTime? SellDate { get; set; }
   
    [DataType(DataType.Date)]
    public DateTime? ActivationDate { get; set; }

    [Required(ErrorMessage = "Data ważności jest wymagana.")]
    [DataType(DataType.Date)]
    public DateTime ExpirationDate { get; set; }

        [DataType(DataType.Date)]
    public DateTime? RealizationDate { get; set; }

    [Required(ErrorMessage = "Ośrodek jest wymagany.")]
    [StringLength(100, ErrorMessage = "Nazwa ośrodka nie może przekraczać 100 znaków.")]
    public string Resort { get; set; } = null!;

    [Required(ErrorMessage = "Status jest wymagany.")]
    public Status Status { get; set; }

    [StringLength(50, ErrorMessage = "Numer faktury nie może przekraczać 50 znaków.")]
    public string? InovoiceNumber { get; set; }
    public List<SelectListItem> StatusList { get; set; } = Status.Nieaktywny.ToSelectList().ToList();
    public List<SelectListItem> VoucherTypeList { get; set; } = VoucherType.Pobytowy.ToSelectList().ToList();
}
