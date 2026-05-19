
using NewEra.Domain.Models;
namespace NewEra.Domain.Interface;

public interface IManageCartProduct
{
    public void addProduct(Product product);
    public Cart OrderProducts(List<Cart> cartItems, int userId);   
    public void updateProduct(Product product);
    public void deleteProduct(int id);
}