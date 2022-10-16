using Microsoft.AspNetCore.Mvc;
using ShoesShop.UI.Models;
using System.Diagnostics;
using ShoesShop.DTO;
using ShoesShop.Service;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ShoesShop.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;
        private readonly ICommentProductService commentProductService;
        private readonly IContactService contactService;
        public HomeController(IProductService productService, ICommentProductService commentProductService, IContactService contactService)
        {
            this.productService = productService;
            this.commentProductService = commentProductService;
            this.contactService = contactService;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                contactService.Create(contactViewModel);
                TempData["success"] = "Your message has been received by us. We will contact you as soon as possible.";

                return RedirectToAction("Index");
            }
            else 
            {
                TempData["error"] = "Error";
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}