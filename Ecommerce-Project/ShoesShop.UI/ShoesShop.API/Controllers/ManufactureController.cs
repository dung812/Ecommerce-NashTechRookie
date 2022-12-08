using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.DTO;
using ShoesShop.Service;

namespace ShoesShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufactureController : ControllerBase
    {
        private readonly IManufactureService manufactureService;
        public ManufactureController(IManufactureService manufactureService)
        {
            this.manufactureService = manufactureService;
        }


        // GET: api/Manufacture
        [HttpGet]
        public IActionResult GetManufactures()
        {
            List<ManufactureViewModel> manufactures = manufactureService.GetAllManufacture();
            return Ok(manufactures);
        }

        // GET: api/Manufacture/1
        [HttpGet("{id}")]
        public IActionResult GetManufacture(int id)
        {
            ManufactureViewModel manufacture = manufactureService.GetManufactureById(id);
            return Ok(manufacture);
        }

        // POST: api/Manufacture
        [HttpPost]
        public IActionResult CreateManufacture(ManufactureViewModel manufactureViewModel)
        {
            var status = manufactureService.CreateManufacture(manufactureViewModel);
            return status ? Ok() : BadRequest();
        }

        // PUT: api/Manufacture/1
        [HttpPut("{id}")]
        public IActionResult UpdateManufacture(int id, ManufactureViewModel manufactureViewModel)
        {
            var status = manufactureService.UpdateManufacture(id, manufactureViewModel);
            return status ? Ok() : BadRequest();
        }

        // DELETE: api/Manufacture/1
        [HttpDelete("{id}")]
        public IActionResult DeleteManufacture(int id)
        {
            var status = manufactureService.DeleteManufacture(id);
            return status ? Ok() : BadRequest();
        }
    }
}
