using Microsoft.AspNetCore.Mvc;
using ShoesShop.DTO;
using ShoesShop.Domain.Enum;
using ShoesShop.Service;
using ShoesShop.UI.Models;
using System.Net.NetworkInformation;
using X.PagedList.Mvc.Core;
using X.PagedList;
using ShoesShop.Data;

namespace ShoesShop.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICommentProductService commentProductService;
        private readonly IManufactureService manufactureService;
        private readonly ICatalogService catalogService;
        public ProductController(IProductService productService, IManufactureService manufactureService, ICatalogService catalogService, ICommentProductService commentProductService)
        {
            this.productService = productService;
            this.manufactureService = manufactureService;
            this.catalogService = catalogService;
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

        [HttpGet]
        public IActionResult ProductList(int? page, string cateGender, int? manufactureId, int? catalogId)
        {
            ViewBag.Catalogs = catalogService.GetAllCatalog();
            ViewBag.Manufatures = manufactureService.GetAllManufacture();
            ViewBag.CateGender = cateGender;
            ViewBag.ManufactureId = manufactureId;
            ViewBag.CatalogId = catalogId;

            var pageNumber = page ?? 1;
            var pageSize = 5; //Show 10 rows every time

            Gender gender = Gender.Women;
            if (cateGender == "Men")
            {
                gender = Gender.Men;
            }           

            IPagedList<ProductViewModel> products = productService.GetAllProductPage(gender, manufactureId, catalogId, pageNumber, pageSize);

            return View(products);
        }


        [HttpGet]
        public IActionResult ProductDetail(int productId)
        {
            if (productId == null)
            {
                return RedirectToAction("Error", "Home");
            }
            ProductViewModel product = productService.GetProductById(productId);
            return View(product);
        }
    }
}
