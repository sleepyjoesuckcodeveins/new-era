using NewEra.Domain.Models;
namespace NewEra.Domain.Interface
{
    public interface ICart
    {
      public Cart SaveOrder(List<Cart> CurrentCart, int userId); 
    }
}