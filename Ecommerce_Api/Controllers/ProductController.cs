using Ecommerce_Entity.DTO;
using Ecommerce_Entity.Models;
using Ecommerce_Service.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _repo;
        public ProductController(IProductRepo repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllProducts());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _repo.GetProductById(id);
            if (result.isError) return NotFound(result);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Supplier")]
        [HttpPost("add-with-images")]
        public async Task<IActionResult> CreateWithImages([FromForm] ProductWithImagesCreateDto model, [FromServices] IWebHostEnvironment env)
        {
            var result = await _repo.AddProductWithImages(model, env);
            if (result.isError) return BadRequest(result);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Supplier")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto model)
        {
            var result = await _repo.AddProduct(model);
            if (result.isError) return BadRequest(result);
            return Ok(result);
        }

        [Authorize(Roles = "Admin,Supplier")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateDto model)
        {
            var result = await _repo.UpdateProduct(model);
            if (result.isError) return BadRequest(result);
            return Ok(result);
        }
        [Authorize(Roles = "Admin,Supplier")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repo.DeleteProduct(id);
            if (result.isError) return BadRequest(result);
            return Ok(result);
        }
    }
}
