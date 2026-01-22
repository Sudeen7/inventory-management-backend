using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.DTOs.Category
{
    public class UpdateCategoryRequest
    {
        //ID from URL
        public string Name { get; set; }=string.Empty;
        public string? Description { get; set; }
    }
}
