using System;
using System.Diagnostics;
using InventoryMgmt.Application.DTOs.StockMovement;
using InventoryMgmt.Application.Interfaces;
using InventoryMgmt.Domain.Common.Enums;
using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Domain.Interfaces;

namespace InventoryMgmt.Application.Services;

public class StockMovementService : IStockMovementService
{
    private readonly IUnitOfWork _unitOfWork;
    public StockMovementService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    //GET operations
    public async Task<IEnumerable<StockMovementResponse>> GetAllAsync()
    {
        var stockMovements = await _unitOfWork.StockMovements.GetAllAsync();

        var response = stockMovements.Select(sm => new StockMovementResponse
        {
            Id = sm.Id,
            ProductId = sm.ProductId,
            ProductName = sm.Product?.Name ?? "Unknown",
            WarehouseId = sm.WarehouseId,
            WarehouseName = sm.Warehouse?.Name ?? "Unknown",
            MovementType = sm.MovementType,
            Quantity = sm.Quantity,
            MovementDate = sm.MovementDate,
            Reference = sm.Reference,
            Notes = sm.Notes
        });
        return response;
    }

    public async Task<StockMovementResponse?> GetByIdAsync(int id)
    {
        var stockMovement = await _unitOfWork.StockMovements.GetByIdAsync(id);

        if (stockMovement == null) return null;

        var response = new StockMovementResponse
        {
            Id = stockMovement.Id,
            ProductId = stockMovement.ProductId,
            ProductName = stockMovement.Product?.Name ?? "Unknown",
            WarehouseId = stockMovement.WarehouseId,
            WarehouseName = stockMovement.Warehouse?.Name ?? "Unknown",
            MovementType = stockMovement.MovementType,
            Quantity = stockMovement.Quantity,
            MovementDate = stockMovement.MovementDate,
            Reference = stockMovement.Reference,
            Notes = stockMovement.Notes
        };
        return response;
    }

    public async Task<IEnumerable<StockMovementResponse>> GetByWarehouseAsync(int warehouseId)
    {
        var stockMovements = await _unitOfWork.StockMovements.GetByWarehouseAsync(warehouseId);

        var response = stockMovements.Select(sm => new StockMovementResponse
        {
            Id = sm.Id,
            ProductId = sm.ProductId,
            ProductName = sm.Product?.Name ?? "Unknown",
            WarehouseId = sm.WarehouseId,
            WarehouseName = sm.Warehouse?.Name ?? "Unknown",
            MovementType = sm.MovementType,
            Quantity = sm.Quantity,
            MovementDate = sm.MovementDate,
            Reference = sm.Reference,
            Notes = sm.Notes
        });
        return response;
    }
    public async Task<IEnumerable<StockMovementResponse>> GetByProductAsync(int productId)
    {
        var stockMovements = await _unitOfWork.StockMovements.GetByProductAsync(productId);

        var response = stockMovements.Select(sm => new StockMovementResponse
        {
            Id = sm.Id,
            ProductId = sm.ProductId,
            ProductName = sm.Product?.Name ?? "Unknown",
            WarehouseId = sm.WarehouseId,
            WarehouseName = sm.Warehouse?.Name ?? "Unknown",
            MovementType = sm.MovementType,
            Quantity = sm.Quantity,
            MovementDate = sm.MovementDate,
            Reference = sm.Reference,
            Notes = sm.Notes
        });
        return response;
    }
    public async Task<IEnumerable<StockMovementResponse>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var stockMovements = await _unitOfWork.StockMovements.GetByDateRangeAsync(startDate, endDate);

