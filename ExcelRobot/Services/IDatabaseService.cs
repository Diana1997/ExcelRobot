using System.Data.SqlClient;

namespace ExcelRobot.Services;

public interface IDatabaseService
{
    public SqlConnection OpenConnection(string connectionString);
}