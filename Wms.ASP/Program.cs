using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Wms.Application.UseCases.Inventory;
using Wms.Application.UseCases.Items;
using Wms.Application.UseCases.Locations;
using Wms.Application.UseCases.Picking;
using Wms.Application.UseCases.Receiving;
using Wms.Application.UseCases.Reports;
using Wms.Domain.Entities;
using Wms.Domain.Repositories;
using Wms.Domain.Services;
using Wms.Infrastructure.Data;
using Wms.Infrastructure.Repositories;
using Wms.Infrastructure.Services;

namespace Wms.ASP;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // Add Entity Framework
        builder.Services.AddDbContext<WmsDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
                              ?? "Data Source=warehouse.db"));

        // Configure JWT Authentication
        var jwtSettings = builder.Configuration.GetSection("Jwt");
        var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured");
        var issuer = jwtSettings["Issuer"] ?? "WMS";
        var audience = jwtSettings["Audience"] ?? "WMS";

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.LoginPath = "/Auth/Login";
            options.LogoutPath = "/Auth/Logout";
            options.AccessDeniedPath = "/Auth/Login";
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
            options.SlidingExpiration = true;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = async context =>
                {
                    var tokenService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();
                    var token = context.SecurityToken as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;
                    
                    if (token != null)
                    {
                        var tokenString = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
                        var appToken = await tokenService.ValidateTokenAsync(tokenString);
                        
                        if (appToken == null)
                        {
                            context.Fail("Token is invalid or revoked");
                        }
                    }
                }
            };
        });

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        // Register infrastructure services
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IStockMovementService, StockMovementService>();
        builder.Services.AddScoped<IAuthService, AuthService>();

        // Register application services
        builder.Services.AddScoped<IGetStockUseCase, GetStockUseCase>();
        builder.Services.AddScoped<IStockAdjustmentUseCase, StockAdjustmentUseCase>();
        builder.Services.AddScoped<IGetItemsUseCase, GetItemsUseCase>();
        builder.Services.AddScoped<ICreateItemUseCase, CreateItemUseCase>();
        builder.Services.AddScoped<IUpdateItemUseCase, UpdateItemUseCase>();
        builder.Services.AddScoped<IGetLocationsUseCase, GetLocationsUseCase>();
        builder.Services.AddScoped<ICreateLocationUseCase, CreateLocationUseCase>();
        builder.Services.AddScoped<IUpdateLocationUseCase, UpdateLocationUseCase>();
        builder.Services.AddScoped<IReceiveItemUseCase, ReceiveItemUseCase>();
        builder.Services.AddScoped<IPutawayUseCase, PutawayUseCase>();
        builder.Services.AddScoped<IPickOrderUseCase, PickOrderUseCase>();
        builder.Services.AddScoped<IMovementReportUseCase, MovementReportUseCase>();

        var app = builder.Build();

        // Initialize Database
        await InitializeDatabaseAsync(app);

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseSession();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            "default",
            "{controller=Dashboard}/{action=Index}/{id?}");

        app.Run();
    }

    private static async Task InitializeDatabaseAsync(WebApplication app)
    {
        try
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WmsDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Seed initial data if needed
            await SeedInitialDataAsync(context, logger, authService);

            logger.LogInformation("Database initialized successfully");
        }
        catch (Exception ex)
        {
            using var scope = app.Services.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while initializing the database");
            throw;
        }
    }

    private static async Task SeedInitialDataAsync(WmsDbContext context, ILogger logger, IAuthService authService)
    {
        // Check if we already have data
        var hasData = await context.Items.AnyAsync() || await context.Locations.AnyAsync();
        
        // Create default warehouse if needed
        if (!hasData)
        {
            var warehouse = new Warehouse("Main Warehouse", "MAIN");
            context.Warehouses.Add(warehouse);
            await context.SaveChangesAsync();

            // Create some sample locations
            var receivingLocation = new Location("RECEIVING", "Receiving Area", warehouse.Id);
            receivingLocation.SetReceivable(true);
            receivingLocation.SetPickable(false);

            var storageLocation = new Location("A001", "Storage Area A1", warehouse.Id);
            storageLocation.SetReceivable(true);
            storageLocation.SetPickable(true);

            var shippingLocation = new Location("SHIPPING", "Shipping Area", warehouse.Id);
            shippingLocation.SetReceivable(false);
            shippingLocation.SetPickable(true);

            context.Locations.AddRange(receivingLocation, storageLocation, shippingLocation);

            // Create some sample items
            var item1 = new Item("WIDGET-001", "Standard Widget", "EA");
            var item2 = new Item("GADGET-001", "Premium Gadget", "EA", true);
            var item3 = new Item("TOOL-001", "Professional Tool", "EA", requiresSerial: true);

            context.Items.AddRange(item1, item2, item3);

            await context.SaveChangesAsync();

            logger.LogInformation("Initial seed data created successfully");
        }

        // Create default user if it doesn't exist
        if (!await context.Users.AnyAsync())
        {
            // Default password: admin123 (change in production!)
            var passwordHash = await authService.HashPasswordAsync("admin123");
            var defaultUser = new User("admin", passwordHash, "WMS", isActive: true);
            context.Users.Add(defaultUser);
            await context.SaveChangesAsync();

            logger.LogInformation("Default user created: admin/admin123");
        }
    }
}