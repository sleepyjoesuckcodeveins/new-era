using NewEra.Domain.Interface;
using NewEra.Domain.Models;
using Microsoft.Data.SqlClient;

namespace NewEra.Dal;
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
        string query = "INSERT INTO Users (Username, Password, Email, Role) VALUES (@Username, @Password, @Email, @Role)";
        ExecuteNonReadableQuery(_connectionString, query, cmd =>
        {
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Role", user.Role);
        });
    }

    public User? GetUser(User user)
    {
        // Code to retrieve a user from the database using _connectionString
        string query = "SELECT id, Username, Password, Email, Role FROM Users WHERE Email = @Email";
        // Implementation to execute the query and populate the user object
        return ExecuteSqlQuery(_connectionString, query, reader => new User
        {
            Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
            Username = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
            Password = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
            Email = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
            Role = reader.IsDBNull(4) ? string.Empty : reader.GetString(4)
        }, user);
    }


    private void ExecuteNonReadableQuery(string connectionString, string query, Action<SqlCommand> parameterize)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            parameterize(cmd);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
    private User? ExecuteSqlQuery(string connectionString, string query, Func<SqlDataReader, User> map, User user)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@Email", user.Email); // Assuming the query has a parameter for email
            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return map(reader);
                }
            }
        }
        return null;
    }    


}