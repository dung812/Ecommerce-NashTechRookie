using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.DTO;
using ShoesShop.Service;
using ShoesShop.Domain.Enum;

namespace ShoesShop.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IAdminService adminService;
        public OrderController(IOrderService orderService, IAdminService adminService)
        {
            this.orderService = orderService;
            this.adminService = adminService;
        }

        // GET: api/Order/1
        [HttpGet("{status}")]
        public IActionResult GetOrders(OrderStatus status)
        {
            List<OrderViewModel> orders = orderService.GetOrderListByStatus(status);
            return Ok(orders);
        }          
        
        [HttpGet("GetOrdersFilter")]
        public IActionResult GetOrdersFilter(OrderStatus status, DateTime? FromDate, DateTime? ToDate)
        {
            List<OrderViewModel> orders = orderService.GetOrderListByStatusFilter(status, FromDate, ToDate);
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

            if (status)
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);
                adminService.SaveActivity(adminId, "Checked order", "Order", orderId);
                return Ok();
            }
            else return BadRequest();
        }       
        
        [HttpGet("SuccessDeliveryOrder/{orderId}")]
        public IActionResult SuccessDeliveryOrder(string orderId)
        {
            var status = orderService.SuccessDeliveryOrder(orderId);

            if (status)
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);
                adminService.SaveActivity(adminId, "Success order", "Order", orderId);
                return Ok();
            }
            else return BadRequest();

        }

        [HttpGet("CancellationOrder/{orderId}")]
        public IActionResult CancellationOrder(string orderId)
        {
            var status = orderService.CancellationOrder(orderId);

            if (status)
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);
                adminService.SaveActivity(adminId, "Cancel order", "Order", orderId);
                return Ok();
            }
            else return BadRequest();
        }

        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrder(string orderId)
        {
            var status = orderService.DeleteOrderByAdmin(orderId);

            if (status)
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);
                adminService.SaveActivity(adminId, "Delete order", "Order", orderId);
                return Ok();
            }
            else return BadRequest();
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
