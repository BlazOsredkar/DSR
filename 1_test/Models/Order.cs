using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace _1_test.Models;

public class Order
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Datum narocila je obvezen.")]
    [Display(Name = "Datum narocila")]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime OrderDate { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Kolicina je obvezna.")]
    [Range(1, 100, ErrorMessage = "Kolicina mora biti med 1 in 100.")]
    [Display(Name = "Kolicina")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Skupaj je obvezno.")]
    [Range(0.1, 10000, ErrorMessage = "Skupaj mora biti med 0,1 in 10000.")]
    [Display(Name = "Skupaj")]
    public double Total { get; set; }

    [Required(ErrorMessage = "Status je obvezen.")]
    [StringLength(50)]
    [Display(Name = "Status")]
    public string Status { get; set; } = "Novo";

    [Display(Name = "Uporabnik")]
    public int UserId { get; set; }
    public User? User { get; set; }

    [Display(Name = "Knjiga")]
    public int BookId { get; set; }
    public Book? Book { get; set; }
}
