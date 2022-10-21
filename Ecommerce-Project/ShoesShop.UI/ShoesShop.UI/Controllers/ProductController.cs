using Microsoft.AspNetCore.Mvc;
using ShoesShop.DTO;
using ShoesShop.Domain.Enum;
using ShoesShop.Service;
using ShoesShop.UI.Models;
using System.Net.NetworkInformation;
using X.PagedList.Mvc.Core;
using X.PagedList;
using ShoesShop.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoesShop.Domain;
using System.Drawing.Printing;

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

        [HttpGet("Product")]
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

        [HttpGet("Filter-product")]
        public IActionResult FilterProduct(int? page, string cateGender, string filterType)
        {
            ViewBag.Catalogs = catalogService.GetAllCatalog();
            ViewBag.Manufatures = manufactureService.GetAllManufacture();
            ViewBag.CateGender = cateGender;
            ViewBag.FilterType = filterType;

            var pageNumber = page ?? 1;
            var pageSize = 5; //Show 10 rows every time

            Gender gender = Gender.Women;
            if (cateGender == "Men")
            {
                gender = Gender.Men;
            }

            var productFilered = productService.FilterProduct(filterType, gender, pageNumber, pageSize);

            return View(productFilered);
        }

        public JsonResult ProductNameList(string keyword)
        {
            var data = productService.GetNameProductList(keyword);
            return Json(new { data = data, status = true });
        }

        [HttpGet("Search-product")]
        public IActionResult SearchProduct(int? page, string keyword)
        {
            ViewBag.Catalogs = catalogService.GetAllCatalog();
            ViewBag.Manufatures = manufactureService.GetAllManufacture();
            ViewBag.Keyword = keyword;

            var pageNumber = page ?? 1;
            var pageSize = 5; //Show 10 rows every time


            var products = productService.SearchProduct(keyword, pageNumber, pageSize);

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
