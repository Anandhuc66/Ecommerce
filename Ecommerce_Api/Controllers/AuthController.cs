using Ecommerce_Common;
using Ecommerce_Entity.DTO;
using Ecommerce_Service.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce_Api.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public AuthController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }


        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto model)
        {
            var result = await _userRepo.RegisterUserAsync(model);
            if (result.isError)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("register-supplier")]
        public async Task<IActionResult> RegisterSupplier([FromBody] SupplierCreateDto model)
        {
            var result = await _userRepo.RegisterSupplierAsync(model);
            if (result.isError)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _userRepo.LoginAsync(model);
            if (result.isError)
                return BadRequest(result);

            return Ok(result);
        }

    }
}

