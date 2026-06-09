using Microsoft.Data.SqlClient;
namespace NewEra.Dal
{
    public static class SqlHelper
    {
    public static void Executecommand(string connectionString, string query, Action<SqlCommand> parameterize)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            parameterize(cmd);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static T? ReadSingle<T>(string connectionString, string query, Func<SqlDataReader, T> map, Action<SqlParameterCollection>? addParameters = null)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            // Assuming the query has a parameter for email
            addParameters?.Invoke(cmd.Parameters);
            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return map(reader);
                }
            }
        }
        return default; 
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
    }
    

}