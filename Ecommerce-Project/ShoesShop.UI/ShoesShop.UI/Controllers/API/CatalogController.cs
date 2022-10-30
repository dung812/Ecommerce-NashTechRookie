using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.DTO;
using ShoesShop.Service;

namespace ShoesShop.UI.Controllers.API
{
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
        public ActionResult<IEnumerable<CatalogViewModel>> GetCatalogs()
        {
            List<CatalogViewModel> catalogs = catalogService.GetAllCatalog();
            return catalogs;
        }

        // GET: api/Catalog/1
        [HttpGet("{id}")]
        public ActionResult<CatalogViewModel> GetCatalog(int id)
        {
            CatalogViewModel catalog = catalogService.GetCatalogById(id);
            return catalog;
        }

        // POST: api/Catalog
        [HttpPost]
        public ActionResult CreateCatalog(CatalogViewModel catalogViewModel)
        {
            var status = catalogService.CreateCatalog(catalogViewModel);
            return status ? Ok() : BadRequest();
        }

        // PUT: api/Catalog/1
        [HttpPut("{id}")]
        public ActionResult UpdateCatalog(int id, CatalogViewModel catalogViewModel)
        {
            var status = catalogService.UpdateCatalog(id, catalogViewModel);
            return status ? Ok() : BadRequest();
        }

        // DELETE: api/Catalog/1
        [HttpDelete("{id}")]
        public ActionResult DeleteCatalog(int id)
        {
            var status = catalogService.DeleteCatalog(id);
            return status ? Ok() : BadRequest();
        }
    }
}
