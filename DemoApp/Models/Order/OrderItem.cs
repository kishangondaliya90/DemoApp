using System;

namespace DemoApp.Models.Order
{
    public class OrderItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product.Product Product { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }

    }
}
