using InventoryMgmt.Application.DTOs.WarehouseProduct;
using InventoryMgmt.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMgmt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseProductsController : ControllerBase
    {
        private readonly IWarehouseProductService _warehouseProductService;
        public WarehouseProductsController(IWarehouseProductService warehouseProductService)
        {
            _warehouseProductService=warehouseProductService;
        }

        //GET: api/warehouseproducts
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var warehouseProducts=await _warehouseProductService.GetAllAsync();
            return Ok(warehouseProducts);
        }

        //GET: api/warehouseproducts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var warehouseProduct=await _warehouseProductService.GetByIdAsync(id);
            if (warehouseProduct == null)
            {
                return NotFound($"WarehouseProduct with ID {id} not found!");
            }
            return Ok(warehouseProduct);
        }

        //GET: api/warehouseproducts/warehouse/{id}
        [HttpGet("warehouse/{warehouseId}")]
        public async Task<IActionResult> GetByWarehouse(int warehouseId)
        {
            var warehouseProducts=await _warehouseProductService.GetByWarehouseAsync(warehouseId);
            return Ok(warehouseProducts);
        }

        //GET: api/warehouseproducts/product/{id}
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId)
        {
            var warehouseProducts=await _warehouseProductService.GetByProductAsync(productId);
            return Ok(warehouseProducts);
        }

        //GET: api/warehouseproducts/warehouse/{warehouseId}/product/{productId}
        [HttpGet("warehouse/{warehouseId}/product/{productId}")]
        public async Task<IActionResult> GetByWarehouseAndProduct(int warehouseId, int productId)
        {
            var warehouseProduct=await _warehouseProductService.GetByWarehouseAndProductAsync(warehouseId,productId);
            if (warehouseProduct == null)
            {
                return NotFound($"Product-{productId} not found at Warehouse-{warehouseId}.");
            }
            return Ok(warehouseProduct);
        }

        //GET: api/warehouseproducts/product/{productId}/total-stock
        [HttpGet("product/{productId}/total-stock")]
        public async Task<IActionResult> GetTotalStockForProduct(int productId)
        {
            var totalStock=await _warehouseProductService.GetTotalStockForProductAsync(productId);
            return Ok(new{productId, totalStock});
        }

        //PUT: api/warehouseproducts/{stock}
        [HttpPut("stock")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStockRequest request)
        {
            try
            {
                var warehouseProduct=await _warehouseProductService.UpdateStockAsync(request);
                return Ok(warehouseProduct);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //DELETE: api/warehouseproducts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted=await _warehouseProductService.DeleteAsync(id);

                if (!deleted)
                {
                    return NotFound($"WarehouseProduct with ID {id} not found!");

                }
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
