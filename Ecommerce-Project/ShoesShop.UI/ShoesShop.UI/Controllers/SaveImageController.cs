using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoesShop.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveImageController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public SaveImageController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }


        // POST: api/Manufacture/PostImage
        [HttpPost("SaveImageManufacture")]
        public string SaveImageManufacture(IFormFile objFile)
        {
            try
            {
                if (objFile.Length > 0)
                {
                    if (!Directory.Exists(webHostEnvironment.WebRootPath + "\\images\\brand\\"))
                    {
                        Directory.CreateDirectory(webHostEnvironment.WebRootPath + "\\images\\brand\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(webHostEnvironment.WebRootPath + "\\images\\brand\\" + objFile.FileName))
                    {
                        objFile.CopyTo(fileStream);
                        fileStream.Flush();
                        return "\\images\\brand\\" + objFile.FileName;
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
        [HttpPost("SaveImageProduct")]
        public ActionResult SaveImageProduct(IFormFile objFile)
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
                        return Ok();
                    };
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost("UpdateImageProduct")]
        public ActionResult UpdateImageProduct(IFormFile objFile, string imageName)
        {
            string imageNameOld = imageName.Split('.')[0];
            string imageNameNew = objFile.FileName.Split('.')[0];
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

                        Directory.Move(webHostEnvironment.WebRootPath + "\\images\\products\\ImageList\\" + imageNameOld + "\\", webHostEnvironment.WebRootPath + "\\images\\products\\ImageList\\" + imageNameNew + "\\");
                        return Ok();
                    };
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost("SaveImageProductGallery")]
        public ActionResult SaveImageProductGallery(IFormFile objFile, string imageName, string imageGalleryName)
        {
            try
            {
                if (objFile.Length > 0)
                {
                    if (!Directory.Exists(webHostEnvironment.WebRootPath + "\\images\\products\\ImageList\\" + imageName + "\\"))
                    {
                        Directory.CreateDirectory(webHostEnvironment.WebRootPath + "\\images\\products\\ImageList\\" + imageName + "\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(webHostEnvironment.WebRootPath + "\\images\\products\\ImageList\\" + imageName + "\\" + imageGalleryName))
                    {
                        objFile.CopyTo(fileStream);
                        fileStream.Flush();
                        return Ok();
                    };
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }




        //[HttpPost("PostImageGalleryNew")]
        //public ActionResult PostImageGalleryNew(IFormFile objFile, string imageName, string indexImageGallery)
        //{
        //    // imageName: nashtech
        //    // indexImageGallery: 1
        //    // -> images/producs/ImageList/imageName(nashtech)/indexImageGallery(1)/    *file: objFile.FileName*

        //    try
        //    {
        //        if (objFile.Length > 0)
        //        {
        //            if (!Directory.Exists(webHostEnvironment.WebRootPath + "\\images\\products\\ImageList\\" + imageName + "\\" + indexImageGallery + "\\"))
        //            {
        //                Directory.CreateDirectory(webHostEnvironment.WebRootPath + "\\images\\products\\ImageList\\" + imageName + "\\" + indexImageGallery + "\\");
        //            }
        //            using (FileStream fileStream = System.IO.File.Create(webHostEnvironment.WebRootPath + "\\images\\products\\ImageList\\" + imageName + "\\" + indexImageGallery + "\\" + objFile.FileName))
        //            {
        //                objFile.CopyTo(fileStream);
        //                fileStream.Flush();
        //                return Ok();
        //            };
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        return BadRequest();
        //    }
        //}

    }
}
