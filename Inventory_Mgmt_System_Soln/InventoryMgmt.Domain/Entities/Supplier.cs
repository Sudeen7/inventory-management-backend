using InventoryMgmt.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Domain.Entities
{
    public class Supplier:BaseEntity
    {
        public string Name { get; set; }=string.Empty;
        public string Address { get; set; }=string.Empty;
        public string? Email { get; set; }
        public string Phone { get; set; } = string.Empty;

        //navigation props
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
