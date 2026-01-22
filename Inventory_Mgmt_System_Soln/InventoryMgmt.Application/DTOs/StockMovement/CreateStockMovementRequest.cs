using InventoryMgmt.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.DTOs.StockMovement
{
    public class CreateStockMovementRequest
    {
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public StockMovementType MovementType{ get; set; }
        public int Quantity { get; set; }
        public string? Reference { get; set; }
        public string? Notes{ get; set; }
        //movement date to be set by the server

    }
}
