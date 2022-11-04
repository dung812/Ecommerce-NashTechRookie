using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.DTO;
using ShoesShop.Service;

namespace ShoesShop.UI.Controllers.API
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
        public ActionResult<IEnumerable<AdminViewModel>> GetAdmins()
        {
            List<AdminViewModel> admins = adminService.GetAllAdmin();
            return admins;
        }

        // GET: api/Admin/1
        [HttpGet("{id}")]
        public ActionResult<AdminViewModel> GetAdmin(int id)
        {
            AdminViewModel admin = adminService.GetAdminById(id);
            return admin;
        }

        // POST: api/Admin
        [HttpPost]
        public ActionResult CreateAdmin(AdminViewModel adminViewModel)
        {
            var status = adminService.CreateAdmin(adminViewModel);
            return status ? Ok() : BadRequest();
        }

        // PUT: api/Admin/1
        [HttpPut("{id}")]
        public ActionResult UpdateAdmin(int id, AdminViewModel adminViewModel)
        {
            var status = adminService.UpdateAdmin(id, adminViewModel);
            return status ? Ok() : BadRequest();
        }

        // DELETE: api/Admin/1
        [HttpDelete("{id}")]
        public ActionResult DeleteAdmin(int id)
        {
            var status = adminService.DeleteAdmin(id);
            return status ? Ok() : BadRequest();
        }
    }
}
