using NewEra.Domain.Interface;
using NewEra.Domain.Models;

namespace NewEra.DAl.repository
{
    public class Admindal : IAdminInterface
    {
        private readonly string _connectionString;
        public Admindal(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Product addProduct(Product newProduct)
        {
            string query = "INSERT INTO newworld_mockdata (Product, Price, Quantity_of_product, Category, Sub_Category) VALUES (@Product, @Price, @Quantity, @Category, @Subcategory)";
            
            SqlHelper.Executecommand(_connectionString, query, cmd => 
            {
                
                cmd.Parameters.AddWithValue("@Product", newProduct.Name);
                cmd.Parameters.AddWithValue("@Price", newProduct.Price);
                cmd.Parameters.AddWithValue("@Quantity", newProduct.Stock);
                cmd.Parameters.AddWithValue("@Category", newProduct.Category);
                cmd.Parameters.AddWithValue("@Subcategory", newProduct.SubCategory);
            });
            return newProduct;
        }

        public Product deleteProduct(int productId)
        {
            string query = "DELETE FROM newworld_mockdata WHERE ProductID = @ProductId";
            
            SqlHelper.Executecommand(_connectionString, query, cmd => 
            {
                cmd.Parameters.AddWithValue("@ProductId", productId);
            });
            return new Product { Id = productId };
        }

        public Product getLowestStockProduct()
        {
            string query = "SELECT top 10 *FROM products WHERE quantity <= 5 order by quantity asc ";
            return SqlHelper.ReadSingle(_connectionString, query, reader => new Product
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2),
                Stock = reader.GetInt32(3),
                Category = reader.GetString(4),
                SubCategory = reader.GetString(5)
            }, null);
        }

        public Product updateStock(int productId, int newStock)
        {
            string query = "UPDATE newworld_mockdata SET Quantity_of_product = @NewStock WHERE ProductID = @ProductId";
            
            SqlHelper.Executecommand(_connectionString, query, cmd => 
            {
                cmd.Parameters.AddWithValue("@NewStock", newStock);
                cmd.Parameters.AddWithValue("@ProductId", productId);
            });
            return new product { Id = productId, Stock = newStock };
        }
    }
}