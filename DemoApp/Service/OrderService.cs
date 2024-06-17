using DemoApp.Models.Order;
using DemoApp.Models.Product;
using DemoApp.Models.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoApp.Service
{
    public class OrderService
    {
        //Get list of order
        public async Task<List<Order>> GetOrders(int customerID)
        {
            return await MockGetOrderData(customerID);
        }

        //Get Order details
        public async Task<OrderDetails> GetOrderDetails(int orderID)
        {
            return await MockGetOrderDetailsData(orderID);
        }

        //Get Order details - simulate polly retry
        public async Task<OrderDetails> GetOrderDetailsWithPolly(int orderID)
        {
            throw new NotSupportedException("Polly exception occured");
        }

        //Place new order
        public async Task<PlaceOrder> PlaceOrder(PlaceOrder placeOrder)
        {
            // Calculate total price
            decimal totalPrice = 0;
            foreach (var item in placeOrder.OrderItems)
            {
                totalPrice += item.Product.Price * item.Quantity;
            }
            placeOrder.TotalPrice = totalPrice;
            placeOrder.OrderDate = DateTime.UtcNow;

            // Save order to the database (mocked)
            // ...

            return placeOrder;
        }

        private async Task<List<Order>> MockGetOrderData(int customerID)
        {
            if (customerID <= 0)
            {
                return null;
            }

            return new List<Order>
            {
                new Order {
                    Id = 101,
                    CustomerId = 1,
                    TotalPrice = 100.00m,
                    OrderStatus = "Disptached",
                    Customer = new User { //customer model data returend from the get customer api of the customer service. 
                        Id = 1,
                        Name = "Krishna Patel",
                        Address = "Dummary Address",
                        Email = "krishna.patel@abc.com"
                    },
                    OrderDate = DateTime.UtcNow,
                    OrderItems = new List<OrderItem> {
                        new OrderItem {
                            ProductId = 10,
                            Quantity = 1,
                            Product = new Product { //product model data returend from the get product api of the product service.
                                Id = 10,
                                Name = "Prodcut 1",
                                Price = 100.00m
                            }
                        }
                    },
                    Action = new Models.Action{ //This action model provide clickable action event to the enduser. 
                        Method = "HttpGet",
                        Name = "CancelOrder",
                        URL = "https://localhost:44364/api/cancelorder/101"
                    }
                },
                 new Order {
                    Id = 102,
                    CustomerId = 1,
                    TotalPrice = 200.00m,
                    OrderStatus = "Delivered",
                    Customer = new User { //customer model data returend from the get customer api of the customer service. 
                        Id = 1,
                        Name = "Krishna Patel",
                        Address = "Test Address",
                        Email = "krishna.patel@test.com"
                    },
                    OrderDate = DateTime.UtcNow.AddHours(-1),
                    OrderItems = new List<OrderItem> {
                        new OrderItem {
                            ProductId = 11,
                            Quantity = 2,
                            Product = new Product { //product model data returend from the get product api of the product service.
                                Id = 11,
                                Name = "Prodcut 1",
                                Price = 100.00m
                            }
                        }
                    }
                },
            };
        }

        private async Task<OrderDetails> MockGetOrderDetailsData(int orderID)
        {
            return new OrderDetails
            {
                OrderId = orderID,
                OrderStatus = "Disptached",
                OrderDate = DateTime.UtcNow.AddHours(-1),
                TotalAmount = 100.00m,
                BillingAddress = "Test billing Address",
                DeliveryAddress = "Test Delivery Address",
                OrderInvoice = "Order Invoice Link - Azure fileshare",
                ProductID = 10,
                Product = new Product // get this data from Product Service API
                {
                    Id = 10,
                    Name = "Product 1",
                    Description = "Product 1 Desc",
                    Image = "Product image link - Azure blob storage",
                    Price = 100.00m
                },
                Payment = new Models.Payment.Payment // get this data from Payment Service API
                {
                    PaymentID = 1021452,
                    OrderID = orderID,
                    PaymentMethod = "Credit Card",
                    PaymentStatusID = 2,
                    PaymentStatus = "Success",
                    TotalPayment = 100.00m
                }
            };
        }
    }
}