        var response = stockMovements.Select(sm => new StockMovementResponse
        {
            Id = sm.Id,
            ProductId = sm.ProductId,
            ProductName = sm.Product?.Name ?? "Unknown",
            WarehouseId = sm.WarehouseId,
            WarehouseName = sm.Warehouse?.Name ?? "Unknown",
            MovementType = sm.MovementType,
            Quantity = sm.Quantity,
            MovementDate = sm.MovementDate,
            Reference = sm.Reference,
            Notes = sm.Notes
        });
        return response;
    }

    public async Task<IEnumerable<StockMovementResponse>> GetByStockMovementTypeAsync(StockMovementType movementType)
    {
        var stockMovements = await _unitOfWork.StockMovements.GetByStockMovementTypeAsync(movementType);

        var response = stockMovements.Select(sm => new StockMovementResponse
        {
            Id = sm.Id,
            ProductId = sm.ProductId,
            ProductName = sm.Product?.Name ?? "Unknown",
            WarehouseId = sm.WarehouseId,
            WarehouseName = sm.Warehouse?.Name ?? "Unknown",
            MovementType = sm.MovementType,
            Quantity = sm.Quantity,
            MovementDate = sm.MovementDate,
            Reference = sm.Reference,
            Notes = sm.Notes
        });
        return response;
    }

    //command operations
    public async Task<StockMovementResponse> CreateAsync(CreateStockMovementRequest request)
    {
        //validate product exists
        if (!await _unitOfWork.Products.ExistsAsync(request.ProductId))
        {
            throw new Exception($"Product with ID '{request.ProductId}' not found!");
        }

        //validate warehouse exists
        if (!await _unitOfWork.Warehouses.ExistsAsync(request.WarehouseId))
        {
            throw new Exception($"Warehouse with ID '{request.WarehouseId}' not found!");
        }

        //validate quantity > 0
        if (request.Quantity <= 0)
        {
            throw new Exception("Quantity must be greater than 0.");
        }

        //handle based on MovementType
        switch (request.MovementType)
        {
            case StockMovementType.In:
                var warehouseProduct = await _unitOfWork.WarehouseProducts.GetByWarehouseAndProductAsync(request.WarehouseId, request.ProductId);
                if (warehouseProduct == null)
                {
                    //product not in this warehouse yet -create new record
                    warehouseProduct = new WarehouseProduct
                    {
                        ProductId = request.ProductId,
                        WarehouseId = request.WarehouseId,
                        Quantity = request.Quantity, //start with initial incoming quantity
                        CreatedAt = DateTime.UtcNow
                    };
                    await _unitOfWork.WarehouseProducts.AddAsync(warehouseProduct);
                }
                else
                {
                    //product already exists - increase quantity
                    warehouseProduct.Quantity += request.Quantity; //ADD to existing quantity
                    warehouseProduct.UpdatedAt = DateTime.UtcNow;
                    await _unitOfWork.WarehouseProducts.UpdateAsync(warehouseProduct);
                }
                break;

            case StockMovementType.Out:
                //GET existing warehouseProduct (must exist in this case)
                warehouseProduct = await _unitOfWork.WarehouseProducts.GetByWarehouseAndProductAsync(request.WarehouseId, request.ProductId);

                if (warehouseProduct == null)
                {
                    throw new Exception($"Product-{request.ProductId} not found in Warehouse-{request.WarehouseId}!");
                }

                //validate sufficient stock
                if (warehouseProduct.Quantity < request.Quantity)
                {
                    throw new Exception($"Insufficient stock! Available: {warehouseProduct.Quantity} || Requested: {request.Quantity}");
                }

                //decrease quantity
                warehouseProduct.Quantity -= request.Quantity; //SUBTRACT from existing quantity
                warehouseProduct.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.WarehouseProducts.UpdateAsync(warehouseProduct);

                break;

            case StockMovementType.Transfer:

                throw new Exception("For transfers, use CreateTransferAsync method instead!");

            default:
                throw new Exception($"Unkonwn movement type: {request.MovementType}");
        }

        //NOW, create StockMovement record
        var stockMovement = new StockMovement
        {
            ProductId = request.ProductId,
            WarehouseId = request.WarehouseId,
            MovementType = request.MovementType,
            Quantity = request.Quantity,
            MovementDate = DateTime.UtcNow,
            Reference = request.Reference,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        var addedMovement = await _unitOfWork.StockMovements.AddAsync(stockMovement);

        //save both, StockMovement and WarehouseProduct changes to DB
        await _unitOfWork.SaveChangesAsync();

        //re-fetch the StockMovement record
        stockMovement = await _unitOfWork.StockMovements.GetByIdAsync(addedMovement.Id);

        if (stockMovement == null) throw new Exception("Unable to fetch created stock movement!");

        //map response
        var response = new StockMovementResponse
        {
            Id = stockMovement.Id,
            ProductId = stockMovement.ProductId,
            ProductName = stockMovement.Product?.Name ?? "Unknown",
            WarehouseId = stockMovement.WarehouseId,
            WarehouseName = stockMovement.Warehouse?.Name ?? "Unknown",
            MovementType = stockMovement.MovementType,
            Quantity = stockMovement.Quantity,
            MovementDate = stockMovement.MovementDate,
            Reference = stockMovement.Reference,
            Notes = stockMovement.Notes
        };

        return response;
    }

    public async Task<IEnumerable<StockMovementResponse>> CreateTransferAsync(
        int productId,
        int sourceWarehouseId, int destinationWarehouseId,
        int quantity,
        string? reference,
        string? notes)
    {
        //validate product exists
        if (!await _unitOfWork.Products.ExistsAsync(productId))
        {
            throw new Exception($"Product with ID {productId} not found!");
        }

        //validate sourceWarehouse exists
        if (!await _unitOfWork.Warehouses.ExistsAsync(sourceWarehouseId))
        {
            throw new Exception($"Source warehouse with ID {sourceWarehouseId} not found!");
        }

        //validate destinationWarehouse exists
        if (!await _unitOfWork.Warehouses.ExistsAsync(destinationWarehouseId))
        {
            throw new Exception($"Destination warehouse with ID {destinationWarehouseId} not found!");
        }

        //validate source and destination warehouses are not same
        if (sourceWarehouseId == destinationWarehouseId)
        {
            throw new Exception("Source and destination warehouses cannot be the same!");
        }

        //validate quantity > 0
        if (quantity <= 0)
        {
            throw new Exception("Transfer quantity must be greater than 0.");
        }

        //check sufficient stock at source warehouse
        var sourceWarehouseProduct = await _unitOfWork.WarehouseProducts.GetByWarehouseAndProductAsync(sourceWarehouseId, productId);

        if (sourceWarehouseProduct == null)
        {
            throw new Exception($"Product-{productId} not found in source warehouse-{sourceWarehouseId}");
        }

        if (sourceWarehouseProduct.Quantity < quantity)
        {
            throw new Exception($"Insufficient stock in source warehouse! Availabe: {sourceWarehouseProduct.Quantity} || Requested: {quantity}");
        }

        //GENERATE a Transfer Reference
        //If no reference provided, generate a unique one with GUID (Global Unique Identifier) taking first 8 chars

        string transferReference = reference ?? $"TRANSFER-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

        //update source warehouse (OUT)
        sourceWarehouseProduct.Quantity -= quantity;
        sourceWarehouseProduct.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.WarehouseProducts.UpdateAsync(sourceWarehouseProduct);

        //update destination warehouse (IN)
        var destinationWarehouseProduct = await _unitOfWork.WarehouseProducts.GetByWarehouseAndProductAsync(destinationWarehouseId, productId);
        if (destinationWarehouseProduct == null)
        {
            //product doesn't exist at destination yet - create new
            destinationWarehouseProduct = new WarehouseProduct
            {
                ProductId = productId,
                WarehouseId = destinationWarehouseId,
                Quantity = quantity,
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.WarehouseProducts.AddAsync(destinationWarehouseProduct);
        }
        else
        {
            //product already exists at destination - update quantity
            destinationWarehouseProduct.Quantity += quantity;
            destinationWarehouseProduct.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.WarehouseProducts.UpdateAsync(destinationWarehouseProduct);
        }

        //CREATE: OUT Stock Movement Record
        var outMovement = new StockMovement
        {
            ProductId = productId,
            WarehouseId = sourceWarehouseId,
            MovementType = StockMovementType.Out,
            Quantity = quantity,
            MovementDate = DateTime.UtcNow,
            Reference = transferReference,
            Notes = notes ?? $"Transfer to warehouse-{destinationWarehouseId}.",
            CreatedAt = DateTime.UtcNow
        };

        var addedOutMovement = await _unitOfWork.StockMovements.AddAsync(outMovement);

        //CREATE: IN Stock Movement Record
        var inMovement = new StockMovement
        {
            ProductId = productId,
            WarehouseId = destinationWarehouseId,
            MovementType = StockMovementType.In,
            Quantity = quantity,
            MovementDate = DateTime.UtcNow,
            Reference = transferReference,
            Notes = notes ?? $"Transfer from warehouse-{sourceWarehouseId}.",
            CreatedAt = DateTime.UtcNow
        };

        var addedInMovement = await _unitOfWork.StockMovements.AddAsync(inMovement);

        /*SAVE all changes (incl. source warehouseProduct, destination warehouseProduct, 
                                    OUT movement record, IN movement record)*/
        await _unitOfWork.SaveChangesAsync();

        //reload both movements with navigation properties
        var outMovementReload = await _unitOfWork.StockMovements.GetByIdAsync(addedOutMovement.Id);
        var inMovementReload = await _unitOfWork.StockMovements.GetByIdAsync(addedInMovement.Id);

        if (outMovementReload == null || inMovementReload == null)
        {
            throw new Exception("Unable to fetch created stock movement record(s)!");
        }

        //map to response
        var responses = new List<StockMovementResponse>
        {
            //OUT movement from source
            new StockMovementResponse
            {
                Id=outMovementReload.Id,
                ProductId=outMovementReload.ProductId,
                ProductName=outMovementReload.Product?.Name ?? "Unknown",
                WarehouseId=outMovementReload.WarehouseId,
                WarehouseName=outMovementReload.Warehouse?.Name ?? "Unknown",
                MovementType=outMovementReload.MovementType,
                Quantity=outMovementReload.Quantity,
                MovementDate=outMovementReload.MovementDate,
                Reference=outMovementReload.Reference,
                Notes=outMovementReload.Notes
            },

            //IN movement to destination
            new StockMovementResponse
            {
                Id=inMovementReload.Id,
                ProductId=inMovementReload.ProductId,
                ProductName=inMovementReload.Product?.Name ?? "Unknown",
                WarehouseId=inMovementReload.WarehouseId,
                WarehouseName=inMovementReload.Warehouse?.Name ?? "Unknown",
                MovementType=inMovementReload.MovementType,
                Quantity=inMovementReload.Quantity,
                MovementDate=inMovementReload.MovementDate,
                Reference=inMovementReload.Reference,
                Notes=inMovementReload.Notes
            }
        };
        return responses;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _unitOfWork.StockMovements.GetByIdAsync(id) != null;
    }
}
