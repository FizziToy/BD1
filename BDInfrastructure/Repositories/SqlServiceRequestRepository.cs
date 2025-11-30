using BDInfrastructure.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace BDInfrastructure.Repositories
{
    public class SqlServiceRequestRepository
    {
        private readonly string _connectionString;

        public SqlServiceRequestRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("RealEstateDB");
        }

        public IEnumerable<SqlServiceRequestResult> GetRequestsWithHistory()
        {
            const string sql = "EXEC [dbo].[sp_GetRequestsWithHistory_SQL]";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var results = connection.Query<SqlServiceRequestResult>(sql);
                return results.ToList();
            }
        }
    }
}