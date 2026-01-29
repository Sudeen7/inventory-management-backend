using InventoryMgmt.Application.DTOs.Product;
using InventoryMgmt.Application.Interfaces;
using InventoryMgmt.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMgmt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        //GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        //GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound($"Product with ID {id} not found!");
            }
            return Ok(product);
        }

        //GET: api/products/sku/{sku}
        [HttpGet("sku/{sku}")]
        public async Task<IActionResult> GetBySku(string sku)
        {
            var product = await _productService.GetBySkuAsync(sku);

            if (product == null)
            {
                return NotFound($"Product with SKU {sku} not found!");
            }
            return Ok(product);
        }

        //GET: api/products/category/{categoryId}
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var products = await _productService.GetByCategoryAsync(categoryId);

            return Ok(products); //returns empty array if no products in category
        }

        //GET: api/products/low-stock
        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStock()
        {
            var products = await _productService.GetLowStockAsync();
            return Ok(products);
        }

        //POST: api/products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            try
            {
                var product = await _productService.CreateAsync(request);
                return CreatedAtAction(
                            nameof(GetById),
                            new { id = product.Id },
                            product
                            );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request)
        {
            try
            {
                var product = await _productService.UpdateAsync(id, request);

                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found!");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _productService.DeleteAsync(id);

                if (!deleted)
                {
                    return NotFound($"Product with ID {id} not found!");
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
