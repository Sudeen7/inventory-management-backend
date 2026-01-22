using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.DTOs.WarehouseProduct
{
    public class UpdateStockRequest
    {
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { get; set; } //new quantity
    }
}
