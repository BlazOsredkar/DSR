using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace _1_test.Models;

public class Book
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Naziv knjige je obvezen.")]
    [StringLength(150)]
    [Display(Name = "Naziv")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Avtor je obvezen.")]
    [StringLength(100)]
    [Display(Name = "Avtor")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cena je obvezna.")]
    [Range(0.1, 500, ErrorMessage = "Cena mora biti med 0,1 in 500.")]
    [Display(Name = "Cena")]
    public double Price { get; set; }

    [Required(ErrorMessage = "Stevilo strani je obvezno.")]
    [Range(1, 2000, ErrorMessage = "Stevilo strani mora biti med 1 in 2000.")]
    [Display(Name = "Stevilo strani")]
    public int PageCount { get; set; }

    [Required(ErrorMessage = "Datum izdaje je obvezen.")]
    [Display(Name = "Datum izdaje")]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime PublishDate { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Zaloga je obvezna.")]
    [Range(0, 1000, ErrorMessage = "Zaloga mora biti med 0 in 1000.")]
    [Display(Name = "Zaloga")]
    public int Stock { get; set; }
}
