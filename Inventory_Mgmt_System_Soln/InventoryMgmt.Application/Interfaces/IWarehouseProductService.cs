using System;
using InventoryMgmt.Application.DTOs.WarehouseProduct;
using InventoryMgmt.Domain.Entities;

namespace InventoryMgmt.Application.Interfaces;

public interface IWarehouseProductService
{
    //GET operations
    Task<IEnumerable<WarehouseProductResponse>> GetAllAsync();
    Task<WarehouseProductResponse?> GetByIdAsync(int id);
    Task<IEnumerable<WarehouseProductResponse>> GetByWarehouseAsync(int warehouseId);
    Task<IEnumerable<WarehouseProductResponse>> GetByProductAsync(int productId);
    Task<WarehouseProductResponse?> GetByWarehouseAndProductAsync(int warehouseId, int productId);
    Task<int> GetTotalStockForProductAsync(int productId); //sum across all warehouses

    //command operations
    Task<WarehouseProductResponse> UpdateStockAsync(UpdateStockRequest request);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int warehouseId, int productId);

    //no create method as warehouseProduct is created auto when stockMovement happens or can be created via UpdateStockAsync
    /*
        UpdateStockAsync logic:
        - if warehouseProduct exists --> Update quantity,
        - if doesn't exist --> Create it with the quantity
    */
    //UpdateStockAsync is 'updating stock levels' not 'updating warehouse product record'
}
