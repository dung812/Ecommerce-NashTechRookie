﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.DTO;
using ShoesShop.Service;

namespace ShoesShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        // GET: api/Customer
        [HttpGet]
        public IActionResult GetCustomers()
        {
            List<CustomerViewModel> customers = customerService.GetAllCustomer();
            return Ok(customers);
        }
    }
}
