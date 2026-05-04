using NewEra.Domain.Models;
namespace NewEra.Domain.Interface;
public interface IProduct
{
    public void getProductByname(string name);
    public List<string> getallcategories();
    public List<Product> getAllProducts();

    public List<Product> searchProduct(string name);
    
   
}
