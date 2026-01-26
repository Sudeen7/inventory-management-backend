using System;
using InventoryMgmt.Domain.Interfaces;
using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryMgmt.Infrastructure.Repositories;

public class WarehouseRepository : Repository<Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(InventoryDbContext context):base(context)
    {
        
    }

    public async Task<IEnumerable<Warehouse>> GetByLocationAsync(string location)
    {
        return await _dbSet
                .Where(w=>w.Location==location)
                .ToListAsync();
    }

    public async Task<Warehouse?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(w=>w.Name==name);
    }
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbSet.AnyAsync(w=>w.Name==name);
    }  
}
