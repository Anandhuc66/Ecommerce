    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Ecommerce_Entity.DTO
    {
        public class SupplierDto
        {
            public int Id { get; set; }
            public string CompanyName { get; set; }
            public string ContactEmail { get; set; }
            public string Phone { get; set; }
        }
        public class SupplierCreateDto
        {
            public string CompanyName { get; set; }
            public string ContactEmail { get; set; }
            public string Phone { get; set; }
            public string FullName { get; set; }
            public string Gender { get; set; }
            public string Password { get; set; }
    }

        public class SupplierUpdateDto : SupplierCreateDto
        {
            public int Id { get; set; }
        }
    }
