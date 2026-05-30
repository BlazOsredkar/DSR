using System.ComponentModel.DataAnnotations;

namespace _1_test.Models.Dto;

public class PurchaseItemDto
{
    [Required]
    public int BookId { get; set; }

    [Required]
    [Range(1, 1000)]
    public int Quantity { get; set; }
}

public class PurchaseCreateDto
{
    [Required]
    [StringLength(200)]
    public string DeliveryAddress { get; set; } = string.Empty;

    [Required]
    [StringLength(30)]
    public string ContactPhone { get; set; } = string.Empty;

    [StringLength(200)]
    public string? Note { get; set; }

    [Required]
    [MinLength(1)]
    public List<PurchaseItemDto> Items { get; set; } = new();
}

public class PurchaseItemReadDto
{
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public double UnitPrice { get; set; }
    public int Quantity { get; set; }
    public double LineTotal { get; set; }
}

public class PurchaseReadDto
{
    public int Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public double TotalPrice { get; set; }
    public string Status { get; set; } = string.Empty;
    public string DeliveryAddress { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public string? Note { get; set; }
    public List<PurchaseItemReadDto> Items { get; set; } = new();
}
