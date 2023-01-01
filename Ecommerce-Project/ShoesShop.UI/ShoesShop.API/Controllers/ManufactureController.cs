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
        private readonly IAdminService adminService;
        public ManufactureController(IManufactureService manufactureService, IAdminService adminService)
        {
            this.manufactureService = manufactureService;
            this.adminService = adminService;
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

            if (status)
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);
                adminService.SaveActivity(adminId, "Create", "Manufacture", manufactureViewModel.Name);
                return Ok();
            }
            else return BadRequest();
        }

        // PUT: api/Manufacture/1
        [HttpPut("{id}")]
        public IActionResult UpdateManufacture(int id, ManufactureViewModel manufactureViewModel)
        {
            string oldManufactureName = manufactureService.GetManufactureById(id).Name;
            var status = manufactureService.UpdateManufacture(id, manufactureViewModel);

            if (status)
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);
                adminService.SaveActivity(adminId, "Update", "Manufacture", oldManufactureName);
                return Ok();
            }
            else return BadRequest();
        }

        // DELETE: api/Manufacture/1
        [HttpDelete("{id}")]
        public IActionResult DeleteManufacture(int id)
        {
            var status = manufactureService.DeleteManufacture(id);

            if (status)
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);
                adminService.SaveActivity(adminId, "Soft delete", "Manufacture", manufactureService.GetManufactureById(id).Name != null ? manufactureService.GetManufactureById(id).Name : "");
                return Ok();
            }
            else return BadRequest();
        }
    }
}
