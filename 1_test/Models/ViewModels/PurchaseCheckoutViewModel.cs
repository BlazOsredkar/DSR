using System.ComponentModel.DataAnnotations;

namespace _1_test.Models.ViewModels;

public class PurchaseCheckoutViewModel
{
    [Required(ErrorMessage = "Naslov dostave je obvezen.")]
    [StringLength(200)]
    [Display(Name = "Naslov dostave")]
    public string DeliveryAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kontaktni telefon je obvezen.")]
    [StringLength(30)]
    [Display(Name = "Kontaktni telefon")]
    public string ContactPhone { get; set; } = string.Empty;

    [StringLength(200)]
    [Display(Name = "Opomba")]
    public string? Note { get; set; }
}
