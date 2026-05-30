namespace _1_test.Models;

public class CartItem
{
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public double UnitPrice { get; set; }
    public int Quantity { get; set; }

    public double LineTotal => UnitPrice * Quantity;
}
