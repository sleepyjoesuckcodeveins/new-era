using NewEra.Domain.Models;
using NewEra.Domain.Interface;
using FuzzySharp;
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

    public Product? GetProductByName(string name)
    {
        _productRepository.getProductByname(name);
    }
    public Product? GetProductByCategory(string category)
    {
        return _productRepository.getProductBycategory(category);
    }
    public List<Product> SearchProduct(string name)
    {    
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Search term cannot be null or empty.", nameof(name));
        }
    
        var products = _productRepository.getAllProducts();

        var matchingProducts = products
        .Select(p => new { Product = p, Score = Fuzz.PartialRatio(name.ToLower(), p.Name.ToLower()) })
        .Where(r => r.Score >= 70)
        .OrderByDescending(r => r.Score)
        .Select(r => r.Product)
        .ToList();
        


        return matchingProducts;
    }

}
