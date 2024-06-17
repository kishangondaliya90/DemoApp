using System;
using System.Collections.Generic;

namespace DemoApp.Models.Order
{
    public class PlaceOrder
    {
        public int CustomerId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
