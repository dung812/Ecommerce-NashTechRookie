using Microsoft.AspNetCore.Mvc;
using ShoesShop.UI.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Data;
using ShoesShop.DTO;
using ShoesShop.Domain;

namespace ShoesShop.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Test get list product
            List<ProductViewModel> products = new List<ProductViewModel>();
            using (var context = new ApplicationDbContext())
            {
                products = context.Products
                    .TagWith("Test get list product fill data in viewmodel")
                    .Select(m => new ProductViewModel
                        {
                            ProductName = m.ProductName,
                            OriginalPrice = m.OriginalPrice
                        }).ToList();
            }
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}