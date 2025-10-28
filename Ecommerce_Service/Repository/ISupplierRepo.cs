using Ecommerce_Common;
using Ecommerce_Entity.DTO;
using Ecommerce_Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_Service.Repository
{
    public interface ISupplierRepo
    {
        Task<Result<List<Supplier>>> GetAllSuppliers();
        Task<Result<Supplier>> GetSupplierById(int id);
        Task<Result<Supplier>> AddSupplier(SupplierCreateDto model);
        Task<Result<Supplier>> UpdateSupplier(SupplierUpdateDto model);
        Task<Result<bool>> DeleteSupplier(int id);
    }
}
