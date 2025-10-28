using Ecommerce_Entity.DTO;
using Ecommerce_Service.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageRepo _repo;
        private readonly IWebHostEnvironment _env;
        public ProductImageController(IProductImageRepo repo, IWebHostEnvironment env)
        {
            _env = env;
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _repo.GetById(id);
            if (result.isError) return NotFound(result);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Supplier")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductImageCreateDto model)
        {
            var result = await _repo.Add(model);
            if (result.isError) return BadRequest(result);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Supplier")]
        [HttpPost("upload-multiple")]
        public async Task<IActionResult> UploadMultiple([FromForm] ProductImageUploadDto model)
        {
            var result = await _repo.AddMultipleFiles(model, _env);

            if (result.Errors.Any())
                return BadRequest(result.Errors);

            return Ok(result);
        }

        [Authorize(Roles = "Admin,Supplier")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductImageUpdateDto model)
        {
            var result = await _repo.Update(model);
            if (result.isError) return BadRequest(result);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Supplier")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repo.Delete(id);
            if (result.isError) return BadRequest(result);
            return Ok(result);
        }
    }
}
