using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.Domain;
using ShoesShop.DTO;
using ShoesShop.DTO.Admin;
using ShoesShop.Service;

namespace ShoesShop.API.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public ActionResult DeleteProduct(int id)
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
    }
}
