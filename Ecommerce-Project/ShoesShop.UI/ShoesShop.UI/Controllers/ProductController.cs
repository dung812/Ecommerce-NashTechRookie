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
using Newtonsoft.Json;

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
     
        public ProductViewModel HandleAvgRatingProduct(ProductViewModel productViewModel)
        {
            var productId = productViewModel.ProductId;
            var getComments = commentProductService.GetListCommentOfProductById(productId);
            if (getComments.Count() == 0)
            {
                productViewModel.AvgStar = 0;
            }
            else
            {
                productViewModel.AvgStar = Functions.AverageRatingCalculator(getComments);
            }
            productViewModel.TotalComment = getComments.Count();

            return productViewModel;
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

            foreach(var i in products)
                HandleAvgRatingProduct(i);

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
            foreach (var i in productFilered)
                HandleAvgRatingProduct(i);


            return View(productFilered);
        }

        public JsonResult GetProductNameList(string keyword)
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
            foreach (var i in products)
                HandleAvgRatingProduct(i);

            return View(products);
        }

        [HttpGet("Product/{slug}-{productId}")]
        public IActionResult ProductDetail(int productId)
        {
            if (productId == null)
            {
                return RedirectToAction("Error", "Home");
            }

            // Get customer info data of session
            var cusomterSession = HttpContext.Session.GetString("CustomerInfo");
            var customerInfoSession = JsonConvert.DeserializeObject<Customer>(cusomterSession != null ? cusomterSession : "");

            // Get customer infor from session
            ViewBag.CustomerInfoSession = customerInfoSession;

            // Varial check customer are commented yet
            ViewBag.IsCommented = customerInfoSession != null ? commentProductService.CheckCustomerCommentYet(productId, customerInfoSession.CustomerId) : false;

            // Get size attribute of product
            ViewBag.SizeOfProduct = productService.GetAttributeOfProduct(productId);


            ProductViewModel product = productService.GetProductById(productId);
            HandleAvgRatingProduct(product);

            // Get related products
            ViewBag.RelativeProducts = productService.RelatedProduct(productId);

            return View(product);
        }


        // Rating product
        public JsonResult GetCommentList(int productId)
        {
            var comments = commentProductService.GetListCommentOfProductById(productId);
            return Json(new { status = 200, comments });
        }

        public JsonResult GetStarOfProduct(int productId)
        {
            var product = productService.GetProductById(productId);
            HandleAvgRatingProduct(product);

            return Json(new { status = 200, product = product });
        }

        [HttpPost]
        public JsonResult AddComment(int productId, int customerId, int star, string content)
        {
            System.Threading.Thread.Sleep(2000);

            if (commentProductService.AddCommentOfProduct(productId, customerId, star, content))
            {
                return Json(new { status = 200 });
            }
            else
            {
                return Json(new { status = 500 });
            }
        }
    }
}
