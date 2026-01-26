using System;
using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Domain.Interfaces;
using InventoryMgmt.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryMgmt.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(InventoryDbContext context) : base(context)
    {

    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbSet.AnyAsync(c => c.Name == name);
    }

    //override to include Products navigation property
    public override async Task<Category?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(c => c.Products) //Load related products
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public override async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _dbSet
            .Include(c => c.Products) //Load related products
            .ToListAsync();
    }


}
