using InventoryMgmt.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Domain.Entities
{
    public class Product:BaseEntity
    {
        public string SKU { get; set; } = string.Empty; //SKU - Stock Keeping Unit (Unique Identifier)
        public string Name { get; set; }=string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int MinimumStockLevel { get; set; }

        //foreign keys
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }

        //navigation properties
        public Category Category { get; set; } = null!; //null!-->(!) is null-forgiving operator
        public Supplier Supplier { get; set; } = null!;
        public ICollection<WarehouseProduct> WarehouseProducts { get; set; } = new List<WarehouseProduct>();
        public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
    }
}
