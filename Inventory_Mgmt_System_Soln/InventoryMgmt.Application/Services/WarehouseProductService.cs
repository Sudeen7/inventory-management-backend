using System;
using InventoryMgmt.Application.DTOs.WarehouseProduct;
using InventoryMgmt.Application.Interfaces;
using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Domain.Interfaces;

namespace InventoryMgmt.Application.Services;

public class WarehouseProductService : IWarehouseProductService
{
    private readonly IUnitOfWork _unitOfWork;
    public WarehouseProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    //GET operations
    public async Task<IEnumerable<WarehouseProductResponse>> GetAllAsync()
    {
        var warehouseProducts = await _unitOfWork.WarehouseProducts.GetAllAsync();

        var response = warehouseProducts.Select(wp => new WarehouseProductResponse
        {
            Id = wp.Id,
            Quantity = wp.Quantity,
            ProductId = wp.ProductId,
            ProductName = wp.Product?.Name ?? "Unknown",
            ProductSKU = wp.Product?.SKU ?? "Unknown",
            WarehouseId = wp.WarehouseId,
            WarehouseName = wp.Warehouse?.Name ?? "Unknown"
        });
        return response;
    }

    public async Task<WarehouseProductResponse?> GetByIdAsync(int id)
    {
        var warehouseProduct = await _unitOfWork.WarehouseProducts.GetByIdAsync(id);
        if (warehouseProduct == null) return null;

        var response = new WarehouseProductResponse
        {
            Id = warehouseProduct.Id,
            Quantity = warehouseProduct.Quantity,
            ProductId = warehouseProduct.ProductId,
            ProductName = warehouseProduct.Product?.Name ?? "Unknown",
            ProductSKU = warehouseProduct.Product?.SKU ?? "Unknown",
            WarehouseId = warehouseProduct.WarehouseId,
            WarehouseName = warehouseProduct.Warehouse?.Name ?? "Unknown"
        };
        return response;
    }

    public async Task<IEnumerable<WarehouseProductResponse>> GetByProductAsync(int productId)
    {
        var warehouseProducts = await _unitOfWork.WarehouseProducts.GetByProductAsync(productId);

        var response = warehouseProducts.Select(wp => new WarehouseProductResponse
        {
            Id = wp.Id,
            Quantity = wp.Quantity,
            ProductId = wp.ProductId,
            ProductName = wp.Product?.Name ?? "Unknown",
            ProductSKU = wp.Product?.SKU ?? "Unknown",
            WarehouseId = wp.WarehouseId,
            WarehouseName = wp.Warehouse?.Name ?? "Unknown"
        });
        return response;
    }
    public async Task<IEnumerable<WarehouseProductResponse>> GetByWarehouseAsync(int warehouseId)
    {
        var warehouseProducts = await _unitOfWork.WarehouseProducts.GetByWarehouseAsync(warehouseId);

        var response = warehouseProducts.Select(wp => new WarehouseProductResponse
        {
            Id = wp.Id,
            Quantity = wp.Quantity,
            ProductId = wp.ProductId,
            ProductName = wp.Product?.Name ?? "Unknown",
            ProductSKU = wp.Product?.SKU ?? "Unknown",
            WarehouseId = wp.WarehouseId,
            WarehouseName = wp.Warehouse?.Name ?? "Unknown"
        });
        return response;
    }

    public async Task<WarehouseProductResponse?> GetByWarehouseAndProductAsync(int warehouseId, int productId)
    {
        var warehouseProduct = await _unitOfWork.WarehouseProducts.GetByWarehouseAndProductAsync(warehouseId, productId);
        if (warehouseProduct == null) return null;

        var response = new WarehouseProductResponse
        {
            Id = warehouseProduct.Id,
            Quantity = warehouseProduct.Quantity,
            ProductId = warehouseProduct.ProductId,
            ProductName = warehouseProduct.Product?.Name ?? "Unknown", 
            ProductSKU = warehouseProduct.Product?.SKU ?? "Unknown",
            WarehouseId = warehouseProduct.WarehouseId,
            WarehouseName = warehouseProduct.Warehouse?.Name ?? "Unknown"
        };
        return response;
    }

    public async Task<int> GetTotalStockForProductAsync(int productId)
    {
        //get all WarehouseProducts for this product
        var warehouseProducts = await _unitOfWork.WarehouseProducts.GetByProductAsync(productId);

        //sum up quantities
        return warehouseProducts.Sum(wp => wp.Quantity);
    }

    //command operations
    public async Task<WarehouseProductResponse> UpdateStockAsync(UpdateStockRequest request)
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

        //validate quantity is not negative
        if (request.Quantity < 0)
        {
            throw new Exception("Quantiy cannot be negative!");
        }

        //check if warehouseProduct already exists
        var warehouseProduct = await _unitOfWork.WarehouseProducts.GetByWarehouseAndProductAsync(request.WarehouseId, request.ProductId);
        if (warehouseProduct == null)
        {
            //create new warehouseProduct
            warehouseProduct = new WarehouseProduct
            {
                ProductId = request.ProductId,
                WarehouseId = request.WarehouseId,
                Quantity = request.Quantity,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.WarehouseProducts.AddAsync(warehouseProduct);
        }
        else
        {
            warehouseProduct.Quantity = request.Quantity;
            warehouseProduct.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.WarehouseProducts.UpdateAsync(warehouseProduct);
        }
        await _unitOfWork.SaveChangesAsync();

        //reload warehouseProduct
        warehouseProduct = await _unitOfWork.WarehouseProducts.GetByWarehouseAndProductAsync(request.WarehouseId, request.ProductId);
        if (warehouseProduct == null)
        {
            throw new Exception("Unable to fetch record!");
        }
        //map response
        var response = new WarehouseProductResponse
        {
            Id = warehouseProduct.Id,
            Quantity = warehouseProduct.Quantity,
            ProductId = warehouseProduct.ProductId,
            ProductName = warehouseProduct.Product?.Name ?? "Unknown",
            ProductSKU = warehouseProduct.Product?.SKU ?? "Unknown",
            WarehouseId = warehouseProduct.WarehouseId,
            WarehouseName = warehouseProduct.Warehouse?.Name ?? "Unknown"
        };
        return response;
    }

    public async Task<bool> ExistsAsync(int warehouseId, int productId)
    {
        //return true if not null
        return await _unitOfWork.WarehouseProducts.GetByWarehouseAndProductAsync(warehouseId,productId) !=null;
        //similar to the following:
        /*var exists = await _unitOfWork.WarehouseProducts.GetByWarehouseAndProductAsync(warehouseId, productId);
        return exists!=null;*/
    }
}
