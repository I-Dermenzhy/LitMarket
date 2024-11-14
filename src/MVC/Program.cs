using DataAccess.Data;
using DataAccess.Data.DbInitialization;
using DataAccess.Extensions;

using Domain.Dto;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MVC.Services;

using Services.Extensions;

using Stripe;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

ConfigureServices();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
    ConfigureForNotDevelopment();

Configure();

SeedData();

app.Run();

void Configure()
{
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSession();

    app.MapRazorPages();

    app.MapControllerRoute(
        name: "default",
        pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

    StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
}

void ConfigureForNotDevelopment()
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

void ConfigureServices()
{
    IServiceCollection services = builder.Services;

    services.AddRazorPages();
    services.AddControllersWithViews();

    services.AddDbContext<LitMarketDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("Default"))
            .UseLazyLoadingProxies());

    services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<LitMarketDbContext>()
        .AddDefaultTokenProviders();

    services.AddAutoMapper(typeof(MappingProfile).Assembly);

    services.AddAuthentication()
        .AddFacebook(options =>
        {
            options.AppId = configuration["Authentication:Facebook:AppId"]!;
            options.AppSecret = configuration["Authentication:Facebook:AppSecret"]!;
        })
        .AddMicrosoftAccount(options =>
        {
            options.ClientId = configuration["Authentication:Microsoft:ClientId"]!;
            options.ClientSecret = configuration["Authentication:Microsoft:ClientSecret"]!;
        });

    services.AddDistributedMemoryCache();

    services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = $"/Identity/Account/Login";
        options.LogoutPath = $"/Identity/Account/Logout";
        options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    });

    services.AddStripeServices();

    services.AddScoped<IFileHelper, FileHelper>();
    services.AddScoped<IDbInitializer, DbInitializer>();

    services.AddLitMarketRepositories();
}

void SeedData()
{
    using var scope = app!.Services.CreateScope();
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.Initialize();
}