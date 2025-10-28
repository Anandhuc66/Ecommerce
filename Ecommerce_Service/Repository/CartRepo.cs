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
    public class CartRepo : ICartRepo
    {
        private readonly ApplicationDbContext _context;
        public CartRepo(ApplicationDbContext context) => _context = context;

        public async Task<Result<List<Cart>>> GetAll()
        {
            var result = new Result<List<Cart>>();
            var carts = await _context.CartsSet.Include(c => c.User)
                                               .Include(c => c.Product)
                                               .ToListAsync();
            if (carts.Any()) result.Response = carts;
            else result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "No carts found" });
            return result;
        }

        public async Task<Result<Cart>> GetById(int id)
        {
            var result = new Result<Cart>();
            var cart = await _context.CartsSet.Include(c => c.User)
                                              .Include(c => c.Product)
                                              .FirstOrDefaultAsync(c => c.CartId == id);
            if (cart != null) result.Response = cart;
            else result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "Cart not found" });
            return result;
        }

        public async Task<Result<Cart>> Add(CartCreateDto model)
        {
            var result = new Result<Cart>();
            var cart = new Cart
            {
                UserId = model.UserId,
                ProductId = model.ProductId,
                Quantity = model.Quantity
            };
            await _context.CartsSet.AddAsync(cart);
            await _context.SaveChangesAsync();
            result.Response = cart;
            result.Message = "Cart added successfully";
            return result;
        }

        public async Task<Result<Cart>> Update(CartUpdateDto model)
        {
            var result = new Result<Cart>();
            var cart = await _context.CartsSet.FindAsync(model.CartId);
            if (cart == null)
            {
                result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "Cart not found" });
                return result;
            }
            cart.ProductId = model.ProductId;
            cart.UserId = model.UserId;
            cart.Quantity = model.Quantity;
            _context.CartsSet.Update(cart);
            await _context.SaveChangesAsync();
            result.Response = cart;
            result.Message = "Cart updated successfully";
            return result;
        }

        public async Task<Result<bool>> Delete(int id)
        {
            var result = new Result<bool>();
            var cart = await _context.CartsSet.FindAsync(id);
            if (cart == null)
            {
                result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "Cart not found" });
                return result;
            }
            _context.CartsSet.Remove(cart);
            await _context.SaveChangesAsync();
            result.Response = true;
            result.Message = "Cart deleted successfully";
            return result;
        }
    }
}
