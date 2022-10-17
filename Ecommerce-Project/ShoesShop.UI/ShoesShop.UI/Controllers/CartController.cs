using Microsoft.AspNetCore.Mvc;

namespace ShoesShop.UI.Controllers
{
    public class CartController : Controller
    {
        public IActionResult CartPage()
        {
            //// Send email
            //string content = System.IO.File.ReadAllText(Path.Combine(_hostEnvironment.WebRootPath, "template\\NewOrder.html"));

            //content = content.Replace("{{url}}", "123");

            //content = content.Replace("{{OrderId}}", "TestId");
            //content = content.Replace("{{OrderDate}}", "TestDate");
            //content = content.Replace("{{TotalMoney}}", "TestMoney");
            //content = content.Replace("{{CustomerName}}", "Test Name");
            //content = content.Replace("{{Phone}}", "Test phone");
            //content = content.Replace("{{Address}}", "Test Address");

            //Functions.SendMail("ntdung8124@gmail.com", "New Order At Footwear", content);
            return View();
        }        
        public IActionResult Checkout()
        {
            return View();
        }        
        public IActionResult OrderSuccess()
        {
            return View();
        }
    }
}
