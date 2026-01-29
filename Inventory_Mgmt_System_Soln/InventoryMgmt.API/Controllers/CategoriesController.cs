using InventoryMgmt.Application.DTOs.Category;
using InventoryMgmt.Application.Interfaces;
using InventoryMgmt.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMgmt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //GET: api/categories
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        //GET: api/categories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound($"Category with ID {id} not found!");
            }
            return Ok(category);
        }

        //POST: api/categories
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        {
            try
            {
                var category = await _categoryService.CreateAsync(request);
                return CreatedAtAction(
                            nameof(GetById),            //ActionName: "GetById"
                            new { id = category.Id },   //Route values: {id}
                            category                    //Response body
                            );
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //PUT: api/categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryRequest request)
        {
            try
            {
                var category = await _categoryService.UpdateAsync(id, request);

                if (category == null)
                {
                    return NotFound($"Category with ID {id} not found!");
                }

                return Ok(category);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //DELETE: api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _categoryService.DeleteAsync(id);

                if (!deleted)
                {
                    return NotFound($"Category with ID {id} not found!");
                }

                return NoContent();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
