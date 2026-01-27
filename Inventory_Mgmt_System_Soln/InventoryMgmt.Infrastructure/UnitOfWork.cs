using System;
using InventoryMgmt.Domain.Interfaces;
using InventoryMgmt.Infrastructure.Data;
using InventoryMgmt.Infrastructure.Repositories;
namespace InventoryMgmt.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly InventoryDbContext _context;

    //Repository Instances
    public IProductRepository Products { get; }

    public ICategoryRepository Categories { get; }

    public IWarehouseRepository Warehouses { get; }

    public ISupplierRepository Suppliers { get; }

    public IWarehouseProductRepository WarehouseProducts { get; }
    public IStockMovementRepository StockMovements { get; }

    public UnitOfWork(InventoryDbContext context)
    {
        _context = context;

        //Initialize all repositories with same context
        Products = new ProductRepository(_context);
        Categories = new CategoryRepository(_context);
        Warehouses = new WarehouseRepository(_context);
        Suppliers = new SupplierRepository(_context);
        WarehouseProducts = new WarehouseProductRepository(_context);
        StockMovements = new StockMovementRepository(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        //clean-up database connection
        _context.Dispose();
    }
}
