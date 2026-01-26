using System;
using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Domain.Interfaces;
using InventoryMgmt.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryMgmt.Infrastructure.Repositories;

public class SupplierRepository : Repository<Supplier>, ISupplierRepository
{
    public SupplierRepository(InventoryDbContext context):base(context)
    {
        
    }
    //custom methods from ISupplierRepository
    public async Task<Supplier?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(s=>s.Email==email);
    }

    public async Task<Supplier?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(s=>s.Name==name);
    }

    public async Task<Supplier?> GetByPhoneAsync(string phone)
    {
        return await _dbSet.FirstOrDefaultAsync(s=>s.Phone==phone);
    }
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbSet.AnyAsync(s=>s.Name==name);
    }
}
