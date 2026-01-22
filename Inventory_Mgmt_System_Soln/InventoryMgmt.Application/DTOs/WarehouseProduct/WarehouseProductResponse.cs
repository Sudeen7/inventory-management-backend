using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.DTOs.WarehouseProduct
{
    public class WarehouseProductResponse
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        //product related properties
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductSKU { get; set; }=string.Empty;

        //warehouse related properties
        public int WarehouseId { get; set; }
        public string WarehouseName{ get; set; }=string.Empty;
    }
}
