using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.DTO;
using ShoesShop.Service;

namespace ShoesShop.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        // GET: api/Catalog
        [HttpGet]
        public IActionResult GetCatalogs()
        {
            List<CatalogViewModel> catalogs = catalogService.GetAllCatalog();
            return Ok(catalogs);
        }

        // GET: api/Catalog/1
        [HttpGet("{id}")]
        public IActionResult GetCatalog(int id)
        {
            CatalogViewModel catalog = catalogService.GetCatalogById(id);
            return Ok(catalog);
        }

        // POST: api/Catalog
        [HttpPost]
        public IActionResult CreateCatalog(CatalogViewModel catalogViewModel)
        {
            var status = catalogService.CreateCatalog(catalogViewModel);
            return status ? Ok() : BadRequest();
        }

        // PUT: api/Catalog/1
        [HttpPut("{id}")]
        public IActionResult UpdateCatalog(int id, CatalogViewModel catalogViewModel)
        {
            var status = catalogService.UpdateCatalog(id, catalogViewModel);
            return status ? Ok() : BadRequest();
        }

        // DELETE: api/Catalog/1
        [HttpDelete("{id}")]
        public IActionResult DeleteCatalog(int id)
        {
            var status = catalogService.DeleteCatalog(id);
            return status ? Ok() : BadRequest();
        }
    }
}
