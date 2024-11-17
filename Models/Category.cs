namespace FiStockBackend.Models;


public class Category
{
    public int CategoryId { get; set; } // Primary Key
    public string CategoryName { get; set; }
    public List<Product> Products { get; set; } // Navigation Property
}