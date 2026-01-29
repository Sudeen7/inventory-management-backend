using InventoryMgmt.Application.DTOs.Warehouse;
using InventoryMgmt.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMgmt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;
        public WarehousesController(IWarehouseService warehouseService)
        {
            _warehouseService=warehouseService;
        }

        //GET: api/warehouses
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var warehouses=await _warehouseService.GetAllAsync();
            return Ok(warehouses);
        }

        //GET: api/warehouses/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var warehouse=await _warehouseService.GetByIdAsync(id);

            if (warehouse == null)
            {
                return NotFound($"Warehouse with ID {id} not found!");
            }

            return Ok(warehouse);
        }

        //GET: api/warehouse/name/{name}
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var warehouse=await _warehouseService.GetByNameAsync(name);
            if (warehouse == null)
            {
                return NotFound($"Warehouse with name '{name}' not found!");
            }

            return Ok(warehouse);
        }

        //GET: api/warehouse/location/{location}
        [HttpGet("location/{location}")]
        public async Task<IActionResult> GetByLocation(string location)
        {
            var warehouse=await _warehouseService.GetByLocationAsync(location);
            
            return Ok(warehouse);
        }

        //POST: api/warehouses
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWarehouseRequest request)
        {
            try
            {
                var warehouse=await _warehouseService.CreateAsync(request);
                return CreatedAtAction(
                        nameof(GetById),
                        new{id=warehouse.Id},
                        warehouse
                        );
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //PUT: api/warehouses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateWarehouseRequest request)
        {
            try
            {
                var warehouse=await _warehouseService.UpdateAsync(id, request);

                if (warehouse == null)
                {
                    return NotFound($"Warehouse with ID {id} not found!");
                }

                return Ok(warehouse);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //DELETE: api/warehouses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted=await _warehouseService.DeleteAsync(id);

                if (!deleted)
                {
                    return NotFound($"Warehouse with ID {id} not found!");

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
