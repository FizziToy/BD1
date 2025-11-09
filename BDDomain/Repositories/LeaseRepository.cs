using BDDomain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BDDomain.Repositories
{
    public class LeaseRepository
    {
        private readonly RealEstateDbContext _context;

        public LeaseRepository(RealEstateDbContext context)
        {
            _context = context;
        }

        public IEnumerable<VwActiveLease> GetActiveLeases()
        {
            return _context.VwActiveLeases
                .FromSqlRaw("EXEC sp_GetActiveLeases")
                .ToList();
        }
    }
}
