
using NewEra.Domain.Models;
namespace NewEra.Domain.Interface
{
    public interface IAdminInterface
    {
        public Product addProduct(Product newProduct);
        public Product getLowestStockProduct();
        public Product updateStock(int productId, int newStock);
        public void deleteProduct(int productId);
 
    }
}