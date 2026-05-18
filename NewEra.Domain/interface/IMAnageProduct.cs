
using NewEra.Domain.Models;
namespace NewEra.Domain.Interface;

public interface IManageProduct
{
    public void addProduct(Product product);
    public void updateProduct(Product product);
    public void deleteProduct(int id);
}