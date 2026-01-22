using InventoryMgmt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMgmt.Domain.Interfaces
{
    public interface ISupplierRepository:IRepository<Supplier>
    {
        Task<Supplier?> GetByPhoneAsync(string phone);
        Task<Supplier?> GetByNameAsync(string name);
        Task<Supplier?> GetByEmailAsync(string email);
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsAsync(int id);
    }
}
