
namespace NewEra.Domain.Models;

public class Cart
{
    int Id { get; set; }
    string ProductName { get; set; }
     int Quantity { get; set; }
    decimal TotalPrice { get; set; }
     int UserId { get; set; }
}