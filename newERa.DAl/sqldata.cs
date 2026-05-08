using NewEra.Domain.Interface;
using NewEra.Domain.Models;
using Microsoft.Data.SqlClient;

namespace NewEra.Dal;

public class NewEraProducts: IProduct
{
    private readonly string _connectionString;
    public NewEraProducts(string connectionString)
    {
        _connectionString = connectionString;
    }
   public List<Product> getAllProducts()
    {
        // Code to retrieve all products from the database using _connectionString
        List<Product> products = new List<Product>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT Id, Product, Price FROM newworld_MockData";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2)
                        };
                        products.Add(product);
                    }
                }
            }
        }
        return products; // Return the list of products
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

}
