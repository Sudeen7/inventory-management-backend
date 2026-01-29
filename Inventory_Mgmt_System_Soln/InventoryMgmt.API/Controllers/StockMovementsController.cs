using InventoryMgmt.Application.DTOs.StockMovement;
using InventoryMgmt.Application.Interfaces;
using InventoryMgmt.Domain.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMgmt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockMovementsController : ControllerBase
    {
        private readonly IStockMovementService _stockMovementService;
        public StockMovementsController(IStockMovementService stockMovementService)
        {
            _stockMovementService = stockMovementService;
        }

        //GET: api/stockmovements
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stockmovements = await _stockMovementService.GetAllAsync();
            return Ok(stockmovements);
        }

        //GET: api/stockmovements/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stockMovement = await _stockMovementService.GetByIdAsync(id);
            if (stockMovement == null)
            {
                return NotFound($"StockMovement with ID {id} not found!");
            }
            return Ok(stockMovement);
        }

        //GET: api/stockmovements/warehouse/{warehouseId}
        [HttpGet("warehouse/{warehouseId}")]
        public async Task<IActionResult> GetByWarehouse(int warehouseId)
        {
            var stockmovements = await _stockMovementService.GetByWarehouseAsync(warehouseId);
            return Ok(stockmovements);
        }

        //GET: api/stockmovements/product/{productId}
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId)
        {
            var stockmovements = await _stockMovementService.GetByProductAsync(productId);
            return Ok(stockmovements);
        }

        //GET: api/stockmovements/date-range?{startDate}&{endDate}
        [HttpGet("date-range")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var stockmovements = await _stockMovementService.GetByDateRangeAsync(startDate, endDate);
            return Ok(stockmovements);
        }

        //GET: api/stockmovements/type/{movementType}
        [HttpGet("type/{movementType}")]
        public async Task<IActionResult> GetByStockMovementType(StockMovementType movementType)
        {
            var stockmovements = await _stockMovementService.GetByStockMovementTypeAsync(movementType);
            return Ok(stockmovements);
        }

        //POST: api/stockmovements
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockMovementRequest request)
        {
            try
            {
                var stockMovement = await _stockMovementService.CreateAsync(request);
                return CreatedAtAction(
                            nameof(GetById),
                            new { id = stockMovement.Id },
                            stockMovement
                        );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //POST: api/stockmovements/transfer
        [HttpPost("transfer")]
        public async Task<IActionResult> CreateTranfer(
                                            [FromQuery] int productId,
                                            [FromQuery] int sourceWarehouseId,
                                            [FromQuery] int destinationWarehouseId,
                                            [FromQuery] int quantity,
                                            [FromQuery] string? reference = null,
                                            [FromQuery] string? notes = null)
        {
            try
            {
                var movements = await _stockMovementService.CreateTransferAsync(
                                                                productId,
                                                                sourceWarehouseId,
                                                                destinationWarehouseId,
                                                                quantity,
                                                                reference,
                                                                notes
                                                            );

                return Ok(movements); //returns both OUT and IN movements
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
