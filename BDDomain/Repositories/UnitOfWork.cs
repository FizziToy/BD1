using BDDomain.Models;

namespace BDDomain.Repositories
{
    public class UnitOfWork
    {
        private readonly RealEstateDbContext _context;
        public TenantRepository Tenants { get; }
        public LeaseRepository Leases { get; }

        public UnitOfWork(RealEstateDbContext context)
        {
            _context = context;
            Tenants = new TenantRepository(context);
            Leases = new LeaseRepository(context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
