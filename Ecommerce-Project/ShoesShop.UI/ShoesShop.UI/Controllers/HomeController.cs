using Microsoft.AspNetCore.Mvc;
using ShoesShop.UI.Models;
using System.Diagnostics;
using ShoesShop.DTO;
using ShoesShop.Service;
using System.Collections.Generic;

namespace ShoesShop.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;
        private readonly ICommentProductService commentProductService;
        public HomeController(IProductService productService, ICommentProductService commentProductService)
        {
            this.productService = productService;
            this.commentProductService = commentProductService;
        }

        public List<ProductViewModel> HandleAvgRatingProduct(List<ProductViewModel> list)
        {
            for (var i = 0; i < list.Count(); i++)
            {
                var productId = list[i].ProductId;
                var getComments = commentProductService.GetListCommentOfProductById(productId);
                if (getComments.Count() == 0)
                {
                    list[i].AvgStar = 0;
                }
                else
                {
                    list[i].AvgStar = Functions.AverageRatingCalculator(getComments);
                }
                list[i].TotalComment = getComments.Count();
            }
            return list;
        }

        public IActionResult Index()
        {
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();

            productViewModels = productService.GetAllProduct();
            productViewModels = HandleAvgRatingProduct(productViewModels);

            // Get Featured Product
            ViewBag.FeaturedProduct = productViewModels.Take(8);

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