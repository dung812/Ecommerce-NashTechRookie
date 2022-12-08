using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.DTO;
using ShoesShop.Service;
using ShoesShop.Domain.Enum;

namespace ShoesShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        // GET: api/Order/1
        [HttpGet("{status}")]
        public IActionResult GetOrders(OrderStatus status)
        {
            List<OrderViewModel> orders = orderService.GetOrderListByStatus(status);
            return Ok(orders);
        }        
        
        [HttpGet("GetProductListOfOrder/{orderId}")]
        public IActionResult GetProductListOfOrder(string orderId)
        {
            List<OrderDetailViewModel> products = orderService.GetItemOfOderById(orderId);
            return Ok(products);
        }

        [HttpGet("CheckedOrder/{orderId}")]
        public IActionResult CheckedOrder(string orderId)
        {
            var status = orderService.CheckedOrder(orderId);
            return status ? Ok() : BadRequest();
        }       
        
        [HttpGet("SuccessDeliveryOrder/{orderId}")]
        public IActionResult SuccessDeliveryOrder(string orderId)
        {
            var status = orderService.SuccessDeliveryOrder(orderId);
            return status ? Ok() : BadRequest();

        }

        [HttpGet("CancellationOrder/{orderId}")]
        public IActionResult CancellationOrder(string orderId)
        {
            var status = orderService.CancellationOrder(orderId);
            return status ? Ok() : BadRequest();
        }

        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrder(string orderId)
        {
            var status = orderService.DeleteOrderByAdmin(orderId);
            return status ? Ok() : BadRequest();
        }


        [HttpGet("GetRecentOrders")]
        public IActionResult GetRecentOrders()
        {
            List<OrderViewModel> orders = orderService.GetRecentOrders();
            return Ok(orders);
        }        

        [HttpGet("GetStatisticOrder")]
        public IActionResult GetStatisticOrder()
        {
            List<OrderStatisticViewModel> list = orderService.GetStatisticOrder();
            return Ok(list);
        }
    }
}
