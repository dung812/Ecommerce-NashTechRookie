using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ShoesShop.Domain;
using ShoesShop.DTO;
using ShoesShop.Service;
using ShoesShop.UI.Models;

namespace ShoesShop.UI.Controllers
{
    public class AccountController : Controller
    {
        private IWebHostEnvironment hostEnvironment;
        private readonly ICustomerService customerService;
        public AccountController(IWebHostEnvironment environment, ICustomerService customerService)
        {
            this.hostEnvironment = environment;
            this.customerService = customerService;
        }

        [HttpGet("Login-registration")]
        public IActionResult LoginRegistration()
        {
            var customer = HttpContext.Session.GetString("CustomerInfo");
            if (customer != null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Get customer info data of session
            //var str = HttpContext.Session.GetString("CustomerInfo");
            //var obj = JsonConvert.DeserializeObject<Customer>(str);

            return View();
        }        
        [HttpPost]
        public IActionResult Login(IFormCollection formFields)
        {
            var email = formFields["email"][0];
            var password = formFields["password"][0];

            password = Functions.MD5Hash(password.Trim());

            //var getValidCustomerAccount = customerService.GetValidCustomerByEmail(email);
            Customer checkAccount = customerService.ValidateCustomerAccount(email, password);

            if(checkAccount != null)
            {
                if (checkAccount.Status == false)
                {
                    // Case customer login account was locked;
                    TempData["loginFail"] = "Your account has been locked.";
                    return View();
                }
                else
                {
                    // Case valid account
                    //HttpContext.Session.Remove("CountNumberError"); // Xóa biến đếm số lần sai

                    var parseCustomerInfo = JsonConvert.SerializeObject(checkAccount);
                    HttpContext.Session.SetString("CustomerInfo", parseCustomerInfo);


                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["loginFail"] = "Incorrect username or password.";
            return View();
        }

        [HttpPost]
        public JsonResult Registration(string firstName, string lastName, string email)
        {
            // Check exist email
            var checkExistEmail = customerService.CheckExistEmailOfCustomer(email);

            if (checkExistEmail)
            {
                var password = Functions.RandomString();
                // Add new customer
                CustomerViewModel customerViewModel = new CustomerViewModel();
                customerViewModel.FirstName = firstName.Trim();
                customerViewModel.LastName = lastName.Trim();  
                customerViewModel.Email = email.Trim();
                customerViewModel.Password = Functions.MD5Hash(password);

                if (customerService.CreateCustomer(customerViewModel))
                {
                    // Send email info account
                    string url = String.Concat(this.Request.Scheme, "://", this.Request.Host);

                    string content = System.IO.File.ReadAllText(Path.Combine(hostEnvironment.WebRootPath, "template\\InfoAccount.html"));
                    content = content.Replace("{{url}}", url);
                    content = content.Replace("{{name}}", firstName + " " + lastName);
                    content = content.Replace("{{email}}", email);
                    content = content.Replace("{{password}}", password);

                    Functions.SendMail(email, "[Shoes shop] Your Login Information", content);

                    return Json(new { status = true, email = email, msg = "Successs" });
                }
            }
            return Json(new { status = false, email = email, msg = "Your email existed!" });
        }
     
        
        [HttpGet("Forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        } 
        
        [HttpPost]
        public JsonResult ForgotPassword(string email)
        {
            var customer = customerService.GetValidCustomerByEmail(email);

            if (customer != null)
            {
                // Generate UUID
                Guid myuuid = Guid.NewGuid();
                string myuuidAsString = myuuid.ToString();

                // Create URL UUID
                string url = String.Concat(this.Request.Scheme, "://", this.Request.Host, "/reset-password/", myuuidAsString);

                return Json(new { status = 200, smg = "Successfully send email!" });

            }
            return Json(new { status = 500, smg = "That address is either invalid, not a verified primary email or is not associated with a personal user account." });
        }

        [HttpGet("Reset-password/{token}")]
        public IActionResult ResetPassword(string token)
        {
            return View();
        } 
        
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult OrderDetail()
        {
            return View();
        }  
        
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }   
        
        [HttpGet]
        public IActionResult AddressList()
        {
            return View();
        }    
        

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("CustomerInfo");
            return RedirectToAction("Index", "Home");
        }        

    }
}
