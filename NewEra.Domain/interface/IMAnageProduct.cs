
using NewEra.Domain.Models;
namespace NewEra.Domain.Interface;

public interface IManageCartProduct
{
    public List<Cart> addProduct(List<Cart> current, Product product, int quantity, int userId);
    
    public void FinalizeOrder(List<Cart> currentCart, int userId, bool transactionSuccess);
}