using System.ComponentModel.DataAnnotations;

namespace _1_test.Models.ViewModels;

public class AdminUserCreateViewModel
{
    [Required(ErrorMessage = "E-posta je obvezna.")]
    [EmailAddress(ErrorMessage = "E-posta ni v pravilnem formatu.")]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Ime")]
    [StringLength(50)]
    public string? FirstName { get; set; }

    [Display(Name = "Priimek")]
    [StringLength(50)]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Geslo je obvezno.")]
    [DataType(DataType.Password)]
    [Display(Name = "Geslo")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ponovitev gesla je obvezna.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Gesli se ne ujemata.")]
    [Display(Name = "Ponovi geslo")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Display(Name = "Administrator")]
    public bool IsAdmin { get; set; }
}
