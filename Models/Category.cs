namespace FiStockBackend.Models;

public class Category
{
    public int CategoryId { get; set; } // Primary key
    public required string CategoryName { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}