using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.DTOs.Warehouse
{
    public class WarehouseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; }=string.Empty;
        public int? Capacity { get; set; }
        public int TotalProductsStored { get; set; } //count of different products
    }
}
