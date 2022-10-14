using Microsoft.AspNetCore.Mvc;

namespace ShoesShop.UI.Controllers
{
    public class CartController : Controller
    {
        public IActionResult CartPage()
        {
            return View();
        }        
        public IActionResult Checkout()
        {
            return View();
        }        
        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}
