using Ecommerce_Common;
using Ecommerce_Entity.DTO;
using Ecommerce_Entity.Models;
using System.Threading.Tasks;

namespace Ecommerce_Service.Repository
{
    public interface IUserRepo
    {
        Task<Result<UserResponse>> RegisterUserAsync(RegisterDto model);
        Task<Result<UserResponse>> RegisterSupplierAsync(SupplierCreateDto model);
        Task<Result<UserResponse>> LoginAsync(LoginDto model);
        Task<string> GenerateToken(ApplicationUser user);
    }
}
