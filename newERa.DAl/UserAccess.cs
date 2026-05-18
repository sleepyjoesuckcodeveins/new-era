using NewEra.Domain.Interface;
using NewEra.Domain.Models;
using Microsoft.Data.SqlClient;

namespace NewEra.DAL;
public class UserAccess : IUserManagement
{
    private readonly string _connectionString;

    public UserAccess(string connectionString)
    {
        _connectionString = connectionString;
    }
    public void RegisterUser(User user)
    {
        // Code to register a new user in the database using _connectionString
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Users (Username, Password, Email, Role) VALUES (@Username, @Password, @Email, @Role)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Role", user.Role);
                command.ExecuteNonQuery();
            }
        }
    }
    public 


}