using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Ecommerce_Entity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Gender { get; set; }  // Male / Female / Other
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        // Navigation properties
        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
