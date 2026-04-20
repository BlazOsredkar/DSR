using System.ComponentModel.DataAnnotations;
using _1_test.Validation;

namespace _1_test.Models.ViewModels;

public class RegistrationStep1ViewModel
{
    [Required(ErrorMessage = "Ime je obvezno.")]
    [StringLength(50)]
    [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Ime lahko vsebuje le crke.")]
    [Display(Name = "Ime")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Priimek je obvezen.")]
    [StringLength(50)]
    [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Priimek lahko vsebuje le crke.")]
    [Display(Name = "Priimek")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Datum rojstva je obvezen.")]
    [Display(Name = "Datum rojstva")]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime BirthDate { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "EMSO je obvezen.")]
    [Emso]
    [Display(Name = "EMSO")]
    public string Emso { get; set; } = string.Empty;

    [Required(ErrorMessage = "Starost je obvezna.")]
    [Range(0, 120, ErrorMessage = "Starost mora biti med 0 in 120.")]
    [Display(Name = "Starost")]
    public int Age { get; set; }
}
