using NewEra.Domain.Models;
namespace NewEra.Domain.Interface;
public interface IProduct
{
    public void getProductByname(string name);
    public List<Product> getAllProducts();

    public Product? getProductById(int id);
    
   
}
