using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Application.DTOs.Category
{
    public class CreateCategoryRequest
    {
        //ID from db
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
