using NewEra.Domain.Models;

namespace NewEra.Domain.Interface;
public interface Ifilterservice
{
 public List<Product> getProductBycategory(string selectedCategory);
 public List<Product> filterByPriceRange(decimal minPrice, decimal maxPrice);
   
}