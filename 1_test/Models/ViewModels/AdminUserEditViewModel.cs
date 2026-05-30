using System.ComponentModel.DataAnnotations;

namespace _1_test.Models.ViewModels;

public class AdminUserEditViewModel
{
    [Required]
    public string Id { get; set; } = string.Empty;

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

    [DataType(DataType.Password)]
    [Display(Name = "Novo geslo")]
    public string? NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Gesli se ne ujemata.")]
    [Display(Name = "Ponovi novo geslo")]
    public string? ConfirmPassword { get; set; }

    [Display(Name = "Administrator")]
    public bool IsAdmin { get; set; }
}
