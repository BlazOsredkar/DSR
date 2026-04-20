using System.ComponentModel.DataAnnotations;

namespace _1_test.Models.ViewModels;

public class RegistrationStep2ViewModel
{
    [Required(ErrorMessage = "Naslov je obvezen.")]
    [StringLength(100)]
    [Display(Name = "Naslov")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Postna stevilka je obvezna.")]
    [Range(1000, 9999, ErrorMessage = "Postna stevilka mora imeti 4 stevke.")]
    [Display(Name = "Postna stevilka")]
    public int PostalCode { get; set; }

    [Required(ErrorMessage = "Posta je obvezna.")]
    [StringLength(100)]
    [Display(Name = "Posta")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Drzava je obvezna.")]
    [StringLength(100)]
    [Display(Name = "Drzava")]
    public string Country { get; set; } = string.Empty;
}
