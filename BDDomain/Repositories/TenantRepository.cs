using BDDomain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BDDomain.Repositories
{
    public class TenantRepository
    {
        private readonly RealEstateDbContext _context;

        public TenantRepository(RealEstateDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tenant> GetActiveTenants()
        {
            return _context.Tenants.FromSqlRaw("EXEC sp_GetActiveTenants").ToList();
        }

        public int AddTenant(string fullName, string phone, string email, int createdBy)
        {
            var result = _context.Database.SqlQueryRaw<int>(
                "EXEC sp_AddTenant @p0, @p1, @p2, @p3",
                parameters: new object[] { fullName, phone, email, createdBy }
            ).AsEnumerable().FirstOrDefault();

            return result;
        }

        public void DeleteTenantSoft(int tenantId, int updatedBy)
        {
            _context.Database.ExecuteSqlRaw("EXEC sp_DeleteTenantSoft @p0, @p1", tenantId, updatedBy);
        }
    }
}
