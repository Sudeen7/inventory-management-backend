using InventoryMgmt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Domain.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        //navigation props
        public ICollection<Product> Products { get; set; }=new List<Product>();
    }
}
