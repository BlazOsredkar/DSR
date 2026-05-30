using System.ComponentModel.DataAnnotations;

namespace _1_test.Models.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "E-posta je obvezna.")]
    [EmailAddress(ErrorMessage = "E-posta ni v pravilnem formatu.")]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Geslo je obvezno.")]
    [DataType(DataType.Password)]
    [Display(Name = "Geslo")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Zapomni si me")]
    public bool RememberMe { get; set; }
}
