using DemoApp.Models.Order;
using DemoApp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoApp.Controllers.OrderController
{
    [ApiController]
    [ApiVersion("2.0")]
    public class OrderV2Controller : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly ILogger<OrderV2Controller> _logger;

        public OrderV2Controller(ILogger<OrderV2Controller> logger, OrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [Route("api/order/placeorder")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrder placeOrder)
        {
            try
            {
                _logger.LogInformation("PlaceOrder Dummy log");

                //If multiple validation then we can write a private method seperatly to return true/false
                if (placeOrder == null || placeOrder.OrderItems == null || placeOrder.OrderItems.Count == 0)
                {
                    _logger.LogInformation("PlaceOrder validation failed Dummy log");
                    return BadRequest("Invalid order data.");
                }

                var placedOrder = await _orderService.PlaceOrder(placeOrder);

                return Ok(placedOrder);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/order/getorders")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrders([FromHeader] int customerID)
        {
            try
            {
                _logger.LogInformation("GetOrders Dummy log");

                if (customerID <= 0)
                {
                    return BadRequest("Invalid customerID.");
                }

                var orders = await _orderService.GetOrders(customerID);

                return Ok(orders);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/order/getorderdetails")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrderDetails([FromHeader] int orderID)
        {
            try
            {
                if (orderID <= 0)
                {
                    _logger.LogInformation("Invalid OrderID");
                    return BadRequest("Invalid OrderID.");
                }

                var orderDetails = await _orderService.GetOrderDetails(orderID);

                return Ok(orderDetails);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/order/getorderdetailswithpolly")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrderDetailsWithPolly([FromHeader] int orderID)
        {
            var retryPolicy = Policy
                .Handle<Exception>().Or<AggregateException>()
                .WaitAndRetryAsync(3, retryAttempt =>
                 TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            try
            {
                if (orderID <= 0)
                {
                    _logger.LogInformation("Invalid OrderID");
                    return BadRequest("Invalid OrderID.");
                }

                OrderDetails orderDetails = null;

                await retryPolicy.ExecuteAsync(async () =>
                {
                    orderDetails = await _orderService.GetOrderDetailsWithPolly(orderID);
                });

                return Ok(orderDetails);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
