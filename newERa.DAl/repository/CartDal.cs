using NewEra.Domain.Interface;
using NewEra.Domain.Models;
using Microsoft.Data.SqlClient;
namespace NewEra.Dal
{
    public class CartDal: ICart
    {
        private readonly string _connectionString;

        public CartDal(string connectionString)
        {
            _connectionString = connectionString;
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
}