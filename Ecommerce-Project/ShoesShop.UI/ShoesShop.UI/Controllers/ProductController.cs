using Microsoft.AspNetCore.Mvc;

namespace ShoesShop.UI.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public IActionResult ProductList(string cateGender)
        {
            return View();
        }
        [HttpGet]
        public IActionResult ProductDetail(int? productId)
        {
            return View();
        }
    }
}
