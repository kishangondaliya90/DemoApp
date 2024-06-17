using System;
using System.Collections.Generic;

namespace DemoApp.Models.Order
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public User.User Customer { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public Action Action { get; set; }
    }
}
