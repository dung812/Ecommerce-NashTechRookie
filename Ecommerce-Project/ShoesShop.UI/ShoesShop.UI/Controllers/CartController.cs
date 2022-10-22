using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoesShop.Domain;
using ShoesShop.DTO;

namespace ShoesShop.UI.Controllers
{
    public class CartController : Controller
    {
        public List<CartViewModel> GetCartSession() // Create list cart and save in session 
        {
            // Get customer info data of session
            var cartSession = HttpContext.Session.GetString("Cart");
            var cartInfoSession = JsonConvert.DeserializeObject<List<CartViewModel>>(cartSession != null ? cartSession : "");

            List<CartViewModel> listCart = cartInfoSession as List<CartViewModel>;
            if (listCart == null)
            {
                // If the list item in the cart empty, it will create a list contains items
                listCart = new List<CartViewModel>();
                var parseCart = JsonConvert.SerializeObject(listCart);
                HttpContext.Session.SetString("Cart", parseCart);
            }
            return listCart;
        }
        public void UpdateCartSession(List<CartViewModel> cart)
        {
            // Get customer info data of session
            var parseCart = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString("Cart", parseCart);
        }
        private int Quanlity() // Lấy tổng số sản phẩm giỏ hàng hiện tại
        {
            int amount = 0;
            List<CartViewModel> listCart = GetCartSession();
            if (listCart != null)
            {
                amount = listCart.Sum(model => model.Quantity);
            }
            return amount;
        }
        private double TotalPrice() //  Lấy tổng số tiền sản phẩm
        {
            double total = 0;
            List<CartViewModel> listCart = GetCartSession();
            if (listCart != null)
            {
                total = listCart.Sum(model => model.TotalPrice);
            }
            return total;
        }
        private double TotalDiscount() //  Lấy tổng số tiền đã giảm
        {
            double total = 0;
            List<CartViewModel> listCart = GetCartSession();
            if (listCart != null)
            {
                total = listCart.Sum(model => model.TotalDiscountedPrice);
            }
            return total;
        }

        public JsonResult GetCartList()
        {
            List<CartViewModel> listCart = GetCartSession();
            return Json(new { status = 200, carts = listCart, totalPrice = TotalPrice(), totalDiscount = TotalDiscount() });
        }

        [HttpPost]
        public JsonResult AddToCart(int productId, int quantity, int attributeId) //  Add item in cart 
        {
            //System.Threading.Thread.Sleep(2000);

            if (quantity <= 0)
            {
                return Json(new { status = 500, smg = "Invalid quantity" });
            }

            List<CartViewModel> listCart = GetCartSession();
            CartViewModel item = listCart.FirstOrDefault(model => model.ProductId == productId && model.AttributeId == attributeId);

            if (item == null)
            {
                item = new CartViewModel(productId, quantity, attributeId);
                if (listCart.Count() == 0)
                    item.ItemId = 1;
                else
                    item.ItemId = listCart[listCart.Count() - 1].ItemId + 1; // Get id of last item and increase + 1 for new item id

                listCart.Add(item);
                UpdateCartSession(listCart);

                return Json(new { status = 200, smg = "Successfull add product in your cart" });
            }
            else // Trường hợp đã tồn tại product trong cart
            {
                item.Quantity = item.Quantity + quantity;
                UpdateCartSession(listCart);
                return Json(new { status = 200, smg = "Successfull increase quantity this product!" });
            }
        }







        public IActionResult CartPage()
        {
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
