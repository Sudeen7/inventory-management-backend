using InventoryMgmt.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.Interfaces
{
    public interface ICategoryService
    {
        //GET operations
        Task<IEnumerable<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse?> GetByIdAsync(int id);
        Task<CategoryResponse?> GetByNameAsync(string name);

        //Command operations
        Task<CategoryResponse> CreateAsync(CreateCategoryRequest request);
        Task<CategoryResponse?> UpdateAsync(int id, UpdateCategoryRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
