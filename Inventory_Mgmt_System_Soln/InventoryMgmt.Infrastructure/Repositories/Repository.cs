using System;
using InventoryMgmt.Domain.Common;
using InventoryMgmt.Domain.Interfaces;
using InventoryMgmt.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryMgmt.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly InventoryDbContext _context;
    protected readonly DbSet<T> _dbSet;
    public Repository(InventoryDbContext dbContext)
    {
        _context = dbContext;
        _dbSet = dbContext.Set<T>(); //get dbSet for this entity type
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return false;
        _dbSet.Remove(entity);
        return true;
    }

    public virtual async Task<bool> ExistsAsync(int id)
    {
        return await _dbSet.AnyAsync(e => e.Id == id);
    }
}
