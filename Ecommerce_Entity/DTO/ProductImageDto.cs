using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Ecommerce_Entity.DTO
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; }
    }
    public class ProductImageCreateDto
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; }
    }
    public class ProductImageUpdateDto : ProductImageCreateDto
    {
        public int Id { get; set; }
    }
    public class ProductImageBulkCreateDto
    {
        public int ProductId { get; set; }
        public List<ProductImageCreateDto> Images { get; set; }
    }
    public class ProductImageUploadDto
    {
        public int ProductId { get; set; }
        public List<IFormFile> Images { get; set; } = new();
        public int PrimaryIndex { get; set; } = 0; // Index of primary image in the list
    }
}
