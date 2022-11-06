using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.DTO;
using ShoesShop.Service;

namespace ShoesShop.UI.Controllers.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ManufactureController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IManufactureService manufactureService;
        public ManufactureController(IWebHostEnvironment webHostEnvironment, IManufactureService manufactureService)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.manufactureService = manufactureService;
        }

        // GET: api/Manufacture
        [HttpGet]
        public ActionResult<IEnumerable<ManufactureViewModel>> GetManufactures()
        {
            List<ManufactureViewModel> manufactures = manufactureService.GetAllManufacture();
            return manufactures;
        }

        // GET: api/Manufacture/1
        [HttpGet("{id}")]
        public ActionResult<ManufactureViewModel> GetManufacture(int id)
        {
            ManufactureViewModel manufacture = manufactureService.GetManufactureById(id);
            return manufacture;
        }

        // POST: api/Manufacture
        [HttpPost]
        public ActionResult CreateManufacture(ManufactureViewModel manufactureViewModel)
        {
            var status = manufactureService.CreateManufacture(manufactureViewModel);
            return status ? Ok() : BadRequest();
        }

        // PUT: api/Manufacture/1
        [HttpPut("{id}")]
        public ActionResult UpdateManufacture(int id, ManufactureViewModel manufactureViewModel)
        {
            var status = manufactureService.UpdateManufacture(id, manufactureViewModel);
            return status ? Ok() : BadRequest();
        }

        // DELETE: api/Manufacture/1
        [HttpDelete("{id}")]
        public ActionResult DeleteManufacture(int id)
        {
            var status = manufactureService.DeleteManufacture(id);
            return status ? Ok() : BadRequest();
        }

        // POST: api/Manufacture/PostImage
        [HttpPost("PostImage")]
        public string PostImage(IFormFile objFile)
        {
            try
            {
                if (objFile.Length > 0)
                {
                    if (!Directory.Exists(webHostEnvironment.WebRootPath + "\\images\\avatars\\"))
                    {
                        Directory.CreateDirectory(webHostEnvironment.WebRootPath + "\\images\\avatars\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(webHostEnvironment.WebRootPath + "\\images\\avatars\\" + objFile.FileName))
                    {
                        objFile.CopyTo(fileStream);
                        fileStream.Flush();
                        return "\\images\\avatars\\" + objFile.FileName;
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
