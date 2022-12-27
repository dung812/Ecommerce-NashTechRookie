using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.DTO.Admin;
using ShoesShop.Service;
using ShoesShop.UI.Models;
using System.Security.Claims;

namespace ShoesShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        // GET: api/Admin
        [HttpGet]
        public IActionResult GetAdmins()
        {
            var admins = adminService.GetAllAdmin();
            return Ok(admins);
        }

        [HttpGet("[action]")]
        public ActionResult<AdminPagingViewModel> GetAdminsPaging(string? filterByRole, DateTime? filterByDate, string? searchString, string? fieldName, string? sortType, int page, int limit)
        {
            var admins = adminService.GetAllAdminPaging(filterByRole, filterByDate, searchString, fieldName, sortType, page, limit);
            return Ok(admins);
        }

        // GET: api/Admin/1
        [HttpGet("{id}")]
        public IActionResult GetAdmin(int id)
        {
            AdminViewModel admin = adminService.GetAdminById(id);
            if (admin != null)
                return Ok(admin);
            else
                return BadRequest();
        }

        // POST: api/Admin
        [HttpPost]
        public IActionResult CreateAdmin(AdminViewModel adminViewModel)
        {
            adminViewModel.Password = Functions.MD5Hash(adminViewModel.Password);
            var admin = adminService.CreateAdmin(adminViewModel);
            if (admin.IsExistedUsername == true)
            {
                return BadRequest("Existed Username");
            }
            var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);
            adminService.SaveActivity(adminId, "Create", "Admin", adminViewModel.UserName);
            return Ok(admin);
        }

        // PUT: api/Admin/1
        [HttpPut("{id}")]
        public ActionResult<AdminViewModel> UpdateAdmin(int id, AdminViewModel adminViewModel)
        {
            var admin = adminService.UpdateAdmin(id, adminViewModel);
            if (admin != null)
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);
                adminService.SaveActivity(adminId, "Update", "Admin", adminService.GetAdminById(id).UserName);
                return Ok(admin);
            }
            else
                return BadRequest();
        }

        // DELETE: api/Admin/1
        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            var admin = adminService.DisabledAdmin(id);
            if (admin != null)
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);

                adminService.SaveActivity(adminId, "Soft Delete", "Admin", adminService.GetAdminById(id).UserName);
                return NoContent();
            }    
            else
                return BadRequest();
        }

        [HttpGet("[action]")]
        public IActionResult GetActivitiesOfAdmin(int? adminId, string? objectType, DateTime? time)
        {
            var activities = adminService.GetActivitiesOfAdmin(adminId, objectType, time);
            return Ok(activities);
        }
    }
}
