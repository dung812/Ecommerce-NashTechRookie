using Microsoft.AspNetCore.Mvc;
using ShoesShop.UI.Models;
using System.Diagnostics;
using ShoesShop.DTO;
using ShoesShop.Service;

namespace ShoesShop.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;
        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index()
        {
            //List<ProductViewModel> products = new List<ProductViewModel>();
            //products = productService.GetAllProduct();

            //ProductViewModel getProductDetail = new ProductViewModel();
            //getProductDetail = productService.GetSingleProduct(1);

            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        //[HttpPost]      
        
        //public IActionResult Contact(Contact)
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}