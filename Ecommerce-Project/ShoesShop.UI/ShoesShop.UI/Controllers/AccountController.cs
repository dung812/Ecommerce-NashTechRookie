using Microsoft.AspNetCore.Hosting;
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
        private readonly ICustomerAddressService customerAddressService;
        private readonly IOrderService orderService;

        public AccountController(IWebHostEnvironment environment, ICustomerService customerService, ICustomerAddressService customerAddressService, IOrderService orderService)
        {
            this.hostEnvironment = environment;
            this.customerService = customerService;
            this.customerAddressService = customerAddressService;
            this.orderService = orderService;
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
            var password = Functions.MD5Hash(formFields["password"][0].Trim());


            //var getValidCustomerAccount = customerService.GetValidCustomerByEmail(email);
            Customer checkAccount = customerService.ValidateCustomerAccount(email, password);

            if(checkAccount != null)
            {
                if (checkAccount.IsLockedFirstLogin == true)
                {
                    TempData["error"] = "Your account was locked because you didn't change password in the first login. Use forgot password to unlock!";
                    return RedirectToAction("LoginRegistration");
                }                
                if (checkAccount.Status == false)
                {
                    TempData["error"] = "Your account was locked!";
                    return RedirectToAction("LoginRegistration");
                }
                var parseCustomerInfo = JsonConvert.SerializeObject(checkAccount);
                HttpContext.Session.SetString("CustomerInfo", parseCustomerInfo);
                return RedirectToAction("Index", "Home");
            }
            TempData["error"] = "Incorrect username or password.";
            return RedirectToAction("LoginRegistration");
        }

        [HttpPost]
        public JsonResult Registration(string firstName, string lastName, string email)
        {
            // Check exist email
            var checkExistEmail = customerService.CheckExistEmailOfCustomer(email);

            if (!checkExistEmail)
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
                if (customerService.CountTokenInCurrentDayOfCustomer(customer.CustomerId) < 3)
                {

                    // Generate token with UUID and add in URL reset password
                    Guid myuuid = Guid.NewGuid();
                    string token = myuuid.ToString();

                    string url = String.Concat(this.Request.Scheme, "://", this.Request.Host, "/Reset-password/", token);

                    // Create new token forgot password in database
                    customerService.CreateTokenForgotPassword(customer.CustomerId, email, token);

                    // Send  URl to email customer
                    string content = System.IO.File.ReadAllText(Path.Combine(hostEnvironment.WebRootPath, "template\\ResetPassword.html"));
                    content = content.Replace("{{url}}", url);

                    Functions.SendMail(email, "[Shoes shop] Reset your password", content);

                    return Json(new { status = 200, smg = "Successfully send email!" });
                } 

                else
                {
                    return Json(new { status = 500, smg = "You cannot be used forgot password feature more than 3 times in day!" });
                }
            }
            return Json(new { status = 500, smg = "That address is either invalid, not a verified primary email or is not associated with a personal user account." });
        }

        [HttpGet("Reset-password/{token}")]
        public IActionResult ResetPassword(string token)
        {
            var checkToken = customerService.TokenValidate(token);
            if (token == null || checkToken.Token == null)
            {
                return RedirectToAction("Error", "Home");
            }

            else
            {
                Customer customer = customerService.GetValidCustomerByEmail(checkToken.Email);
                ViewBag.token = checkToken.Token;
                ViewBag.Email = customer.Email;
                ViewBag.customerId = customer.CustomerId;
                return View();
            } 
        }
        [HttpPost]
        public IActionResult ResetPassword(IFormCollection form)
        {
            var newPassword = Functions.MD5Hash(form["password"][0].Trim());
            var customerId = Convert.ToInt32(form["customerId"][0]);
            var token = form["token"][0];

            var checkToken = customerService.TokenValidate(token);

            if (checkToken != null)
            {
                customerService.ResetPassword(customerId, newPassword);

                // Set token false to unactive token
                customerService.ActiveToken(token);

                TempData["success"] = "Successfully reset your password.";
                return RedirectToAction("LoginRegistration");
            }
            return View();
        }


        [HttpGet("Profile/{customerId}")]
        public IActionResult Profile(int customerId)
        {
            // Get customer info data of session
            var cusomterSession = HttpContext.Session.GetString("CustomerInfo");
            var customerInfoSession = JsonConvert.DeserializeObject<Customer>(cusomterSession != null ? cusomterSession : "");

            if (cusomterSession == null)
            {
                TempData["error"] = "Authentication session has expired!";
                return RedirectToAction("Index", "Home");
            }
            if (customerInfoSession?.CustomerId != customerId)
            {
                return RedirectToAction("Error", "Home");
            }

            var customer = customerService.GetValidCustomerById(customerId);
            var defaultAddress = customerAddressService.GetDefaultAddressOfCustomer(customerId);

            if (defaultAddress != null)
            {
                CustomerAddressViewModel customerAddressViewModel = new CustomerAddressViewModel();
                customerAddressViewModel.FirstName = defaultAddress.FirstName;
                customerAddressViewModel.LastName = defaultAddress.LastName;
                customerAddressViewModel.Address = defaultAddress.Address;
                customerAddressViewModel.Phone = defaultAddress.Phone;
                ViewBag.DefaultAddress = customerAddressService.GetDefaultAddressOfCustomer(customerId);
            }

            ViewBag.CustomerId = customerId;
            ViewBag.CustomerName = customer.FirstName + " " + customer.LastName;
            ViewBag.Avatar = customer.Avatar;
            ViewBag.CountAddress = customerAddressService.CountAddressOfCustomer(customerId);

            ViewBag.OrderHistorys = orderService.GetListOrderByCustomerId(customerId);

            return View();
        }
        
        [HttpGet("Order-detail/{orderId}-{customerId}")]
        public IActionResult OrderDetail(int customerId, string orderId)
        {
            // Get customer info data of session
            var cusomterSession = HttpContext.Session.GetString("CustomerInfo");
            var customerInfoSession = JsonConvert.DeserializeObject<Customer>(cusomterSession != null ? cusomterSession : "");

            var orderItems = orderService.GetItemOfOderById(orderId);
            var orderDetail = orderService.GetOrderDetailById(orderId);

            if (customerInfoSession?.CustomerId != customerId || orderDetail == null)
            {
                return RedirectToAction("Error", "Home");
            }

            // Handle display price of order
            var totalPrice = 0;
            var totalDiscount = 0;
            foreach (var i in orderItems)
            {
                var currentPrice = Functions.DiscountedPriceCalulator(i.UnitPrice, i.PromotionPercent); // tính tiền giảm %
                var totalDiscountOfProduct = (i.UnitPrice - currentPrice) * i.Quantity;
                var totalPriceOfProduct = (i.UnitPrice * i.Quantity) - totalDiscountOfProduct;

                totalPrice += totalPriceOfProduct;
                totalDiscount += totalDiscountOfProduct;
            }

            ViewBag.OrderInfo = orderDetail; // Information of order
            ViewBag.TotalPrice = totalPrice; // Total money of order
            ViewBag.TotalDiscount = totalDiscount; // Total discounted money of order

            return View(orderItems);
        }  
        
        public IActionResult DeleteOrder(string orderId, int customerId)
        {
            if (orderService.DeleteOrderByCustomer(customerId, orderId))
            {
                TempData["success"] = "Successfully delete your order!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "Error delete your order!";
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult ChangeAvatar(IFormFile objFile)
        {
            try
            {
                // Get customer info data of session
                var cusomterSession = HttpContext.Session.GetString("CustomerInfo");
                var customerInfoSession = JsonConvert.DeserializeObject<Customer>(cusomterSession != null ? cusomterSession : "");

                if (cusomterSession == null)
                {
                    TempData["error"] = "Authentication session has expired!";
                    return RedirectToAction("Index", "Home");
                }

                if (customerInfoSession != null)
                {
                    var customerAfterUpdate = customerService.ChangeAvatarOfCustomer(customerInfoSession.CustomerId, objFile.FileName);

                    if (objFile.Length > 0 && customerAfterUpdate != null)
                    {
                        if (!Directory.Exists(hostEnvironment.WebRootPath + "\\images\\avatars\\"))
                        {
                            Directory.CreateDirectory(hostEnvironment.WebRootPath + "\\images\\avatars\\");
                        }
                        using (FileStream fileStream = System.IO.File.Create(hostEnvironment.WebRootPath + "\\images\\avatars\\" + objFile.FileName))
                        {
                            objFile.CopyTo(fileStream);
                            fileStream.Flush();

                            // Update customer info session
                            var parseCustomerInfo = JsonConvert.SerializeObject(customerAfterUpdate);
                            HttpContext.Session.SetString("CustomerInfo", parseCustomerInfo);

                            TempData["success"] = "Successfully change your avatar!";
                            return RedirectToAction("Index", "Home");
                        };
                    }
                }
                TempData["error"] = "Error change your avatar!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData["error"] = "Error change your avatar!";
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpGet("Change-password/{customerId}")]
        public IActionResult ChangePassword(int customerId)
        {
            // Get customer info data of session
            var cusomterSession = HttpContext.Session.GetString("CustomerInfo");
            var customerInfoSession = JsonConvert.DeserializeObject<Customer>(cusomterSession != null ? cusomterSession : "");

            if (cusomterSession == null)
            {
                TempData["error"] = "Authentication session has expired!";
                return RedirectToAction("Index", "Home");
            }
            if (customerInfoSession?.CustomerId != customerId)
            {
                return RedirectToAction("Error", "Home");
            }

            var customer = customerService.GetValidCustomerById(customerId);

            ViewBag.CustomerId = customer.CustomerId;

            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(IFormCollection form)
        {
            // Get customer info data of session
            var cusomterSession = HttpContext.Session.GetString("CustomerInfo");

            if (cusomterSession == null)
            {
                TempData["error"] = "Authentication session has expired!";
                return RedirectToAction("Index", "Home");
            }

            var customerId = Convert.ToInt32(form["customerId"][0]);
            var currentPassword = Functions.MD5Hash(form["current-password"][0]);
            var newPassword = Functions.MD5Hash(form["password"][0]);

           
            var customer = customerService.GetValidCustomerById(customerId);

            if (customer.Password == currentPassword)
            {
                customerService.ChangePassword(customerId, newPassword);

                TempData["success"] = "Successfully change your password.";
                return RedirectToAction("Index", "Home");
            }

            ViewBag.CustomerId = customer.CustomerId;
            ViewBag.IncorrectPassword = "Your current password incorrect";
            return View();
        }


        [HttpGet("Address/{customerId}")]
        public IActionResult AddressList(int customerId)
        {

            // Get customer info data of session
            var cusomterSession = HttpContext.Session.GetString("CustomerInfo");
            var customerInfoSession = JsonConvert.DeserializeObject<Customer>(cusomterSession != null ? cusomterSession : "");

            if (cusomterSession == null)
            {
                TempData["error"] = "Authentication session has expired!";
                return RedirectToAction("Index", "Home");
            }

            if (customerInfoSession?.CustomerId != customerId)
            {
                return RedirectToAction("Error", "Home");
            }

            var customer = customerService.GetValidCustomerById(customerId);

            List<CustomerAddressViewModel> addresses = new List<CustomerAddressViewModel>();
            addresses = customerAddressService.GetAddressListOfCustomerById(customer.CustomerId);

            ViewBag.CustomerId = customer.CustomerId;

            if (addresses != null)
            {
                ViewBag.IsOnlyOneDefault = addresses.Count() == 1 ? true : false;
            }    
            return View(addresses);
        }
        [HttpPost]
        public IActionResult CreateAddress(IFormCollection form)
        {
            // Get customer info data of session
            var cusomterSession = HttpContext.Session.GetString("CustomerInfo");

            if (cusomterSession == null)
            {
                TempData["error"] = "Authentication session has expired!";
                return RedirectToAction("Index", "Home");
            }


            int customerId = Int32.Parse(form["id"][0]);
            string firstName = form["firstName"][0];
            string lastName = form["lastName"][0];
            string address = form["address"][0];
            string phone = form["phone"][0];
            bool isDefault = form["default"].Count() == 0 ? false : true;

            CustomerAddressViewModel customer = new CustomerAddressViewModel();
            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.Address = address;
            customer.Phone = phone;
            customer.CustomerId = customerId;

            if (customerAddressService.CreateAddressOfCustomer(customer, isDefault))
            {
                TempData["success"] = "Successfully add a new address!";
                return Redirect("~/Address/" + customerId);
            }
            else
            {
                TempData["error"] = "Something were wrong!";
                return Redirect("~/Address/" + customerId);
            }
        }

        [HttpPost]
        public IActionResult UpdateAddress(IFormCollection form)
        {
            // Get customer info data of session
            var cusomterSession = HttpContext.Session.GetString("CustomerInfo");

            if (cusomterSession == null)
            {
                TempData["error"] = "Authentication session has expired!";
                return RedirectToAction("Index", "Home");
            }

            var customerId = Int32.Parse(form["customerId"][0]);
            var customerAddressId = Int32.Parse(form["customerAddressId"][0]);
            string firstName = form["firstName"][0];
            string lastName = form["lastName"][0];
            string address = form["address"][0];
            string phone = form["phone"][0];
            bool isDefault = form["default"].Count() == 0 ? false : true;


            CustomerAddressViewModel customer = new CustomerAddressViewModel();
            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.Address = address;
            customer.Phone = phone;
            customer.CustomerId = customerId;

            if (customerAddressService.UpdateAddressOfCustomer(customerAddressId, customer, isDefault))
            {
                TempData["success"] = "Successfully update your address!";
                return Redirect("~/Address/" + customerId);
            }
            else
            {
                TempData["error"] = "Something were wrong!";
                return Redirect("~/Address/" + customerId);
            }
        }

        //[HttpPost]
        public IActionResult DeleteAddress(int addressId, int customerId)
        {
            // Get customer info data of session
            var cusomterSession = HttpContext.Session.GetString("CustomerInfo");

            if (cusomterSession == null)
            {
                TempData["error"] = "Authentication session has expired!";
                return RedirectToAction("Index", "Home");
            }

            if (customerAddressService.DeleteAddressOfCustomer(addressId, customerId))
            {
                TempData["success"] = "Successfully delete your address!";
                return Redirect("~/Address/" + customerId);
            }
            else
            {
                TempData["error"] = "Something were wrong!";
                return Redirect("~/Address/" + customerId);
            }
        }

        public IActionResult Logout()
        {
            // Clear session
            HttpContext.Session.Remove("CustomerInfo");
            return RedirectToAction("Index", "Home");
        }        

    }
}
