using NewEra.Domain.Models;
namespace NewEra.Domain.Interface
{
    public interface ICart
    {
       public void SaveOrder(List<Cart> currentCart, int userId);
 
    }
}