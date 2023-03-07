using System.Data.SqlClient;

namespace DapperPractice.Connection
{
    internal sealed class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly IConfiguration _config;

        public SqlConnectionFactory(IConfiguration config)
        {
            _config = config;
        }
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_config.GetConnectionString("default"));
        }
    }
}
