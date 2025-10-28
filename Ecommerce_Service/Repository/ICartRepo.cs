using Ecommerce_Common;
using Ecommerce_Entity.DTO;
using Ecommerce_Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_Service.Repository
{
    public interface ICartRepo
    {

        Task<Result<List<Cart>>> GetAll();
        Task<Result<Cart>> GetById(int id);
        Task<Result<Cart>> Add(CartCreateDto model);
        Task<Result<Cart>> Update(CartUpdateDto model);
        Task<Result<bool>> Delete(int id);
    }
}
