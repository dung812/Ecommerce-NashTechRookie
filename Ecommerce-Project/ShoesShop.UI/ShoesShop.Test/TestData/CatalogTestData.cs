using ShoesShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShoesShop.Test.TestData
{
    public class CatalogTestData
    {
        public static List<Catalog> GetCatalogs()
        {
            return new List<Catalog>() {
                new Catalog() {
                   CatalogId = 1,
                   Name = "Boots",
                   Status = true
                },               
                new Catalog() {
                   CatalogId = 2,
                   Name = "Clogs and Mules",
                   Status = true
                },               
                new Catalog() {
                   CatalogId = 3,
                   Name = "Flats",
                   Status = true
                },               
                new Catalog() {
                   CatalogId = 4,
                   Name = "Heels",
                   Status = true
                },               
                new Catalog() {
                   CatalogId = 5,
                   Name = "Sandals",
                   Status = true
                }           
            };
        }
    }
}
