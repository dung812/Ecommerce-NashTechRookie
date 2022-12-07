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
        public ActionResult<List<StatisticNumberViewModel>> GetStatisticCardNumber()
        {
            var list = statisticService.GetStatisticNumber();
            return list;
        }        
        [HttpGet("GetRecentOrder")]
        public ActionResult<List<OrderViewModel>> GetRecentOrder()
        {
            var list = statisticService.GetRecentOrders();
            return list;
        }
    }
}
