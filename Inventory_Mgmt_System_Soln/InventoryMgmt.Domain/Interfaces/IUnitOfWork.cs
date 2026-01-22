using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Domain.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IStockMovementRepository StockMovements { get; }
        ISupplierRepository Suppliers { get; }
        IWarehouseProductRepository WarehouseProducts{ get; }
        IWarehouseRepository Warehouses{ get; }

        Task<int> SaveChangesAsync(); //cancellation token can be used
    }
}
