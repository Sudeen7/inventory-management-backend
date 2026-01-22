using InventoryMgmt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Domain.Interfaces
{
    public interface IWarehouseProductRepository:IRepository<WarehouseProduct>
    {
        Task<IEnumerable<WarehouseProduct>> GetByWarehouseAsync(int warehouseId);
        Task<IEnumerable<WarehouseProduct>> GetByProductAsync(int productId);
        Task<WarehouseProduct?> GetByWarehouseAndProductAsync(int warehouseId, int productId);
        Task<int> GetTotalStockForProductAsync(int productId); //sum across all warehouses
    }
}
