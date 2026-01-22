using InventoryMgmt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Domain.Interfaces
{
    public interface IWarehouseRepository:IRepository<Warehouse>
    {
        Task<Warehouse?> GetByNameAsync(string name);
        Task<IEnumerable<Warehouse>> GetByLocationAsync(string location);
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsAsync(int id);
    }
}
