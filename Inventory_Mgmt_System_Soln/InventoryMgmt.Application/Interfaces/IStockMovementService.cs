using System;
using System.Security.Cryptography.X509Certificates;
using InventoryMgmt.Application.DTOs.StockMovement;
using InventoryMgmt.Domain.Common.Enums;

namespace InventoryMgmt.Application.Interfaces;

public interface IStockMovementService
{
    //GET operations
    Task<IEnumerable<StockMovementResponse>> GetAllAsync();
    Task<StockMovementResponse?> GetByIdAsync(int id);
    Task<IEnumerable<StockMovementResponse>> GetByWarehouseAsync(int warehouseId);
    Task<IEnumerable<StockMovementResponse>> GetByProductAsync(int productId);
    Task<IEnumerable<StockMovementResponse>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<StockMovementResponse>> GetByStockMovementTypeAsync(StockMovementType movementType);

    //command operations
    Task<StockMovementResponse> CreateAsync(CreateStockMovementRequest request);

    //method for Stock Transfers (creates 2 movements)
    Task<IEnumerable<StockMovementResponse>> CreateTransferAsync(int productId, 
                                                            int sourceWarehouseId, 
                                                            int destinationWarehouseId, 
                                                            int quantity, 
                                                            string? reference, 
                                                            string? notes);
    Task<bool> ExistsAsync(int id);

    //no update and delete as audit record should never be modified or deleted
}
