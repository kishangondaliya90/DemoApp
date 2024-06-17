using DemoApp.Models.Order;
using DemoApp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoApp.Controllers.OrderController
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    public class OrderV1Controller : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly ILogger<OrderV1Controller> _logger;

        public OrderV1Controller(ILogger<OrderV1Controller> logger, OrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [Route("api/order/placeorder")]
        [HttpPost]
        [Authorize]
        public IActionResult PlaceOrder([FromBody] PlaceOrder placeOrder)
        {
            _logger.LogInformation("PlaceOrder Dummy log");

            //If multiple validation then we can write a private method seperatly to return true/false
            if (placeOrder == null || placeOrder.OrderItems == null || placeOrder.OrderItems.Count == 0)
            {
                _logger.LogInformation("PlaceOrder validation failed Dummy log");
                return BadRequest("Invalid order data.");
            }

            var placedOrder = _orderService.PlaceOrder(placeOrder);

            return Ok(placedOrder);
        }

        [Route("api/order/getorders")]
        [HttpGet]
        [Authorize]
        public IActionResult GetOrders([FromHeader] int customerID)
        {
            _logger.LogInformation("GetOrders Dummy log");

            if (customerID <= 0)
            {
                return BadRequest("Invalid customerID.");
            }

            var orders = _orderService.GetOrders(customerID);
            return Ok(orders);
        }
    }
}
