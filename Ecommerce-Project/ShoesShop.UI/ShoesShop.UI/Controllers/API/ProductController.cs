using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.DTO;
using ShoesShop.Service;

namespace ShoesShop.UI.Controllers.API
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IProductService productService;
        public ProductController(IProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            this.productService = productService;
            this.webHostEnvironment = webHostEnvironment;
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
        public ActionResult CreateProduct(ProductViewModel productViewModel)
        {
            var status = productService.CreateProduct(productViewModel);
            return status ? Ok() : BadRequest();
        }

        // PUT: api/Product/1
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, ProductViewModel productViewModel)
        {
            var status = productService.UpdateProduct(id, productViewModel);
            return status ? Ok() : BadRequest();
        }

        // DELETE: api/Product/1
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var status = productService.DeleteProduct(id);
            return status ? Ok() : BadRequest();
        }

        [HttpPost("PostImage")]
        public string PostImage(IFormFile objFile)
        {
            try
            {
                if (objFile.Length > 0)
                {
                    if (!Directory.Exists(webHostEnvironment.WebRootPath + "\\images\\products\\Image\\"))
                    {
                        Directory.CreateDirectory(webHostEnvironment.WebRootPath + "\\images\\products\\Image\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(webHostEnvironment.WebRootPath + "\\images\\products\\Image\\" + objFile.FileName))
                    {
                        objFile.CopyTo(fileStream);
                        fileStream.Flush();
                        return "\\images\\products\\Image\\" + objFile.FileName;
                    };
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }

    }
}
