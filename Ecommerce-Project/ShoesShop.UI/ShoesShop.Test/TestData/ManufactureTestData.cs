using ShoesShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Test.TestData
{
    public class ManufactureTestData
    {
        public static List<Manufacture> GetManufactures()
        {
            return new List<Manufacture>() {
                new Manufacture() {
                    ManufactureId = 1,
                    Name = "Nike",
                    Logo = "",
                    Status = true
                },                
                new Manufacture() {
                    ManufactureId = 2,
                    Name = "Converse",
                    Logo = "",
                    Status = true
                },                
                new Manufacture() {
                    ManufactureId = 3,
                    Name = "Vans",
                    Logo = "",
                    Status = true
                },                
                new Manufacture() {
                    ManufactureId = 4,
                    Name = "Crocs",
                    Logo = "",
                    Status = true
                },
                
            };
        }
    }
}
