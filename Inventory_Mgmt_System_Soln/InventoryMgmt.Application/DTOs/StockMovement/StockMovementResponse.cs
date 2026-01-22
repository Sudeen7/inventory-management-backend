using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryMgmt.Domain.Common.Enums;

namespace InventoryMgmt.Application.DTOs.StockMovement
{
    public class StockMovementResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = string.Empty;
        public StockMovementType MovementType { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }
        public string? Reference { get; set; }
        public string? Notes { get; set; }
    }
}
