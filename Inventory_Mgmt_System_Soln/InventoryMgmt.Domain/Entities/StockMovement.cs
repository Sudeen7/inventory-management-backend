using InventoryMgmt.Domain.Common;
using InventoryMgmt.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Domain.Entities
{
    public class StockMovement : BaseEntity
    {
        public StockMovementType MovementType { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }
        public string? Reference { get; set; } //order number, etc.
        public string? Notes { get; set; }

        //foreign keys
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }

        //navigation props
        public Product Product { get; set; } = null!;
        public Warehouse Warehouse { get; set; } = null!;
    }
}
