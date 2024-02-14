using Microsoft.Data.SqlClient;
using System.Data;

namespace SmartWayTest.Models.Data
{
    public class DapperDBContext
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public DapperDBContext(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
