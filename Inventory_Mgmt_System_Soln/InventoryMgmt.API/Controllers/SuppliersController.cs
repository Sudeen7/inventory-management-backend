using InventoryMgmt.Application.DTOs.Supplier;
using InventoryMgmt.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMgmt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService=supplierService;
        }

        //GET: api/suppliers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var suppliers=await _supplierService.GetAllAsync();
            return Ok(suppliers);
        }

        //GET: api/suppliers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var supplier=await _supplierService.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound($"Supplier with ID {id} not found!");
            }
            return Ok(supplier);
        }

        //GET: api/suppliers/phone/{phone}
        [HttpGet("phone/{phone}")]
        public async Task<IActionResult> GetByPhone(string phone)
        {
            var supplier=await _supplierService.GetByPhoneAsync(phone);
            if(supplier == null)
            {
                return NotFound($"Supplier with phone number '{phone}' not found!");
            }
            return Ok(supplier);
        }

        //GET: api/suppliers/name/{name}
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var supplier=await _supplierService.GetByNameAsync(name);
            if (supplier == null)
            {
                return NotFound($"Supplier with name '{name}' not found!");
            }
            return Ok(supplier);
        }

        //POST: api/suppliers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSupplierRequest request)
        {
            try
            {
                var supplier=await _supplierService.CreateAsync(request);
                return CreatedAtAction(
                        nameof(GetById),
                        new{id=supplier.Id},
                        supplier
                        );
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //PUT: api/suppliers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSupplierRequest request)
        {
            try
            {
                var supplier=await _supplierService.UpdateAsync(id,request);

                if (supplier == null)
                {
                    return NotFound($"Supplier with ID {id} not found!");
                }

                return Ok(supplier);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //DELETE: api/suppliers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted=await _supplierService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound($"Supplier with ID {id} not found!");
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
