using Ecommerce_Common;
using Ecommerce_Entity.DTO;
using Ecommerce_Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_Service.Repository
{
    public interface IOrderRepo
    {
        Task<Result<List<Order>>> GetAll();
        Task<Result<Order>> GetById(int id);
        Task<Result<Order>> Add(OrderCreateDto model);
        Task<Result<Order>> Update(OrderUpdateDto model);
        Task<Result<bool>> Delete(int id);
    }
}
