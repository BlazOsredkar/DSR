using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace _1_test.Models;

public class ApplicationUser : IdentityUser
{
    [StringLength(50)]
    [Display(Name = "Ime")]
    public string? FirstName { get; set; }

    [StringLength(50)]
    [Display(Name = "Priimek")]
    public string? LastName { get; set; }

    [Display(Name = "Datum rojstva")]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? BirthDate { get; set; }

    [Display(Name = "Starost")]
    public int? Age { get; set; }

    [Display(Name = "EMSO")]
    [StringLength(13)]
    public string? Emso { get; set; }

    [Display(Name = "Kraj rojstva")]
    [StringLength(100)]
    public string? BirthPlace { get; set; }

    [Display(Name = "Naslov")]
    [StringLength(100)]
    public string? Address { get; set; }

    [Display(Name = "Postna stevilka")]
    public int? PostalCode { get; set; }

    [Display(Name = "Posta")]
    [StringLength(100)]
    public string? City { get; set; }

    [Display(Name = "Drzava")]
    [StringLength(100)]
    public string? Country { get; set; }
}
