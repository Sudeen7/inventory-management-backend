using InventoryMgmt.Application.DTOs.Category;
using InventoryMgmt.Application.Interfaces;
using InventoryMgmt.Domain.Entities;
using InventoryMgmt.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly IUnitOfWork _unitOfWork;
		public CategoryService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		//GET operations
		public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
		{
			//get all categories from repository
			var categories = await _unitOfWork.Categories.GetAllAsync();

			//Map entities to DTOs
			var response = categories.Select(c => new CategoryResponse
			{
				Id = c.Id,
				Name = c.Name,
				Description = c.Description,
				ProductCount = c.Products?.Count ?? 0, // count of products in this category
			});
			return response;
		}

		public async Task<CategoryResponse?> GetByIdAsync(int id)
		{
			//get category from repository
			var category = await _unitOfWork.Categories.GetByIdAsync(id);
			//if not found, return null
			if (category == null) return null;
			//map entity to DTO
			var response = new CategoryResponse
			{
				Id = category.Id,
				Name = category.Name,
				Description = category.Description,
				ProductCount = category.Products?.Count ?? 0,
			};
			return response;
		}
		public async Task<CategoryResponse?> GetByNameAsync(string name)
		{
			var category = await _unitOfWork.Categories.GetByNameAsync(name);
			if (category == null) return null;
			var response = new CategoryResponse
			{
				Id = category.Id,
				Name = category.Name,
				Description = category.Description,
				ProductCount = category.Products?.Count ?? 0,
			};
			return response;
		}

		//Command operations
		public async Task<CategoryResponse> CreateAsync(CreateCategoryRequest request)
		{
			//check if name already exists
			var exists=await _unitOfWork.Categories.ExistsByNameAsync(request.Name);
			if (exists)
			{
				throw new Exception($"Category with name '{request.Name}' already exists!");
			}

			//map DTO to Entity
			var category = new Category
			{
				Name = request.Name,
				Description = request.Description,
				CreatedAt = DateTime.UtcNow
			};

			//add to repository (tracked, not saved yet)
			var addedCategory=await _unitOfWork.Categories.AddAsync(category);
			//save changes to database
			await _unitOfWork.SaveChangesAsync();

			//map Entity back to DTO
			var response = new CategoryResponse
			{
				Id = category.Id,
				Name = category.Name,
				Description = category.Description,
				ProductCount = 0, // new category has no products yet
			};
			return response;
		}
		public async Task<CategoryResponse?> UpdateAsync(int id, UpdateCategoryRequest request)
		{
			//get existing category
			var category = await _unitOfWork.Categories.GetByIdAsync(id);
			if (category == null) return null; //not found

			//validation: check if new name already exists (for another category)
			var existingCategory = await _unitOfWork.Categories.GetByNameAsync(request.Name);
			if (existingCategory!=null && existingCategory.Id != id)
			{
				throw new Exception($"Category with name '{request.Name}' already exists!");
			}

			//update entity properties
			category.Name= request.Name;
			category.Description= request.Description;
			category.UpdatedAt = DateTime.UtcNow;

			//update repository
			await _unitOfWork.Categories.UpdateAsync(category);
			//save changes to db
			await _unitOfWork.SaveChangesAsync();

			category=await _unitOfWork.Categories.GetByIdAsync(id);
			if(category==null) return null;

			//map response to dto
			var response = new CategoryResponse
			{
				Id = category.Id,
				Name = category.Name,
				Description = category.Description,
				ProductCount = category.Products?.Count ?? 0,
			};
			return response;
		}
		public async Task<bool> DeleteAsync(int id)
		{
			var category=await _unitOfWork.Categories.GetByIdAsync(id);
			if (category == null) return false;

			//validation: do not allow deleting category with products
			if(category.Products!=null && category.Products.Any())
			{
				throw new Exception("Cannot delete category with existing products!");
			}

			//delete from repository
			var deleted=await _unitOfWork.Categories.DeleteAsync(id);

			//save changes to db
			if (deleted)
			{
				await _unitOfWork.SaveChangesAsync();
			}
			return deleted;
		}

		public async Task<bool> ExistsAsync(int id)
		{
			return await _unitOfWork.Categories.ExistsAsync(id);
		}   
	}
}
