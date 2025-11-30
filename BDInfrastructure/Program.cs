using BDDomain.Models;
using BDDomain.Repositories;
using BDInfrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RealEstateDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RealEstateDB")));

builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<TenantRepository>();
builder.Services.AddScoped<LeaseRepository>();
builder.Services.AddSingleton<MongoServiceRequestRepository>();
builder.Services.AddTransient<SqlServiceRequestRepository>();
builder.Services.AddTransient<SqlTenantRepository>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
