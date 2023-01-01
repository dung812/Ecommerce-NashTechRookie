using Microsoft.CodeAnalysis;
using ShoesShop.Domain;
using ShoesShop.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Test.TestData
{
    public class ProductTestData
    {
        public static List<Product> GetProducts()
        {
            return new List<Product>() {
                new Product() {
                    ProductId = 1,
                    ProductName = "Product A",
                    OriginalPrice = 0,
                    PromotionPercent = 0,
                    Description = "",
                    Quantity = 0,
                    Status = true,
                    ProductGenderCategory = Gender.Men,
                    DateCreate = DateTime.Now,
                    ImageFileName = "",
                    ImageName = "",
                    AdminId = 1,
                    CatalogId = 1,
                    ManufactureId = 1,
                },                
                new Product() {
                    ProductId = 2,
                    ProductName = "Product B",
                    OriginalPrice = 0,
                    PromotionPercent = 0,
                    Description = "",
                    Quantity = 0,
                    Status = true,
                    ProductGenderCategory = Gender.Men,
                    DateCreate = DateTime.Now,
                    ImageFileName = "",
                    ImageName = "",
                    AdminId = 1,
                    CatalogId = 1,
                    ManufactureId = 1,
                },
            };
        }
    }
}
