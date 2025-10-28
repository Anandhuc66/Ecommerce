    using Ecommerce_Common;
    using Ecommerce_Entity.DTO;
    using Ecommerce_Entity.Models;
    using Ecommerce_Service.Repository;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    namespace Ecommerce_Api.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "Admin")]
        public class SupplierController : ControllerBase
        {
            private readonly ISupplierRepo _repo;

            public SupplierController(ISupplierRepo repo)
            {
                _repo = repo;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var result = await _repo.GetAllSuppliers();
                if (result.isError) return NotFound(result);
                return Ok(result);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> Get(int id)
            {
                var result = await _repo.GetSupplierById(id);
                if (result.isError) return NotFound(result);
                return Ok(result);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] SupplierCreateDto model)
            {
                var result = await _repo.AddSupplier(model);
                if (result.isError) return BadRequest(result);
                return Ok(result);
            }

            [HttpPut]
            public async Task<IActionResult> Update([FromBody] SupplierUpdateDto model)
            {
                var result = await _repo.UpdateSupplier(model);
                if (result.isError) return BadRequest(result);
                return Ok(result);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var result = await _repo.DeleteSupplier(id);
                if (result.isError) return BadRequest(result);
                return Ok(result);
            }
        }
    }
