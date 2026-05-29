
namespace NewEra.Domain.Models;

public class Cart
{
    public int Id { get; set; }
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Price { get; set; }
    public int UserId { get; set; }
}