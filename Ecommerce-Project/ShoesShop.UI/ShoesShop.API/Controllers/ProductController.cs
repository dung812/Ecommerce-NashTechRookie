using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Domain;
using ShoesShop.DTO;
using ShoesShop.DTO.Admin;
using ShoesShop.Service;

namespace ShoesShop.API.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IAdminService adminService;
        public ProductController(IProductService productService, IAdminService adminService)
        {
            this.productService = productService;
            this.adminService = adminService;
        }

        // GET: api/Product
        [HttpGet]
        public ActionResult<IEnumerable<ProductViewModel>> GetProducts()
        {
            List<ProductViewModel> products = productService.GetAllProduct();
            return products;
        }

        // GET: api/Product/1
        [HttpGet("{id}")]
        public ActionResult<ProductViewModel> GetProduct(int id)
        {
            ProductViewModel product = productService.GetProductById(id);
            return product;
        }

        // POST: api/Product
        [HttpPost]
        public ActionResult CreateProduct(CreateProductViewModel productViewModel)
        {
            var status = productService.CreateProduct(productViewModel);

            if (status)
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);
                adminService.SaveActivity(adminId, "Create", "Product", productViewModel.ProductName);
                return Ok();
            }
            else return BadRequest();
        }

        // PUT: api/Product/1
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, ProductViewModel productViewModel)
        {
            string oldProductName = productService.GetProductById(id).ProductName;
            var status = productService.UpdateProduct(id, productViewModel);

            if (status)
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);
                adminService.SaveActivity(adminId, "Update", "Product", oldProductName);
                return Ok();
            }
            else return BadRequest();
        }

        // DELETE: api/Product/1
        [HttpDelete("{id}")]
        public ActionResult SoftDeleteProduct(int id)
        {
            var status = productService.DeleteProduct(id);

            if (status)
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);
                adminService.SaveActivity(adminId, "Soft delete", "Product", "");
                return Ok();
            }
            else return BadRequest();
        }

        // GET: api/Product/GetProductsDisabled
        [HttpGet("[action]")]
        public ActionResult<List<ProductViewModel>> GetProductsDisabled()
        {
            List<ProductViewModel> products = productService.GetAllProductDisabled();
            return products;
        }

        // GET: api/Product/RestoreProduct/1
        [HttpGet("[action]/{id}")]
        public ActionResult<List<ProductViewModel>> RestoreProduct(int id)
        {
            var status = productService.RestoreProduct(id);

            if (status)
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);
                adminService.SaveActivity(adminId, "Restore", "Product", "");
                return Ok();
            }
            else return BadRequest();
        }

        // GET: api/Product/CheckProductCanDelete/1
        [HttpGet("[action]/{id}")]
        public IActionResult CheckProductCanDelete(int id)
        {
            bool CheckExistedOrder = productService.CheckExistedOrder(id);
            return !CheckExistedOrder ? NoContent() : BadRequest();
        }

        // DELETE: api/Product/DeleteProduct/1
        [HttpDelete("[action]/{id}")]
        public ActionResult DeleteProduct(int id)
        {
            bool CheckExistedOrder = productService.CheckExistedOrder(id);
            if (!CheckExistedOrder)
            {
                var status = productService.HardDeleteProduct(id);

                if (status)
                {
                    var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);
                    adminService.SaveActivity(adminId, "Delete", "Product", "");
                    return NoContent();
                }
                else
                    return BadRequest();
            }
            else 
                return BadRequest("Have product in order");
        }

    }
}
