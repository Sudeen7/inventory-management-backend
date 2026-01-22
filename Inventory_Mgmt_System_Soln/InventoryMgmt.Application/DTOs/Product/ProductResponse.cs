using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.DTOs.Product
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string SKU { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int MinimumStockLevel { get; set; }

        //category related properties
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        //supplier related properties
        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = string.Empty;

    }
}
