using Microsoft.AspNetCore.Http;
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

        //[AllowAnonymous]
        [HttpGet("GetStatisticCardNumber")]
        public ActionResult<IEnumerable<StatisticNumberViewModel>> GetStatisticCardNumber()
        {
            List<StatisticNumberViewModel> list = statisticService.GetStatisticNumber();
            return list;
        }
    }
}
