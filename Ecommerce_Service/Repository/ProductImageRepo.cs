using Ecommerce_Common;
using Ecommerce_Entity.Data;
using Ecommerce_Entity.DTO;
using Ecommerce_Entity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecommerce_Service.Repository
{
    public class ProductImageRepo : IProductImageRepo
    {
        private readonly ApplicationDbContext _context;
        public ProductImageRepo(ApplicationDbContext context) => _context = context;

        public async Task<Result<List<ProductImage>>> GetAll()
        {
            var result = new Result<List<ProductImage>>();
            var images = await _context.ProductImagesSet.Include(p => p.Product).ToListAsync();
            if (images.Any()) result.Response = images;
            else result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "No Product Images Found" });
            return result;
        }

        public async Task<Result<ProductImage>> GetById(int id)
        {
            var result = new Result<ProductImage>();
            var image = await _context.ProductImagesSet.Include(p => p.Product).FirstOrDefaultAsync(p => p.Id == id);
            if (image != null) result.Response = image;
            else result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "Product Image Not Found" });
            return result;
        }

        public async Task<Result<ProductImage>> Add(ProductImageCreateDto model)
        {
            var result = new Result<ProductImage>();
            var image = new ProductImage
            {
                ProductId = model.ProductId,
                ImageUrl = model.ImageUrl,
                IsPrimary = model.IsPrimary
            };
            await _context.ProductImagesSet.AddAsync(image);
            await _context.SaveChangesAsync();
            result.Response = image;
            result.Message = "Product image added successfully";
            return result;
        }
        public async Task<Result<List<ProductImage>>> AddMultipleFiles(ProductImageUploadDto model, IWebHostEnvironment env)
        {
            var result = new Result<List<ProductImage>>();

            if (model.Images == null || !model.Images.Any())
            {
                result.Errors.Add(new Errors { ErrorCode = 400, ErrorMessage = "No files provided." });
                return result;
            }

            // Ensure WebRootPath is set
            string webRootPath = env.WebRootPath;
            if (string.IsNullOrEmpty(webRootPath))
                webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            string uploadPath = Path.Combine(webRootPath, "uploads");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var imagesList = new List<ProductImage>();

            for (int i = 0; i < model.Images.Count; i++)
            {
                var file = model.Images[i];
                string fileExtension = Path.GetExtension(file.FileName);

                // Avoid duplicate file names
                string fileName = $"{Guid.NewGuid()}{fileExtension}";
                string filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                imagesList.Add(new ProductImage
                {
                    ProductId = model.ProductId,
                    ImageUrl = $"uploads/{fileName}",  // URL accessible via browser
                    IsPrimary = i == model.PrimaryIndex
                });
            }

            await _context.ProductImagesSet.AddRangeAsync(imagesList);
            await _context.SaveChangesAsync();

            result.Response = imagesList;
            result.Message = $"{imagesList.Count} images uploaded successfully.";
            return result;
        }




        public async Task<Result<ProductImage>> Update(ProductImageUpdateDto model)
        {
            var result = new Result<ProductImage>();
            var image = await _context.ProductImagesSet.FindAsync(model.Id);
            if (image == null)
            {
                result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "Product Image Not Found" });
                return result;
            }
            image.ProductId = model.ProductId;
            image.ImageUrl = model.ImageUrl;
            image.IsPrimary = model.IsPrimary;
            _context.ProductImagesSet.Update(image);
            await _context.SaveChangesAsync();
            result.Response = image;
            result.Message = "Product image updated successfully";
            return result;
        }

        public async Task<Result<bool>> Delete(int id)
        {
            var result = new Result<bool>();
            var image = await _context.ProductImagesSet.FindAsync(id);
            if (image == null)
            {
                result.Errors.Add(new Errors { ErrorCode = 404, ErrorMessage = "Product Image Not Found" });
                return result;
            }
            _context.ProductImagesSet.Remove(image);
            await _context.SaveChangesAsync();
            result.Response = true;
            result.Message = "Product image deleted successfully";
            return result;
        }
    }
}
