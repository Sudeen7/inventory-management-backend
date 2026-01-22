using InventoryMgmt.Domain.Common.Enums;
using InventoryMgmt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Domain.Interfaces
{
    public interface IStockMovementRepository:IRepository<StockMovement>
    {
        Task<IEnumerable<StockMovement>> GetByProductAsync(int productId);
        Task<IEnumerable<StockMovement>> GetByWarehouseAsync(int warehouseId);
        Task<IEnumerable<StockMovement>> GetByDateRangeAsync(DateTime startDate,DateTime endDate);
        Task<IEnumerable<StockMovement>> GetByStockMovementTypeAsync(StockMovementType movementType);
    }
}
