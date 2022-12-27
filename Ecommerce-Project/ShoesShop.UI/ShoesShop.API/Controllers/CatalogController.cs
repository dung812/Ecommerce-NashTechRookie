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
        private readonly IAdminService adminService;

        public CatalogController(ICatalogService catalogService, IAdminService adminService)
        {
            this.catalogService = catalogService;
            this.adminService = adminService;
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

            if (status)
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);
                adminService.SaveActivity(adminId, "Create", "Catalog", catalogViewModel.Name);
                return Ok();
            }
            else return BadRequest();
        }

        // PUT: api/Catalog/1
        [HttpPut("{id}")]
        public IActionResult UpdateCatalog(int id, CatalogViewModel catalogViewModel)
        {
            string oldCatalogName = catalogService.GetCatalogById(id).Name;
            var status = catalogService.UpdateCatalog(id, catalogViewModel);

            if (status)
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);
                adminService.SaveActivity(adminId, "Update", "Catalog", oldCatalogName);
                return Ok();
            }
            else return BadRequest();
        }

        // DELETE: api/Catalog/1
        [HttpDelete("{id}")]
        public IActionResult DeleteCatalog(int id)
        {
            var status = catalogService.DeleteCatalog(id);

            if (status)
            {
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(m => m.Type == "id").Value);
                adminService.SaveActivity(adminId, "Soft delete", "Catalog", catalogService.GetCatalogById(id).Name);
                return Ok();
            }
            else return BadRequest();
        }
    }
}
