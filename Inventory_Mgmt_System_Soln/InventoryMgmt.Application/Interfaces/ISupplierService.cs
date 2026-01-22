using System;
using InventoryMgmt.Application.DTOs.Supplier;

namespace InventoryMgmt.Application.Interfaces;

public interface ISupplierService
{
    //GET operations
    Task<IEnumerable<SupplierResponse>> GetAllAsync();
    Task<SupplierResponse?> GetByIdAsync(int id);
    Task<SupplierResponse?> GetByPhoneAsync(string phone);
    Task<SupplierResponse?> GetByNameAsync(string name);

    //command operations
    Task<SupplierResponse> CreateAsync(CreateSupplierRequest request);
    Task<SupplierResponse?> UpdateAsync(int id, UpdateSupplierRequest request);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
