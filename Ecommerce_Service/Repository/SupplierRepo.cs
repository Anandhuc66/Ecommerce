using Ecommerce_Common;
using Ecommerce_Entity.Data;
using Ecommerce_Entity.DTO;
using Ecommerce_Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Service.Repository
{
    public class SupplierRepo : ISupplierRepo
    {
        private readonly ApplicationDbContext _context;

        public SupplierRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<Supplier>>> GetAllSuppliers()
        {
            var result = new Result<List<Supplier>>();
            var suppliers = await _context.SuppliersSet.Include(s => s.Products).ToListAsync();
            if (suppliers.Any()) result.Response = suppliers;
            else result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "No Suppliers Found" });
            return result;
        }

        public async Task<Result<Supplier>> GetSupplierById(int id)
        {
            var result = new Result<Supplier>();
            var supplier = await _context.SuppliersSet.Include(s => s.Products)
                                                      .FirstOrDefaultAsync(s => s.Id == id);
            if (supplier != null) result.Response = supplier;
            else result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "Supplier Not Found" });
            return result;
        }

        public async Task<Result<Supplier>> AddSupplier(SupplierCreateDto model)
        {
            var result = new Result<Supplier>();
            var supplier = new Supplier
            {
                CompanyName = model.CompanyName,
                ContactEmail = model.ContactEmail,
                Phone = model.Phone
            };
            await _context.SuppliersSet.AddAsync(supplier);
            await _context.SaveChangesAsync();
            result.Response = supplier;
            result.Message = "Supplier added successfully";
            return result;
        }

        public async Task<Result<Supplier>> UpdateSupplier(SupplierUpdateDto model)
        {
            var result = new Result<Supplier>();
            var supplier = await _context.SuppliersSet.FindAsync(model.Id);
            if (supplier == null)
            {
                result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "Supplier Not Found" });
                return result;
            }
            supplier.CompanyName = model.CompanyName;
            supplier.ContactEmail = model.ContactEmail;
            supplier.Phone = model.Phone;
            _context.SuppliersSet.Update(supplier);
            await _context.SaveChangesAsync();
            result.Response = supplier;
            result.Message = "Supplier updated successfully";
            return result;
        }

        public async Task<Result<bool>> DeleteSupplier(int id)
        {
            var result = new Result<bool>();
            var supplier = await _context.SuppliersSet.FindAsync(id);
            if (supplier == null)
            {
                result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "Supplier Not Found" });
                return result;
            }
            _context.SuppliersSet.Remove(supplier);
            await _context.SaveChangesAsync();
            result.Response = true;
            result.Message = "Supplier deleted successfully";
            return result;
        }
    }
}
