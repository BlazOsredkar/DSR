using System.ComponentModel.DataAnnotations;

namespace _1_test.Models;

public class Purchase
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser? User { get; set; }

    [Required(ErrorMessage = "Datum nakupa je obvezen.")]
    [Display(Name = "Datum nakupa")]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "Skupni znesek je obvezen.")]
    [Range(0.1, 100000, ErrorMessage = "Skupni znesek mora biti vec kot 0.")]
    [Display(Name = "Skupaj")]
    public double TotalPrice { get; set; }

    [Required(ErrorMessage = "Naslov dostave je obvezen.")]
    [StringLength(200)]
    [Display(Name = "Naslov dostave")]
    public string DeliveryAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kontaktni telefon je obvezen.")]
    [StringLength(30)]
    [Display(Name = "Kontaktni telefon")]
    public string ContactPhone { get; set; } = string.Empty;

    [StringLength(200)]
    [Display(Name = "Opomba")]
    public string? Note { get; set; }

    [Required]
    [StringLength(30)]
    [Display(Name = "Status")]
    public string Status { get; set; } = "Novo";

    public List<PurchaseItem> Items { get; set; } = new();
}
