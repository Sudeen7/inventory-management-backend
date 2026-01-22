using InventoryMgmt.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.Interfaces
{
    public interface IProductService
    {
        //GET operations
        Task<IEnumerable<ProductResponse>> GetAllAsync();
        Task<ProductResponse?> GetByIdAsync(int id);
        Task<ProductResponse?> GetBySkuAsync(string sku);
        Task<IEnumerable<ProductResponse>> GetByCategoryAsync(int categoryId); //filter by category
        Task<IEnumerable<ProductResponse>> GetLowStockAsync(); //products below minimum stock

        //Command operations
        Task<ProductResponse> CreateAsync(CreateProductRequest request);
        Task<ProductResponse?> UpdateAsync(int id, UpdateProductRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);

        Task<bool> ExistsAsync(string sku);
    }
}
