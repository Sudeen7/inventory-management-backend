using InventoryMgmt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Domain.Interfaces
{
    public interface ICategoryRepository:IRepository<Category>
    {
        Task<Category?> GetByNameAsync(string name);
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsAsync(int id);
    }
}
