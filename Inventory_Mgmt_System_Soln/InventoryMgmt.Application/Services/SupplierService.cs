using System;
using System.Data.Common;
using System.Net.Cache;
using InventoryMgmt.Application.DTOs.Supplier;
using InventoryMgmt.Application.Interfaces;
using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Domain.Interfaces;
using Microsoft.VisualBasic;

namespace InventoryMgmt.Application.Services;

public class SupplierService : ISupplierService
{
    private readonly IUnitOfWork _unitOfWork;
    public SupplierService(IUnitOfWork unitOfWork)
    {
        _unitOfWork=unitOfWork;
    }
    //GET operations
    public async Task<IEnumerable<SupplierResponse>> GetAllAsync()
    {
        var suppliers=await _unitOfWork.Suppliers.GetAllAsync();
        //map response
        var response = suppliers.Select(s => new SupplierResponse
        {
            Id=s.Id,
            Name=s.Name,
            Address=s.Address,
            Email=s.Email,
            Phone=s.Phone
        });
        return response;
    }

    public async Task<SupplierResponse?> GetByIdAsync(int id)
    {
        var supplier=await _unitOfWork.Suppliers.GetByIdAsync(id);
        if (supplier==null) return null;
        //map response
        var response=new SupplierResponse
        {
            Id=supplier.Id,
            Name=supplier.Name,
            Address=supplier.Address,
            Email=supplier.Email,
            Phone=supplier.Phone
        };
        return response;
    }

    public async Task<SupplierResponse?> GetByNameAsync(string name)
    {
        var supplier=await _unitOfWork.Suppliers.GetByNameAsync(name);
        if(supplier==null) return null;
        var response=new SupplierResponse
        {
            Id=supplier.Id,
            Name=supplier.Name,
            Address=supplier.Address,
            Email=supplier.Email,
            Phone=supplier.Phone
        };
        return response;
    }

    public async Task<SupplierResponse?> GetByPhoneAsync(string phone)
    {
        var supplier=await _unitOfWork.Suppliers.GetByPhoneAsync(phone);
        if(supplier==null) return null;
        var response= new SupplierResponse
        {
            Id=supplier.Id,
            Name=supplier.Name,
            Address=supplier.Address,
            Email=supplier.Email,
            Phone=supplier.Phone
        };
        return response;
    }

    //command operations

    public async Task<SupplierResponse> CreateAsync(CreateSupplierRequest request)
    {
        var exists=await _unitOfWork.Suppliers.ExistsByNameAsync(request.Name);
        if (exists)
        {
            throw new Exception($"Supplier with name '{request.Name}' already exists!");
        }
        //map dto to entity
        var supplier=new Supplier
        {
            Name=request.Name,
            Address=request.Address,
            Email=request.Email,
            Phone=request.Phone,
            CreatedAt=DateTime.UtcNow
        };
        var addedSupplier=await _unitOfWork.Suppliers.AddAsync(supplier);
        await _unitOfWork.SaveChangesAsync();

        //map response
        var response= new SupplierResponse
        {
            Id=supplier.Id,
            Name=supplier.Name,
            Address=supplier.Address,
            Email=supplier.Email,
            Phone=supplier.Phone,
        };
        return response;
    }
    public async Task<SupplierResponse?> UpdateAsync(int id, UpdateSupplierRequest request)
    {
        var supplier=await _unitOfWork.Suppliers.GetByIdAsync(id);
        if (supplier==null) return null;

        var existingSupplier=await _unitOfWork.Suppliers.GetByNameAsync(request.Name);
        if(existingSupplier!=null && existingSupplier.Id != id)
        {
            throw new Exception($"Supplier with name '{request.Name}' already exists!");
        }
        //update properties
        supplier.Name=request.Name;
        supplier.Address=request.Address;
        supplier.Email=request.Email;
        supplier.Phone=request.Phone;
        supplier.UpdatedAt=DateTime.UtcNow;

        await _unitOfWork.Suppliers.UpdateAsync(supplier);
        await _unitOfWork.SaveChangesAsync();
        
        //refetch the supplier and handle possible null
        supplier=await _unitOfWork.Suppliers.GetByIdAsync(id);
        if(supplier==null) return null;

        //map response
        var response= new SupplierResponse
        {
            Id=supplier.Id,
            Name=supplier.Name,
            Address=supplier.Address,
            Email=supplier.Email,
            Phone=supplier.Phone,
        };
        return response;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var supplier=await _unitOfWork.Suppliers.GetByIdAsync(id);
        if(supplier==null) return false;
        var deleted=await _unitOfWork.Suppliers.DeleteAsync(id);
        if (deleted)
        {
            await _unitOfWork.SaveChangesAsync();  
        }
        return deleted;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _unitOfWork.Suppliers.ExistsAsync(id);
    }
}
