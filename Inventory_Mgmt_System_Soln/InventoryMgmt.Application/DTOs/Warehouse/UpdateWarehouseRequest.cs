using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.DTOs.Warehouse
{
    public class UpdateWarehouseRequest
    {
        public string Name { get; set; }=string.Empty;
        public string Location { get; set; } = string.Empty;
        public int? Capacity { get; set; }
    }
}
