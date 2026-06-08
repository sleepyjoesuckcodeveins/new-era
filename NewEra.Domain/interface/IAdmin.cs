
using NewEra.Domain.Models;
namespace NewEra.Domain.Interface
{
    public interface IAdmin
    {
        public product addProduct(product newProduct);
        public product getLowestStockProduct();
        public product updateStock(int productId, int newStock);
        public void deleteProduct(int productId);
 
    }
}