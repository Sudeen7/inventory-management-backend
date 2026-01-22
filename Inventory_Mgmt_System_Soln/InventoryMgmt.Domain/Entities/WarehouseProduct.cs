using InventoryMgmt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Domain.Entities
{
    public class WarehouseProduct:BaseEntity
    {
        public int Quantity { get; set; }

        //foreign keys
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }

        //navigation props
        public Product Product { get; set; } = null!;
        public Warehouse Warehouse { get; set; } = null!;
    }
}
