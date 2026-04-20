using System.ComponentModel.DataAnnotations;

namespace _1_test.Models.ViewModels;

public class RegistrationStep3ViewModel
{
    [Required(ErrorMessage = "E-posta je obvezna.")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "E-posta ni v pravilnem formatu.")]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Geslo je obvezno.")]
    [DataType(DataType.Password)]
    [RegularExpression("^(?=.*[0-9])(?=.*[^A-Za-z0-9]).{6,}$", ErrorMessage = "Geslo mora imeti vsaj eno stevilko in poseben znak.")]
    [Display(Name = "Geslo")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ponovitev gesla je obvezna.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Gesli se ne ujemata.")]
    [Display(Name = "Ponovi geslo")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
