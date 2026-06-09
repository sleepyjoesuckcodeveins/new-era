
using NewEra.Domain.Models;
namespace NewEra.Domain.Interface
{
    public interface IAdminInterface
    {
        public Product addProduct(Product newProduct);
        public List<Product> getLowestStockProducts();
        public List<Product> getAllProducts();
        public Product updateStock(int productId, int newStock);
        public Product deleteProduct(int productId);
 
    }
}