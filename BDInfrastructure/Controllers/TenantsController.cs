using Microsoft.AspNetCore.Mvc;
using BDDomain.Models;
using BDDomain.Repositories;

public class TenantController : Controller
{
    private readonly UnitOfWork _uow;

    public TenantController()
    {
        var context = new RealEstateDbContext();
        _uow = new UnitOfWork(context);
    }

    public IActionResult Index()
    {
        var tenants = _uow.Tenants.GetActiveTenants();
        return View(tenants);
    }
}
