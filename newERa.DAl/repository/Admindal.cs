using NewEra.Domain.Interface;
using NewEra.Domain.Models;
using NewEra.Dal;

namespace NewEra.DAl.repository
{
    public class Admindal : IAdminInterface  {
        private readonly string _connectionString;
        public Admindal(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Product addProduct(Product newProduct)
        {
            string query = "INSERT INTO Product (Product, Price, Quantity_of_product, Category, Sub_Category) VALUES (@Product, @Price, @Quantity, @Category, @Subcategory)";
            
            SqlHelper.Executecommand(_connectionString, query, cmd => 
            {
                
                cmd.Parameters.AddWithValue("@Product", newProduct.Name);
                cmd.Parameters.AddWithValue("@Price", newProduct.Price);
                cmd.Parameters.AddWithValue("@Quantity", newProduct.Quantity);
                cmd.Parameters.AddWithValue("@Category", newProduct.Category);
                cmd.Parameters.AddWithValue("@Subcategory", newProduct.Subcategory);
            });
            return newProduct;
        }

        public Product deleteProduct(int productId)
        {
            string query = "DELETE FROM Product WHERE ProductID = @ProductId";
            
            SqlHelper.Executecommand(_connectionString, query, cmd => 
            {
                cmd.Parameters.AddWithValue("@ProductId", productId);
            });
            return new Product { Id = productId };
        }

        public List<Product> getLowestStockProducts()
        {
            string query = "SELECT TOP 5 ProductID, Product, Price, Quantity_of_product, Category, Sub_Category FROM Product WHERE Quantity_of_product <= 5";
            return SqlHelper.ReadableSqlQuery(_connectionString, query, reader => new Product
            {
                Id = Convert.ToInt32(reader.GetValue(0)),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2),
                Quantity = Convert.ToInt32(reader.GetValue(3)),
                Category = reader.GetString(4),
                Subcategory = reader.GetString(5)
            });
        }

        public Product updateStock(int productId, int newStock)
        {
            string query = "UPDATE Product SET Quantity_of_product = @NewStock WHERE ProductID = @ProductId";
            
            SqlHelper.Executecommand(_connectionString, query, cmd => 
            {
                cmd.Parameters.AddWithValue("@NewStock", newStock);
                cmd.Parameters.AddWithValue("@ProductId", productId);
            });
            return new Product { Id = productId, Quantity = newStock };
        }
        public List<Product> getAllProducts()
        {
            string query = "SELECT ProductID, Product, Price, Quantity_of_product, Category, Sub_Category FROM Product";

            return SqlHelper.ReadableSqlQuery(_connectionString, query, reader => new Product
            {
                Id = reader.IsDBNull(0)?0:Convert.ToInt32(reader.GetValue(0)),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2),
                Quantity = reader.IsDBNull(3)?0:Convert.ToInt32(reader.GetValue(3)),
                Category = reader.GetString(4),
                Subcategory = reader.GetString(5)
            });    
        }
    }
}