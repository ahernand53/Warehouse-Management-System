// Wms.Infrastructure/Data/WmsDbContext.cs

using Microsoft.EntityFrameworkCore;
using Wms.Domain.Entities;
using Wms.Infrastructure.Data.Configurations;

namespace Wms.Infrastructure.Data;

public class WmsDbContext : DbContext
{
    public WmsDbContext(DbContextOptions<WmsDbContext> options) : base(options)
    {
    }

    public DbSet<Item> Items => Set<Item>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Lot> Lots => Set<Lot>();
    public DbSet<Stock> Stock => Set<Stock>();
    public DbSet<Movement> Movements => Set<Movement>();
    public DbSet<User> Users => Set<User>();
    public DbSet<AppToken> AppTokens => Set<AppToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ItemConfiguration());
        modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
        modelBuilder.ApplyConfiguration(new LocationConfiguration());
        modelBuilder.ApplyConfiguration(new LotConfiguration());
        modelBuilder.ApplyConfiguration(new StockConfiguration());
        modelBuilder.ApplyConfiguration(new MovementConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new AppTokenConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}