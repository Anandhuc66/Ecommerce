using Ecommerce_Common;
using Ecommerce_Entity.Data;
using Ecommerce_Entity.DTO;
using Ecommerce_Entity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Service.Repository
{
    public class ProductRepo : IProductRepo
    {
        private readonly ApplicationDbContext _context;
        public ProductRepo(ApplicationDbContext context) => _context = context;

        public async Task<Result<List<ProductDto>>> GetAllProducts()
        {
            var result = new Result<List<ProductDto>>();

            var products = await _context.ProductsSet
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Include(p => p.Images)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.Name : null,
                    SupplierId = p.SupplierId,
                    SupplierName = p.Supplier != null ? p.Supplier.CompanyName : null,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    ImageUrls = p.Images.Select(img => img.ImageUrl).ToList()
                })
                .ToListAsync();

            if (products.Any())
                result.Response = products;
            else
                result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "No Products Found" });

            return result;
        }

        public async Task<Result<ProductDto>> GetProductById(int id)
        {
            var result = new Result<ProductDto>();

            var product = await _context.ProductsSet
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Include(p => p.Images)
                .Where(p => p.Id == id)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.Name : null,
                    SupplierId = p.SupplierId,
                    SupplierName = p.Supplier != null ? p.Supplier.CompanyName : null,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    ImageUrls = p.Images.Select(img => img.ImageUrl).ToList()

                })
                .FirstOrDefaultAsync();

            if (product != null)
                result.Response = product;
            else
                result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "Product Not Found" });

            return result;
        }



        public async Task<Result<Product>> AddProduct(ProductCreateDto model)
        {
            var result = new Result<Product>();
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                StockQuantity = model.StockQuantity,
                SKU = model.SKU,
                CategoryId = model.CategoryId,
                SupplierId = model.SupplierId
            };
            await _context.ProductsSet.AddAsync(product);
            await _context.SaveChangesAsync();
            result.Response = product;
            result.Message = "Product added successfully";
            return result;
        }

        public async Task<Result<Product>> AddProductWithImages(ProductWithImagesCreateDto model, IWebHostEnvironment env)
        {
            var result = new Result<Product>();

            // 1️⃣ Save product first
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                StockQuantity = model.StockQuantity,
                SKU = model.SKU,
                CategoryId = model.CategoryId,
                SupplierId = model.SupplierId
            };

            await _context.ProductsSet.AddAsync(product);
            await _context.SaveChangesAsync();

            // 2️ Save images (if any)
            if (model.Images != null && model.Images.Count > 0)
            {
                string webRootPath = env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                string uploadPath = Path.Combine(webRootPath, "uploads");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var imageList = new List<ProductImage>();

                foreach (var file in model.Images)
                {
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    string filePath = Path.Combine(uploadPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    imageList.Add(new ProductImage
                    {
                        ProductId = product.Id,
                        ImageUrl = $"uploads/{fileName}",
                        IsPrimary = false
                    });
                }

                await _context.ProductImagesSet.AddRangeAsync(imageList);
                await _context.SaveChangesAsync();
            }

            result.Response = product;
            result.Message = "Product and images added successfully";
            return result;
        }

        public async Task<Result<Product>> UpdateProduct(ProductUpdateDto model)
        {
            var result = new Result<Product>();
            var product = await _context.ProductsSet.FindAsync(model.Id);
            if (product == null)
            {
                result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "Product Not Found" });
                return result;
            }
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.StockQuantity = model.StockQuantity;
            product.SKU = model.SKU;
            product.CategoryId = model.CategoryId;
            product.SupplierId = model.SupplierId;

            _context.ProductsSet.Update(product);
            await _context.SaveChangesAsync();
            result.Response = product;
            result.Message = "Product updated successfully";
            return result;
        }

        public async Task<Result<bool>> DeleteProduct(int id)
        {
            var result = new Result<bool>();
            var product = await _context.ProductsSet.FindAsync(id);
            if (product == null)
            {
                result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "Product Not Found" });
                return result;
            }
            _context.ProductsSet.Remove(product);
            await _context.SaveChangesAsync();
            result.Response = true;
            result.Message = "Product deleted successfully";
            return result;
        }
    }
}
