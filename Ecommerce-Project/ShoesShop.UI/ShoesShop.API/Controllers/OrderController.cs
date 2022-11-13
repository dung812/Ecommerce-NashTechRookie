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
        //[AllowAnonymous]
        [HttpGet("{status}")]
        public ActionResult<IEnumerable<OrderViewModel>> GetOrders(OrderStatus status)
        {
            List<OrderViewModel> orders = orderService.GetOrderListByStatus(status);
            return orders;
        }        
        
        [HttpGet("GetProductListOfOrder/{orderId}")]
        public ActionResult<IEnumerable<OrderDetailViewModel>> GetProductListOfOrder(string orderId)
        {
            List<OrderDetailViewModel> products = orderService.GetItemOfOderById(orderId);
            return products;
        }

        [HttpGet("CheckedOrder/{orderId}")]
        public ActionResult CheckedOrder(string orderId)
        {
            var status = orderService.CheckedOrder(orderId);
            return status ? Ok() : BadRequest();
        }       
        
        [HttpGet("SuccessDeliveryOrder/{orderId}")]
        public ActionResult SuccessDeliveryOrder(string orderId)
        {
            var status = orderService.SuccessDeliveryOrder(orderId);
            return status ? Ok() : BadRequest();

        }

        [HttpGet("CancellationOrder/{orderId}")]
        public ActionResult CancellationOrder(string orderId)
        {
            var status = orderService.CancellationOrder(orderId);
            return status ? Ok() : BadRequest();
        }

        [HttpDelete("{orderId}")]
        public ActionResult DeleteOrder(string orderId)
        {
            var status = orderService.DeleteOrderByAdmin(orderId);
            return status ? Ok() : BadRequest();
        }


        [HttpGet("GetRecentOrders")]
        public ActionResult<IEnumerable<OrderViewModel>> GetRecentOrders()
        {
            List<OrderViewModel> orders = orderService.GetRecentOrders();
            return orders;
        }        

        [HttpGet("GetStatisticOrder")]
        public ActionResult<IEnumerable<OrderStatisticViewModel>> GetStatisticOrder()
        {
            List<OrderStatisticViewModel> list = orderService.GetStatisticOrder();
            return list;
        }
    }
}
