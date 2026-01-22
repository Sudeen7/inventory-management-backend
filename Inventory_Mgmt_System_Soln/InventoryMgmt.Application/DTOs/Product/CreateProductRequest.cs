using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.DTOs.Product
{
    public class CreateProductRequest
    {
        //ID is created by database in create request
        public string SKU { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int MinimumStockLevel { get; set; }

        //related IDs to link
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
    }
}
