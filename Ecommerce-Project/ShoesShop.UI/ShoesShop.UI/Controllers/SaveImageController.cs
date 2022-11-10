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


        // Handle image manufacture
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

        // Handle image product
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

        [HttpPost("SaveImageProductGallery")]
        public ActionResult SaveImageProductGallery(IFormFile objFile, string imageName, string indexImageGallery)
        {
            // imageName: nashtech
            // indexImageGallery: 1
            // -> images/producs/ImageList/imageName(nashtech)/indexImageGallery(1)/    *file: objFile.FileName*

            try
            {
                if (objFile.Length > 0)
                {
                    if (!Directory.Exists(webHostEnvironment.WebRootPath + "\\images\\products\\ImageList\\" + imageName + "\\" + indexImageGallery + "\\"))
                    {
                        Directory.CreateDirectory(webHostEnvironment.WebRootPath + "\\images\\products\\ImageList\\" + imageName + "\\" + indexImageGallery + "\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(webHostEnvironment.WebRootPath + "\\images\\products\\ImageList\\" + imageName + "\\" + indexImageGallery + "\\" + objFile.FileName))
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
        public ActionResult UpdateImageProduct(IFormFile objFile, string imageFileName)
        {
            string imageNameOld = imageFileName.Split('.')[0];
            string imageNameNew = objFile.FileName.Split('.')[0];
            try
            {
                if (objFile.Length > 0)
                {
                    if (!Directory.Exists(webHostEnvironment.WebRootPath + "\\images\\products\\Image\\"))
                    {
                        Directory.CreateDirectory(webHostEnvironment.WebRootPath + "\\images\\products\\Image\\");
                    }

                    // Remove old image
                    string oldImagePath = Path.Combine(webHostEnvironment.WebRootPath + "\\images\\products\\Image\\" + imageFileName);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        // Delete image
                        System.IO.File.Delete(oldImagePath);
                    }

                    using (FileStream fileStream = System.IO.File.Create(webHostEnvironment.WebRootPath + "\\images\\products\\Image\\" + objFile.FileName))
                    {
                        objFile.CopyTo(fileStream);
                        fileStream.Flush();

                        if (Directory.Exists(webHostEnvironment.WebRootPath + "\\images\\products\\ImageList\\" + imageNameNew + "\\"))
                        {
                            Directory.Delete(webHostEnvironment.WebRootPath + "\\images\\products\\ImageList\\" + imageNameNew + "\\");
                        }
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

        [HttpPost("UpdateImageProductGallery")]
        public ActionResult UpdateImageProductGallery(IFormFile objGalleryFile, string imageName, string indexImageGallery, string oldImageGalleryFileName)
        {
            // imageName: nashtech
            // indexImageGallery: 1
            // -> images/producs/ImageList/imageName(nashtech)/indexImageGallery(1)/    *file: objFile.FileName*

            try
            {
                if (objGalleryFile.Length > 0)
                {


                    if (!Directory.Exists(webHostEnvironment.WebRootPath + "\\images\\products\\ImageList\\" + imageName + "\\" + indexImageGallery + "\\"))
                    {
                        Directory.CreateDirectory(webHostEnvironment.WebRootPath + "\\images\\products\\ImageList\\" + imageName + "\\" + indexImageGallery + "\\");
                    }


                    // Remove old image
                    string oldImagePath = Path.Combine(webHostEnvironment.WebRootPath + "\\images\\products\\ImageList\\" + imageName + "\\" + indexImageGallery + "\\" + oldImageGalleryFileName);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    // Create new image
                    using (FileStream fileStream = System.IO.File.Create(webHostEnvironment.WebRootPath + "\\images\\products\\ImageList\\" + imageName + "\\" + indexImageGallery + "\\" + objGalleryFile.FileName))
                    {
                        objGalleryFile.CopyTo(fileStream);
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
    }
}
