using Domain.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data.DbInitialization;

public class DbInitializer(
    LitMarketDbContext db,
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager)
    : IDbInitializer
{
    private readonly LitMarketDbContext _db = db;
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public void Initialize()
    {
        ApplyMigrations();
        AddUserRoles();
    }

    private void ApplyMigrations()
    {
        if (_db.Database.GetPendingMigrations().Any())
            _db.Database.Migrate();
    }

    private void AddUserRoles()
    {
        if (!_roleManager.Roles.Any())
        {
            _roleManager.CreateAsync(new IdentityRole(Roles.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Roles.Company)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Roles.Customer)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Roles.Employee)).GetAwaiter().GetResult();

            AddAdmin();
        }
    }

    private void AddAdmin()
    {
        _userManager.CreateAsync(new IdentityUser
        {
            UserName = "admin",
            Email = "admin@gmail.com"
        }, "Admin123*").GetAwaiter().GetResult();

        IdentityUser admin = _userManager.FindByNameAsync("admin").GetAwaiter().GetResult()!;

        _userManager.AddToRoleAsync(admin, Roles.Admin).GetAwaiter().GetResult();
    }
}
