using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Domain.Common.Enums
{
    public enum StockMovementType
    {
        In, //purchase, return
        Out, //sale, damage
        Transfer //stock movement between warehouses
    }
}
