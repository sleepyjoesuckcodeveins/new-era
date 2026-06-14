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
            string query = "INSERT INTO Product (Product, Price, Quantity_of_product, SubCategoryID) VALUES (@Product, @Price, @Quantity, @SubCategoryID)";
            
            SqlHelper.Executecommand(_connectionString, query, cmd => 
            {
                
                cmd.Parameters.AddWithValue("@Product", newProduct.Name);
                cmd.Parameters.AddWithValue("@Price", newProduct.Price);
                cmd.Parameters.AddWithValue("@Quantity", newProduct.Quantity);
                cmd.Parameters.AddWithValue("@SubCategoryID", newProduct.SubcategoryID);
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
                string query = @"
                SELECT TOP 5
                    p.ProductID,
                    p.Product,
                    p.Price,
                    p.Quantity_of_product,
                    sc.SubCategoryName,
                    c.CategoryName,
                    p.SubCategoryID
                FROM Product p
                JOIN SubCategory sc ON p.SubCategoryID = sc.SubCategoryID
                JOIN Category c ON sc.CategoryID = c.CategoryID
                WHERE p.Quantity_of_product <= 5
                ORDER BY p.Quantity_of_product ASC;";
            return SqlHelper.ReadableSqlQuery(_connectionString, query, reader => new Product
            {
                Id = reader.IsDBNull(0) ? 0 : Convert.ToInt32(reader.GetValue(0)),
                Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                Price = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                Quantity = reader.IsDBNull(3) ? 0 : Convert.ToInt32(reader.GetValue(3)),
                Subcategory = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                Category = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                SubcategoryID = reader.IsDBNull(6) ? 0 : Convert.ToInt32(reader.GetValue(6))
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
                    //this need to be reworked first to get the subcatoryid based on the subcategory name and then get the category id based on the subcategory id
                    string query = @"
                        SELECT 
                            p.ProductID,
                            p.Product,
                            p.Price,
                            p.Quantity_of_product,
                            sc.SubCategoryName,
                            c.CategoryName,
                            p.SubCategoryID
                        FROM Product p
                        JOIN SubCategory sc ON p.SubCategoryID = sc.SubCategoryID
                        JOIN Category c ON sc.CategoryID = c.CategoryID";

                return SqlHelper.ReadableSqlQuery(_connectionString, query, reader => new Product
                {
                    Id = reader.IsDBNull(0) ? 0 : Convert.ToInt32(reader.GetValue(0)),
                    Name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    Price = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                    Quantity = reader.IsDBNull(3) ? 0 : Convert.ToInt32(reader.GetValue(3)),
                    Subcategory = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                    Category = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                    SubcategoryID = reader.IsDBNull(6) ? 0 : Convert.ToInt32(reader.GetValue(6))
                });    
                }
            }
}