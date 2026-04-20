using System.ComponentModel.DataAnnotations;
using _1_test.Validation;
using Microsoft.AspNetCore.Mvc;

namespace _1_test.Models;

public class User
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }

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

    [Required(ErrorMessage = "Starost je obvezna.")]
    [Range(0, 120, ErrorMessage = "Starost mora biti med 0 in 120.")]
    [Display(Name = "Starost")]
    public int Age { get; set; }

    [Required(ErrorMessage = "EMSO je obvezen.")]
    [Emso]
    [Display(Name = "EMSO")]
    public string Emso { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kraj rojstva je obvezen.")]
    [Display(Name = "Kraj rojstva")]
    public string BirthPlace { get; set; } = string.Empty;

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

    [Required(ErrorMessage = "E-posta je obvezna.")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "E-posta ni v pravilnem formatu.")]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;
}
