using System.ComponentModel.DataAnnotations;

namespace _1_test.Models;

public class PurchaseItem
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }

    [Required]
    public int PurchaseId { get; set; }
    public Purchase? Purchase { get; set; }

    [Required]
    public int BookId { get; set; }
    public Book? Book { get; set; }

    [Required]
    [Range(1, 1000, ErrorMessage = "Kolicina mora biti med 1 in 1000.")]
    [Display(Name = "Kolicina")]
    public int Quantity { get; set; }

    [Required]
    [Range(0.1, 100000, ErrorMessage = "Cena mora biti vec kot 0.")]
    [Display(Name = "Cena")]
    public double UnitPrice { get; set; }
}
