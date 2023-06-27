using System.Data.SqlClient;

namespace ExcelRobot.Services;

public class DatabaseService : IDatabaseService
{
    public SqlConnection OpenConnection(string connectionString)
    {
        SqlConnection connection = null;
        try
        {
            connection = new SqlConnection(connectionString);
            return connection;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            connection?.Close();
        }
    }
}