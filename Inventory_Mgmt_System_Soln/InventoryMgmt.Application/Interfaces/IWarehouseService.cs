using System;
using InventoryMgmt.Application.DTOs.Warehouse;

namespace InventoryMgmt.Application.Interfaces;

public interface IWarehouseService
{
    //GET operations
    Task<IEnumerable<WarehouseResponse>> GetAllAsync();
    Task<WarehouseResponse?> GetByIdAsync(int id);
    Task<WarehouseResponse?> GetByNameAsync(string name);
    Task<IEnumerable<WarehouseResponse>> GetByLocationAsync(string location);

    //command operations
    Task<WarehouseResponse> CreateAsync(CreateWarehouseRequest request);
    Task<WarehouseResponse?> UpdateAsync(int id, UpdateWarehouseRequest request);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
