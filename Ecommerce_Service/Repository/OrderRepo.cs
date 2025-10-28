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
    public class OrderRepo : IOrderRepo
    {
        private readonly ApplicationDbContext _context;
        public OrderRepo(ApplicationDbContext context) => _context = context;

        public async Task<Result<List<Order>>> GetAll()
        {
            var result = new Result<List<Order>>();
            var list = await _context.OrdersSet
                                     .Include(o => o.OrderDetails)
                                     .ThenInclude(od => od.Product)
                                     .Include(o => o.Payment)
                                     .Include(o => o.User)
                                     .ToListAsync();
            if (list.Any()) result.Response = list;
            else result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "No orders found" });
            return result;
        }

        public async Task<Result<Order>> GetById(int id)
        {
            var result = new Result<Order>();
            var order = await _context.OrdersSet
                                      .Include(o => o.OrderDetails)
                                      .ThenInclude(od => od.Product)
                                      .Include(o => o.Payment)
                                      .Include(o => o.User)
                                      .FirstOrDefaultAsync(o => o.Id == id);
            if (order != null) result.Response = order;
            else result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "Order not found" });
            return result;
        }

        public async Task<Result<Order>> Add(OrderCreateDto model)
        {
            var result = new Result<Order>();

            // ✅ Create the main order record
            var order = new Order
            {
                UserId = model.UserId,
                ShippingAddress = model.ShippingAddress,
                TotalAmount = model.TotalAmount,
                Status = model.Status ?? "Pending",
                OrderDate = DateTime.UtcNow,

                // 👇 FIX: Generate a unique order number (required by DB)
                OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMddHHmmssfff}"
            };

            await _context.OrdersSet.AddAsync(order);
            await _context.SaveChangesAsync();

            // ✅ Add OrderDetails
            if (model.OrderDetails != null && model.OrderDetails.Any())
            {
                foreach (var od in model.OrderDetails)
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = od.ProductId,
                        Quantity = od.Quantity,
                        UnitPrice = od.UnitPrice
                    };
                    await _context.OrderDetailsSet.AddAsync(orderDetail);
                }

                await _context.SaveChangesAsync();
            }

            result.Response = order;
            result.Message = "Order created successfully";
            return result;
        }


        public async Task<Result<Order>> Update(OrderUpdateDto model)
        {
            var result = new Result<Order>();
            var order = await _context.OrdersSet.Include(o => o.OrderDetails)
                                                .FirstOrDefaultAsync(o => o.Id == model.Id);
            if (order == null)
            {
                result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "Order not found" });
                return result;
            }

            order.ShippingAddress = model.ShippingAddress;
            order.Status = model.Status;
            order.TotalAmount = model.TotalAmount;

            _context.OrdersSet.Update(order);
            await _context.SaveChangesAsync();
            result.Response = order;
            result.Message = "Order updated successfully";
            return result;
        }

        public async Task<Result<bool>> Delete(int id)
        {
            var result = new Result<bool>();
            var order = await _context.OrdersSet.FindAsync(id);
            if (order == null)
            {
                result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "Order not found" });
                return result;
            }
            _context.OrdersSet.Remove(order);
            await _context.SaveChangesAsync();
            result.Response = true;
            result.Message = "Order deleted successfully";
            return result;
        }
    }
}
