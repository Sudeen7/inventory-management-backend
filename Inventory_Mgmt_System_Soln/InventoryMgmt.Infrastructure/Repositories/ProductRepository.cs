using System;
using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Domain.Interfaces;
using InventoryMgmt.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryMgmt.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(InventoryDbContext context) : base(context)
    {

    }

    // Custom methods from IProductRepository
    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
    {
        return await _dbSet
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
    }

    public async Task<Product?> GetBySkuAsync(string sku)
    {
        return await _dbSet
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.SKU == sku);
    }

    public async Task<IEnumerable<Product>> GetBySupplierAsync(int supplierId)
    {
        return await _dbSet
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.SupplierId == supplierId)
                .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetLowStockProductsAsync()
    {
        // Products where total stock across all warehouses is below minimum
        return await _dbSet
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Include(p => p.WarehouseProducts)
                .Where(p => p.WarehouseProducts.Sum(wp => wp.Quantity) < p.MinimumStockLevel)
                .ToListAsync();
    }
    public async Task<bool> ExistsBySkuAsync(string sku)
    {
        return await _dbSet.AnyAsync(p => p.SKU == sku);
    }

    //Override methods to include navigation property
    public override async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .ToListAsync();
    }

    public override async Task<Product?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Include(p => p.WarehouseProducts)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
