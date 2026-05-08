namespace NewEra.Domain.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; }
    public string Subcategory { get; set; }
    public decimal totalPrice { get; set; }


}
