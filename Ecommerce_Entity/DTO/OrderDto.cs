using System;
using System.Collections.Generic;

namespace Ecommerce_Entity.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string ShippingAddress { get; set; }

        public List<OrderDetailDto> OrderDetails { get; set; }
        public PaymentDto Payment { get; set; }
    }

    public class OrderCreateDto
    {
        public string UserId { get; set; }
        public string ShippingAddress { get; set; }
        public List<OrderDetailCreateDto> OrderDetails { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
    public class OrderUpdateDto : OrderCreateDto
    {
        public int Id { get; set; }
    }

    //public class OrderDetailDto
    //{
    //    public int ProductId { get; set; }
    //    public string ProductName { get; set; }
    //    public int Quantity { get; set; }
    //    public decimal UnitPrice { get; set; }
    //}

    //public class OrderDetailCreateDto
    //{
    //    public int ProductId { get; set; }
    //    public int Quantity { get; set; }
    //}

    //public class PaymentDto
    //{
    //    public int Id { get; set; }
    //    public string PaymentMethod { get; set; }
    //    public DateTime PaymentDate { get; set; }
    //    public decimal Amount { get; set; }
    //    public string Status { get; set; }
    //}
}
