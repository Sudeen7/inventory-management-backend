using System;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Domain.Common;

namespace InventoryMgmt.Infrastructure.Data;

public class InventoryDbContext:DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options):base(options)
    {
        
    }

    //DbSets - represent tables in the database
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<WarehouseProduct> WarehouseProducts { get; set; }
    public DbSet<StockMovement> StockMovements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //apply all configurations from the Data/Configurations folder
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken=default)
    {
        //automatically set timestamps when saving
        var entries=ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt=DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt=DateTime.UtcNow;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
