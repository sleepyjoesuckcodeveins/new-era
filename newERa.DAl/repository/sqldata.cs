using NewEra.Domain.Interface;
using NewEra.Domain.Models;
using Microsoft.Data.SqlClient;

namespace NewEra.Dal;

public class NewEraProducts: IProduct, ICart
{
    private readonly string _connectionString;
    public NewEraProducts(string connectionString)
    {
        _connectionString = connectionString;
    }

    public static List<T> ReadableSqlQuery<T>(string connectionString, string query, Func<SqlDataReader, T> map, Action<SqlParameterCollection> addParameters = null)
    {
        List<T> results = new List<T>();

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            addParameters?.Invoke(cmd.Parameters);
            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    results.Add(map(reader));
                }
            }
        }

        return results;
    }

   public List<Product> getAllProducts()
    {
        string query = "SELECT ProductID, Product, Price FROM newworld_mockdata";

        return ReadableSqlQuery(_connectionString, query, reader => new Product
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            Price = reader.GetDecimal(2)
        });    
    }
    
    public Product? getProductByname(string name)
    {
        string query = "SELECT ProductID, Product, Price FROM newworld_mockdata WHERE Product = @Name";
        
        var products = ReadableSqlQuery(_connectionString, query, 
            reader => new Product
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2)
            }, 
            p => p.AddWithValue("@Name", name));

        return products.FirstOrDefault();
    }

    public Product? getProductById(int id)
    {
        string query = "SELECT ProductID, Product, Price FROM newworld_mockdata WHERE ProductID = @Id";
        
        var products = ReadableSqlQuery(_connectionString, query, 
            reader => new Product
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2)
            }, 
            p => p.AddWithValue("@Id", id));

        return products.FirstOrDefault();
    }

    public void addProduct(Product product)
    {
        string query = "INSERT INTO newworld_mockdata (Product, Price, Quantity_of_product, Category, Sub_Category) VALUES (@Product, @Price, @Quantity, @Category, @Subcategory)";
        
        SqlHelper.ExecuteNonReadableQuery(_connectionString, query, cmd => 
        {
            cmd.Parameters.AddWithValue("@Product", product.Name);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@Quantity", product.Quantity); // Assuming Product model has Quantity
            cmd.Parameters.AddWithValue("@Category", product.Category); // Assuming Product model has Category
            cmd.Parameters.AddWithValue("@Subcategory", product.Subcategory); // Assuming Product model has SubCategory
            cmd.ExecuteNonQuery();
        });
    }
    
    public List<Product> searchProduct(string name)
    {
        string query = "SELECT ProductID, Product, Price FROM newworld_mockdata WHERE Product LIKE @Name";
        
        return ReadableSqlQuery(_connectionString, query, 
            reader => new Product
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2)
            }, 
            p => p.AddWithValue("@Name", "%" + name + "%"));
    }

    public void SaveOrder(List<Cart> currentCart, int userId)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                decimal totalOrderPrice = currentCart.Sum(item => item.TotalPrice);
                int totalQuantity = currentCart.Sum(item => item.Quantity);

                string orderQuery = "INSERT INTO orders (user_id, quantity, total_price, order_status, order_date) " +
                                    "OUTPUT INSERTED.order_id " +
                                    "VALUES (@UserId, @Quantity, @TotalPrice, 'Pending', GETDATE());";

                int orderId;
                using (SqlCommand orderCmd = new SqlCommand(orderQuery, connection, transaction))
                {
                    orderCmd.Parameters.AddWithValue("@UserId", userId);
                    orderCmd.Parameters.AddWithValue("@Quantity", totalQuantity);
                    orderCmd.Parameters.AddWithValue("@TotalPrice", totalOrderPrice);
                    orderId = (int)orderCmd.ExecuteScalar();
                }

                string orderItemQuery = "INSERT INTO order_items (order_id, ProductID, quantity, price, ProductName) " +
                                        "VALUES (@OrderId, @ProductId, @Quantity, @Price, @ProductName);";

                foreach (var item in currentCart)
                {
                    using (SqlCommand itemCmd = new SqlCommand(orderItemQuery, connection, transaction))
                    {
                        itemCmd.Parameters.AddWithValue("@OrderId", orderId);
                        itemCmd.Parameters.AddWithValue("@ProductId", item.ProductID);
                        itemCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                        itemCmd.Parameters.AddWithValue("@Price", item.Price);
                        itemCmd.Parameters.AddWithValue("@ProductName", item.ProductName);
                        itemCmd.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw; 
            }
        }
    }
}