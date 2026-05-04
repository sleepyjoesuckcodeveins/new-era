using NewEra.Domain.Interface;
using NewEra.Domain.Models;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace NewEra.Dal;

public class NewEraProducts: IProduct, Ifilterservice
{
    private readonly string _connectionString;
    public NewEraProducts(string connectionString)
    {
        _connectionString = connectionString;
    }

    private static void sqlquery(string query, string connectionString)
    {
        // Code to execute a SQL query using _connectionString and return the result as a SqlDataReader
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    // Process the data from the reader
                }
                
                
             }
             catch (SqlException ex)
            {
                System.Console.WriteLine("An error occurred while executing the SQL query: " + ex.Message);
            }              // Handle any exceptions that may occur during data processing
            
            finally
            {
                reader.Close();
            }
           
        }
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

    public List<string> getallcategories()
    {
        // Code to retrieve all categories from the database using _connectionString
        return getallcategories();
        // Return the list of categories
    }

    public List<string> getallsubcategories()
    {
        // Code to retrieve all subcategories from the database using _connectionString
        List<string> subcategories = new List<string>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT DISTINCT subcategory FROM newworld_MockData";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        subcategories.Add(reader.GetString(0));
                    }
                }
            }
        }
        return subcategories; // Return the list of subcategories
    }

    public List<Product> getProductBycategory(string selectedcategory)
    {
        // Code to retrieve products by category from the database using _connectionString
        List<Product> products = new List<Product>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT Id, Product, Price FROM newworld_MockData WHERE category = @Category";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Category", selectedcategory);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Product")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price"))
                        };
                        products.Add(product);
                    }
                }
            }
        }

        return products;
    }
     public List<Product> filterByPriceRange(decimal minPrice, decimal maxPrice)
     {
        // Code to filter products by price range from the database using _connectionString
      throw new NotImplementedException(); // Placeholder for filter by price range implementation
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
