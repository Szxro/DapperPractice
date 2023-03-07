using System.Data.SqlClient;

namespace DapperPractice.Connection
{
    public interface ISqlConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}
