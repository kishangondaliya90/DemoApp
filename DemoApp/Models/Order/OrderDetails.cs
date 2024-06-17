using System;

namespace DemoApp.Models.Order
{
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public int ProductID { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public Product.Product Product { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderInvoice { get; set; }
        public string BillingAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public Payment.Payment Payment { get; set; }
    }
}
