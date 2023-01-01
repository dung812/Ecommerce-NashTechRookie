using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.DTO;
using ShoesShop.Service;

namespace ShoesShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IAdminService adminService;
        private readonly ICustomerService customerService;
        public CustomerController(ICustomerService customerService, IAdminService adminService)
        {
            this.customerService = customerService;
            this.adminService = adminService;
        }

        // GET: api/Customer
        [HttpGet]
        public IActionResult GetCustomers()
        {
            List<CustomerViewModel> customers = customerService.GetAllCustomer();
            return Ok(customers);
        }

        // DELETE: api/Admin/1
        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            var admin = customerService.DisabledCustomer(id);
            if (admin != null)
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);

                adminService.SaveActivity(adminId, "Soft Delete", "Customer", adminService.GetAdminById(id).UserName);
                return NoContent();
            }
            else
                return BadRequest();
        }
    }
}
