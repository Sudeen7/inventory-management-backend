using System;
using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Domain.Interfaces;
using InventoryMgmt.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryMgmt.Infrastructure.Repositories;

public class WarehouseProductRepository : Repository<WarehouseProduct>, IWarehouseProductRepository
{
    public WarehouseProductRepository(InventoryDbContext context) : base(context)
    {

    }
    public async Task<IEnumerable<WarehouseProduct>> GetByProductAsync(int productId)
    {
        return await _dbSet
                .Include(wp => wp.Warehouse)
                .Include(wp => wp.Product)
                .Where(wp => wp.ProductId == productId)
                .ToListAsync();
    }

    public async Task<WarehouseProduct?> GetByWarehouseAndProductAsync(int warehouseId, int productId)
    {
        return await _dbSet
                .Include(wp => wp.Warehouse)
                .Include(wp => wp.Product)
                .FirstOrDefaultAsync(wp => wp.WarehouseId == warehouseId && wp.ProductId == productId);
    }

    public async Task<IEnumerable<WarehouseProduct>> GetByWarehouseAsync(int warehouseId)
    {
        return await _dbSet
                .Include(wp=>wp.Warehouse)
                .Include(wp=>wp.Product)
                .Where(wp=>wp.WarehouseId==warehouseId)
                .ToListAsync();
    }

    public async Task<int> GetTotalStockForProductAsync(int productId)
    {
        return await _dbSet
                .Where(wp=>wp.ProductId==productId)
                .SumAsync(wp=>wp.Quantity);
    }

    //override methods to get navigation property
    public override async Task<IEnumerable<WarehouseProduct>> GetAllAsync()
    {
        return await _dbSet
                .Include(wp=>wp.Warehouse)
                .Include(wp=>wp.Product)
                .ToListAsync();
    }

    public override async Task<WarehouseProduct?> GetByIdAsync(int id)
    {
        return await _dbSet
                .Include(wp=>wp.Warehouse)
                .Include(wp=>wp.Product)
                .FirstOrDefaultAsync(wp=>wp.Id==id);
    }
}
