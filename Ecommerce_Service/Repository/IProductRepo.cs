using Ecommerce_Common;
using Ecommerce_Entity.DTO;
using Ecommerce_Entity.Models;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_Service.Repository
{
    public interface IProductRepo
    {
        Task<Result<List<ProductDto>>> GetAllProducts();
        Task<Result<ProductDto>> GetProductById(int id);
        Task<Result<Product>> AddProduct(ProductCreateDto model);
        Task<Result<Product>> AddProductWithImages(ProductWithImagesCreateDto model, IWebHostEnvironment env);

        Task<Result<Product>> UpdateProduct(ProductUpdateDto model);
        Task<Result<bool>> DeleteProduct(int id);
    }
}
