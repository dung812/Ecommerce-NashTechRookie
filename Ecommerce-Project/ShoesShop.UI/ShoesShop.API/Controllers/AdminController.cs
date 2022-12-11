using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.DTO.Admin;
using ShoesShop.Service;

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
            var admins = adminService.GetAllAdminPaging(filterByRole, filterByDate, fieldName, searchString, sortType, page, limit);
            return Ok(admins);
        }

        // GET: api/Admin/1
        [HttpGet("{id}")]
        public IActionResult GetAdmin(int id)
        {
            AdminViewModel admin = adminService.GetAdminById(id);
            return Ok(admin);
        }

        // POST: api/Admin
        [HttpPost]
        public IActionResult CreateAdmin(AdminViewModel adminViewModel)
        {
            var status = adminService.CreateAdmin(adminViewModel);
            return status ? Ok() : BadRequest();
        }

        // PUT: api/Admin/1
        [HttpPut("{id}")]
        public IActionResult UpdateAdmin(int id, AdminViewModel adminViewModel)
        {
            var status = adminService.UpdateAdmin(id, adminViewModel);
            return status ? Ok() : BadRequest();
        }

        // DELETE: api/Admin/1
        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            var status = adminService.DeleteAdmin(id);
            return status ? Ok() : BadRequest();
        }
    }
}
