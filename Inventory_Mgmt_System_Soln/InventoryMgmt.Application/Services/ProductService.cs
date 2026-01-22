using InventoryMgmt.Application.DTOs.Product;
using InventoryMgmt.Application.Interfaces;
using InventoryMgmt.Domain.Interfaces;
using InventoryMgmt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //GET operations
        public async Task<IEnumerable<ProductResponse>> GetAllAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            //map entities to dtos
            var response = products.Select(p => new ProductResponse
            {
                Id = p.Id,
                SKU = p.SKU,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                MinimumStockLevel = p.MinimumStockLevel,

                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? "Unknown",
                SupplierId = p.SupplierId,
                SupplierName = p.Supplier?.Name ?? "Unknown"
            });
            return response;
        }
        public async Task<ProductResponse?> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null) return null;
            var response = new ProductResponse
            {
                Id = product.Id,
                SKU = product.SKU,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                MinimumStockLevel = product.MinimumStockLevel,

                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? "Unknown",
                SupplierId = product.SupplierId,
                SupplierName = product.Supplier?.Name ?? "Unknown"
            };
            return response;
        }
        public async Task<IEnumerable<ProductResponse>> GetByCategoryAsync(int categoryId)
        {
            var products = await _unitOfWork.Products.GetByCategoryAsync(categoryId);

            var response = products.Select(p => new ProductResponse
            {
                Id = p.Id,
                SKU = p.SKU,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                MinimumStockLevel = p.MinimumStockLevel,

                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? "Unknown",
                SupplierId = p.SupplierId,
                SupplierName = p.Supplier?.Name ?? "Unknown"
            });
            return response;

        }
        public async Task<ProductResponse?> GetBySkuAsync(string sku)
        {
            var product = await _unitOfWork.Products.GetBySkuAsync(sku);
            if (product == null) return null;
            var response = new ProductResponse
            {
                Id = product.Id,
                SKU = product.SKU,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                MinimumStockLevel = product.MinimumStockLevel,

                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? "Unknown",
                SupplierId = product.SupplierId,
                SupplierName = product.Supplier?.Name ?? "Unknown"
            };
            return response;
        }
        public async Task<IEnumerable<ProductResponse>> GetLowStockAsync()
        {
            var products = await _unitOfWork.Products.GetLowStockProductsAsync();

            var response = products.Select(p => new ProductResponse
            {
                Id = p.Id,
                SKU = p.SKU,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                MinimumStockLevel = p.MinimumStockLevel,

                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? "Unknown",
                SupplierId = p.SupplierId,
                SupplierName = p.Supplier?.Name ?? "Unknown"
            });
            return response;
        }

        //Command operations
        public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
        {
            var exists = await _unitOfWork.Products.ExistsBySkuAsync(request.SKU);
            if (exists)
            {
                throw new Exception($"Product with SKU '{request.SKU}' already exists!");
            }
            //validate category exists
            if (!await _unitOfWork.Categories.ExistsAsync(request.CategoryId))
            {
                throw new Exception($"Category with ID '{request.CategoryId}' does not exist!");
            }
            //validate supplier exists
            if (!await _unitOfWork.Suppliers.ExistsAsync(request.SupplierId))
            {
                throw new Exception($"Supplier with ID '{request.SupplierId}' does not exist!");
            }

            var product = new Product
            {
                SKU = request.SKU,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                MinimumStockLevel = request.MinimumStockLevel,
                CategoryId = request.CategoryId,
                SupplierId = request.SupplierId,
                CreatedAt = DateTime.UtcNow,
            };

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            var response = new ProductResponse
            {
                Id = product.Id,
                SKU = product.SKU,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                MinimumStockLevel = product.MinimumStockLevel,

                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? "Unknown",
                SupplierId = product.SupplierId,
                SupplierName = product.Supplier?.Name ?? "Unknown"
            };
            return response;
        }
        public async Task<ProductResponse?> UpdateAsync(int id, UpdateProductRequest request)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null) return null;

            //validation: check if the request sku already exists (for another product)
            var existingProduct = await _unitOfWork.Products.GetBySkuAsync(request.SKU);
            if (existingProduct != null && existingProduct.Id != id)
            {
                throw new Exception($"Product with SKU '{request.SKU}' already exists!");
            }
            //validate category exists
            if (!await _unitOfWork.Categories.ExistsAsync(request.CategoryId))
            {
                throw new Exception($"Category with ID '{request.CategoryId}' does not exist!");
            }
            //validate supplier exists
            if (!await _unitOfWork.Suppliers.ExistsAsync(request.SupplierId))
            {
                throw new Exception($"Supplier with ID '{request.SupplierId}' does not exist!");
            }
            product.SKU = request.SKU;
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.MinimumStockLevel = request.MinimumStockLevel;
            product.CategoryId = request.CategoryId;
            product.SupplierId = request.SupplierId;
            product.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            //re-fetch the product (ensure response reflects db-side changes)
            product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null) return null; //handle potential null

            var response = new ProductResponse
            {
                Id = product.Id,
                SKU = product.SKU,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                MinimumStockLevel = product.MinimumStockLevel,

                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? "Unknown",
                SupplierId = product.SupplierId,
                SupplierName = product.Supplier?.Name ?? "Unknown"
            };
            return response;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null) return false;
            var deleted = await _unitOfWork.Products.DeleteAsync(id);
            if (deleted)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return deleted;
        }

        public async Task<bool> ExistsAsync(string sku)
        {
            return await _unitOfWork.Products.ExistsBySkuAsync(sku);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _unitOfWork.Products.ExistsAsync(id);
        }
    }
}
