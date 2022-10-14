using Microsoft.AspNetCore.Mvc;

namespace ShoesShop.UI.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult LoginRegistration()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }    
        
        [HttpGet]
        public IActionResult ResetPassword()
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
            return View();
        }        

    }
}
