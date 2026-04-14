using NewEra.Domain.Models;
using NewEra.Domain.Interface;
namespace NewEra.BLL;

public class NeweraProductService
{
    private readonly IProduct _productRepository;

    public NeweraProductService(IProduct productRepository)
    {
        _productRepository = productRepository;
    }

    public List<Product> GetAllProducts()
    {
        return _productRepository.getAllProducts();
    }

    public Product? GetProductById(int id)
    {
        return _productRepository.getProductById(id);
    }

    public void GetProductByName(string name)
    {
        _productRepository.getProductByname(name);
    }
    public List<Product> SearchProduct(string name)
    {
        return _productRepository.searchProduct(name);
    }

}
