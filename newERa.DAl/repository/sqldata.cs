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

    public static List<T> ReadableSqlQuery<T>(string connectionString, string query, Func<SqlDataReader, T> map)
    {
        List<T> results = new List<T>();

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
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
        // Code to retrieve all products from the database using _connectionString
    string query = "SELECT Id, Product, Price FROM newworld_MockData";

    return ReadableSqlQuery(_connectionString, query, reader => new Product
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            Price = reader.GetDecimal(2)
        });    
    }
    public void getProductByname(string name)
    {
        // Code to retrieve a product by name from the database using _connectionString
        string query = "SELECT Id, Product, Price FROM newworld_MockData WHERE Product = @Name";
        
    }

    public Product? getProductById(int id)
    {
        // Code to retrieve a product by ID from the database using _connectionString
        string query = "SELECT Id, Product, Price FROM newworld_MockData WHERE Id = @Id";

        return null; // Placeholder return statement
    }
    public void addProduct(Product product)
    {
        // Code to add a new product to the database using _connectionString
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();  
        string query = "INSERT INTO newworld_MockData (Product, Price, quantity, category, subcategory) VALUES (@Product, @Price, @Quantity, @Category, @Subcategory)";
        
         }
    }
    
    public List<Product> searchProduct(string name)
    {
        // Code to search for products by name in the database using _connectionString
       throw new NotImplementedException(); // Placeholder for search product implementation
    }
    public Cart SaveOrder(List<Cart> CurrentCart, int userId)
    {
        // Code to process the order for the products in the cart
        string query = "INSERT INTO Orders (UserId, ProductName, Quantity, TotalPrice) VALUES (@UserId, @ProductName, @Quantity, @TotalPrice)";
        SqlHelper.ExecuteNonReadableQuery(_connectionString, query, cmd =>
        {
            foreach (var item in CurrentCart)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@ProductName", item.ProductName);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                cmd.Parameters.AddWithValue("@TotalPrice", item.TotalPrice);
                cmd.ExecuteNonQuery();
            }
        });
        return new Cart(); // Placeholder return statement

    }
}
