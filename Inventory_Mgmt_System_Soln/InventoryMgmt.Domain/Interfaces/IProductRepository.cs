using InventoryMgmt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Domain.Interfaces
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<Product?> GetBySkuAsync(string sku);
        Task<IEnumerable<Product>> GetByNameAsync(string name);
        Task<IEnumerable<Product>> GetByCategoryAsync(int  categoryId);
        Task<IEnumerable<Product>> GetBySupplierAsync(int  supplierId);
        Task<IEnumerable<Product>> GetLowStockProductsAsync();
        Task<bool> ExistsBySkuAsync(string sku);
        Task<bool> ExistsAsync(int id);
    }
}
