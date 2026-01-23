using InventoryMgmt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Domain.Entities
{
    public class Warehouse:BaseEntity
    {
        public string Name { get; set; }=string.Empty;
        public string Location { get; set; }=string.Empty;
        public int? Capacity { get; set; }

        //navigation props
        public ICollection<WarehouseProduct> WarehouseProducts { get; set; } = new List<WarehouseProduct>();
        public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();

    }
}
