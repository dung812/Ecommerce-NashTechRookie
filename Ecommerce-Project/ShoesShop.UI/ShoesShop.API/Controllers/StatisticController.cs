using Microsoft.AspNetCore.Mvc;
using ShoesShop.Domain.Enum;
using ShoesShop.DTO;
using ShoesShop.Service;

namespace ShoesShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService statisticService;
        public StatisticController(IStatisticService statisticService)
        {
            this.statisticService = statisticService;
        }

        [HttpGet("GetStatisticCardNumber")]
        public IActionResult GetStatisticCardNumber()
        {
            var list = statisticService.GetStatisticNumber();
            return Ok(list);
        }
        [HttpGet("GetRecentOrder")]
        public IActionResult GetRecentOrder()
        {
            var list = statisticService.GetRecentOrders();
            return Ok(list);
        }
        [HttpGet("ReportCustomer")]
        public IActionResult ReportCustomer()
        {
            var list = statisticService.ReportCustomer();
            return Ok(list);
        }
    }
}
