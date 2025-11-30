using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace BDInfrastructure.Repositories
{
    public class SqlTenantRepository
    {
        private readonly string _connectionString;

        public SqlTenantRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("RealEstateDB");
        }

        public string GetTenantFullNameFromSql(int tenantId)
        {
            const string sql = "SELECT dbo.fn_GetTenantFullName(@TenantId)";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QuerySingleOrDefault<string>(sql, new { TenantId = tenantId });
            }
        }
    }
}