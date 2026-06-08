
using System.Collections.Generic;
using System.Linq;
using NewEra.Domain.Interface;
using NewEra.Domain.Models;

namespace NewEra.BLL
{
    public class Adminservice
    {
        private readonly IAdminInterface _adminRepository;

        public Adminservice(IAdminInterface adminRepository)
        {
            _adminRepository = adminRepository;
        }
          public Product addProduct(Product newProduct){
            return _adminRepository.addProduct(newProduct);
            }
        public List<Product> getLowestStockProducts()
        {
            return _adminRepository.getLowestStockProducts();
        }
        public Product updateStock(int productId, int newStock){
            return _adminRepository.updateStock(productId, newStock);
        }
        public void deleteProduct(int productId)
        {
            _adminRepository.deleteProduct(productId);
        }
            }
 
}