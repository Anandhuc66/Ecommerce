using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Ecommerce_Entity.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // ✅ Add this new property for images
        public List<string> ImageUrls { get; set; } = new();
    }

    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? SKU { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
    }

    public class ProductUpdateDto : ProductCreateDto
    {
        public int Id { get; set; }
    }

    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string SKU { get; set; }
        public object Category { get; set; }
        public object Supplier { get; set; }

        // ✅ Optional: you can also align this one
        public List<string> ImageUrls { get; set; } = new();
    }

        public class ProductWithImagesCreateDto
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public int StockQuantity { get; set; }
            public string SKU { get; set; }
            public int CategoryId { get; set; }
            public int SupplierId { get; set; }

            // Image upload support
            public List<IFormFile>? Images { get; set; }
        }
    

}
