using ShoesShop.Domain.Enum;
using ShoesShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Test.TestData
{
    public class ProductGalleriesTestData
    {
        public static List<ProductGallery> GetProductGalleries()
        {
            return new List<ProductGallery>() {
                new ProductGallery() {
                    ProductGalleryId = 1,
                    ProductId = 1,
                    GalleryName = "",
                    Status = true
                },                
                new ProductGallery() {
                    ProductGalleryId = 2,
                    ProductId = 1,
                    GalleryName = "",
                    Status = true
                },                
                new ProductGallery() {
                    ProductGalleryId = 3,
                    ProductId = 1,
                    GalleryName = "",
                    Status = true
                },
            };
        }
    }
}
