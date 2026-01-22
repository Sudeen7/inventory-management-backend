using System;
using InventoryMgmt.Application.DTOs.Warehouse;
using InventoryMgmt.Application.Interfaces;
using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Domain.Interfaces;

namespace InventoryMgmt.Application.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IUnitOfWork _unitOfWork;
    public WarehouseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    //GET operations
    public async Task<IEnumerable<WarehouseResponse>> GetAllAsync()
    {
        var warehouses = await _unitOfWork.Warehouses.GetAllAsync();
        //map to response
        var response = warehouses.Select(w => new WarehouseResponse
        {
            Id = w.Id,
            Name = w.Name,
            Location = w.Location,
            Capacity = w.Capacity,
            TotalProductsStored = w.WarehouseProducts?.Count ?? 0
        });
        return response;
    }

    public async Task<WarehouseResponse?> GetByIdAsync(int id)
    {
        var warehouse = await _unitOfWork.Warehouses.GetByIdAsync(id);
        if (warehouse == null) return null;
        //map response
        var response = new WarehouseResponse
        {
            Id = warehouse.Id,
            Name = warehouse.Name,
            Location = warehouse.Location,
            Capacity = warehouse.Capacity,
            TotalProductsStored = warehouse.WarehouseProducts?.Count ?? 0
        };
        return response;
    }

    public async Task<IEnumerable<WarehouseResponse>> GetByLocationAsync(string location)
    {
        var warehouse = await _unitOfWork.Warehouses.GetByLocationAsync(location);
        //map response
        var response = warehouse.Select(w => new WarehouseResponse
        {
            Id = w.Id,
            Name = w.Name,
            Location = w.Location,
            Capacity = w.Capacity,
            TotalProductsStored = w.WarehouseProducts?.Count ?? 0
        });
        return response;
    }

    public async Task<WarehouseResponse?> GetByNameAsync(string name)
    {
        var warehouse = await _unitOfWork.Warehouses.GetByNameAsync(name);
        if (warehouse == null) return null;
        //map response
        var response = new WarehouseResponse
        {
            Id = warehouse.Id,
            Name = warehouse.Name,
            Location = warehouse.Location,
            Capacity = warehouse.Capacity,
            TotalProductsStored = warehouse.WarehouseProducts?.Count ?? 0
        };
        return response;
    }

    //command operations
    public async Task<WarehouseResponse> CreateAsync(CreateWarehouseRequest request)
    {
        var exists = await _unitOfWork.Warehouses.ExistsByNameAsync(request.Name);
        if (exists)
        {
            throw new Exception($"Warehouse with name '{request.Name}' already exists!");
        }
        var warehouse = new Warehouse
        {
            Name = request.Name,
            Location = request.Location,
            Capacity = request.Capacity,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Warehouses.AddAsync(warehouse);
        await _unitOfWork.SaveChangesAsync();

        //map response
        var response = new WarehouseResponse
        {
            Id = warehouse.Id,
            Name = warehouse.Name,
            Location = warehouse.Location,
            Capacity = warehouse.Capacity,
            TotalProductsStored = warehouse.WarehouseProducts?.Count ?? 0

        };
        return response;
    }
    public async Task<WarehouseResponse?> UpdateAsync(int id, UpdateWarehouseRequest request)
    {
        var warehouse = await _unitOfWork.Warehouses.GetByIdAsync(id);
        if (warehouse == null) return null;

        var existingWarehouse = await _unitOfWork.Warehouses.GetByNameAsync(request.Name);
        if (existingWarehouse != null && existingWarehouse.Id != id)
        {
            throw new Exception($"Warehouse with name '{request.Name}' already exists!");
        }
        //update properties
        warehouse.Name = request.Name;
        warehouse.Location = request.Location;
        warehouse.Capacity = request.Capacity;
        warehouse.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Warehouses.UpdateAsync(warehouse);
        await _unitOfWork.SaveChangesAsync();

        //re-fetch warehouse
        warehouse = await _unitOfWork.Warehouses.GetByIdAsync(id);
        if (warehouse == null) return null;

        //map response
        var response = new WarehouseResponse
        {
            Id = warehouse.Id,
            Name = warehouse.Name,
            Location = warehouse.Location,
            Capacity = warehouse.Capacity,
            TotalProductsStored = warehouse.WarehouseProducts?.Count ?? 0
        };
        return response;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _unitOfWork.Warehouses.GetByIdAsync(id);
        if (exists == null) return false;
        var deleted = await _unitOfWork.Warehouses.DeleteAsync(id);
        if (deleted)
        {
            await _unitOfWork.SaveChangesAsync();
        }
        return deleted;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _unitOfWork.Warehouses.ExistsAsync(id);
    }
}
