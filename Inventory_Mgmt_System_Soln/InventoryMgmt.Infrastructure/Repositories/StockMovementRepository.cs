using System;
using InventoryMgmt.Domain.Common.Enums;
using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Domain.Interfaces;
using InventoryMgmt.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryMgmt.Infrastructure.Repositories;

public class StockMovementRepository : Repository<StockMovement>, IStockMovementRepository
{
    public StockMovementRepository(InventoryDbContext context) : base(context)
    {

    }
    public async Task<IEnumerable<StockMovement>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
                .Include(sm => sm.Product)
                .Include(sm => sm.Warehouse)
                .Where(sm => sm.MovementDate >= startDate && sm.MovementDate <= endDate)
                .OrderByDescending(sm => sm.MovementDate)
                .ToListAsync();
    }

    public async Task<IEnumerable<StockMovement>> GetByProductAsync(int productId)
    {
        return await _dbSet
                .Include(sm => sm.Product)
                .Include(sm => sm.Warehouse)
                .Where(sm => sm.ProductId == productId)
                .ToListAsync();
    }

    public async Task<IEnumerable<StockMovement>> GetByStockMovementTypeAsync(StockMovementType movementType)
    {
        return await _dbSet
                .Include(sm => sm.Product)
                .Include(sm => sm.Warehouse)
                .Where(sm => sm.MovementType == movementType)
                .ToListAsync();
    }

    public async Task<IEnumerable<StockMovement>> GetByWarehouseAsync(int warehouseId)
    {
        return await _dbSet
                .Include(sm => sm.Product)
                .Include(sm => sm.Warehouse)
                .Where(sm => sm.WarehouseId == warehouseId)
                .ToListAsync();
    }

    //override methods to get navigation property
    public override async Task<IEnumerable<StockMovement>> GetAllAsync()
    {
        return await _dbSet
                .Include(sm=>sm.Product)
                .Include(sm=>sm.Warehouse)
                .ToListAsync();
    }

    public override async Task<StockMovement?> GetByIdAsync(int id)
    {
        return await _dbSet
                .Include(sm=>sm.Product)
                .Include(sm=>sm.Warehouse)
                .FirstOrDefaultAsync(sm=>sm.Id==id);
    }
}
