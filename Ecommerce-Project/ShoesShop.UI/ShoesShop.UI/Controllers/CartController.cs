using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NuGet.Common;
using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.DTO;
using ShoesShop.Service;
using ShoesShop.UI.Models;
using System.Security.Policy;
using PayPal.Api;
using PayPalCheckoutSdk.Orders;
using Item = PayPal.Api.Item;
using System.Numerics;

namespace ShoesShop.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly ICustomerAddressService customerAddressService;
        private readonly IPaymentService paymentService;
        private readonly IOrderService orderService;
        private readonly IProductService productService;

        private readonly ApplicationDbContext applicationDbContext;
        public CartController(IWebHostEnvironment hostEnvironment, ICustomerAddressService customerAddressService, IPaymentService paymentService, IOrderService orderService, IProductService productService, ApplicationDbContext applicationDbContext)
        {
            this.hostEnvironment = hostEnvironment;
            this.customerAddressService = customerAddressService;
            this.paymentService = paymentService;
            this.orderService = orderService;
            this.productService = productService;
            this.applicationDbContext = applicationDbContext;
        }

        public List<CartViewModel> GetCartSession() // Create list cart and save in session 
        {
            if (HttpContext.Session.GetString("Cart") != null)
            {
                // Get list cart data of session
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
            return new List<CartViewModel>();
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

        public JsonResult GetQuantityCartNavbar()
        {
            return Json(new { status = 200, quantity = Quanlity() });
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
            var item = listCart.FirstOrDefault(model => model.ProductId == productId && model.AttributeId == attributeId);

            if (item == null)
            {
                var product = productService.GetProductById(productId);
                var attribute = productService.GetAttributeById(attributeId);

                item = new CartViewModel(product, attribute, quantity);
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

        [HttpPost]
        public JsonResult UpdateQuantityItemCart(int cartItemId, int quantity)
        {
            //System.Threading.Thread.Sleep(1000);

            if (quantity <= 0)
            {
                return Json(new { status = 500, smg = "Invalid quantity" });
            }

            List<CartViewModel> listCart = GetCartSession();
            var item = listCart.FirstOrDefault(model => model.ItemId == cartItemId);
            if (item != null)
            {
                item.Quantity = quantity;
                item.TotalDiscountedPrice = (item.UnitPrice - item.CurrentPriceItem) * quantity;
                
                UpdateCartSession(listCart);
                return Json(new { status = 200 });
            }
            else
                return Json(new { status = 500, smg = "Not exist product" });
        }

        [HttpPost]
        public JsonResult DeteteItemCart(int cartItemId)
        {
            //System.Threading.Thread.Sleep(2000);
            List<CartViewModel> listCart = GetCartSession();
            var item = listCart.SingleOrDefault(model => model.ItemId == cartItemId);

            if (item != null)
            {
                listCart.Remove(item);
                UpdateCartSession(listCart);
                return Json(new { status = 200 });
            }

            return Json(new { status = 500 });
        }

        [HttpGet("Cart")]
        public IActionResult CartPage()
        {
            // Get customer info data of session
            var cusomterSession = HttpContext.Session.GetString("CustomerInfo");
            var customerInfoSession = JsonConvert.DeserializeObject<Customer>(cusomterSession != null ? cusomterSession : "");


            ViewBag.IsLogged = cusomterSession != null ? true : false;
            ViewBag.CustomerId = customerInfoSession?.CustomerId;

            return View();
        }
        
        [HttpGet("Checkout-{customerId}")]
        public IActionResult Checkout(int customerId)
        {
            // Get customer info data of session
            var cusomterSession = HttpContext.Session.GetString("CustomerInfo");
            var customerInfoSession = JsonConvert.DeserializeObject<Customer>(cusomterSession != null ? cusomterSession : "");
            List<CartViewModel> listCart = GetCartSession();


            if (cusomterSession == null)
            {
                TempData["error"] = "Authentication session has expired!";
                return RedirectToAction("Index", "Home");
            }
            if (customerInfoSession?.CustomerId != customerId || listCart.Count == 0)
            {
                return RedirectToAction("Error", "Home");
            }

            ViewBag.TotalPrice = TotalPrice();
            ViewBag.AddressList = customerAddressService.GetAddressListOfCustomerById(customerId);
            ViewBag.Payments = paymentService.GetAllPayment();
            ViewBag.Customer = customerInfoSession;

            return View(listCart);
        }
        
        [HttpPost]
        public IActionResult CashOnDeliveryPayment(IFormCollection form)
        {
            // Get value in form submit
            var customerId = Int32.Parse(form["customerId"][0]);
            var paymentId = Int32.Parse(form["paymentId"][0]);
            var email = form["email"][0].Trim();
            var firstName = form["first-name"][0].Trim();
            var lastName = form["last-name"][0].Trim();
            var phone = form["phone"][0].Trim();
            var address = form["address"][0].Trim();
            var note = form["note"][0];

            // Get cart list value
            List<CartViewModel> cartList = GetCartSession();
            // Get customer info data of session
            var cusomterSession = HttpContext.Session.GetString("CustomerInfo");
            var customerInfoSession = JsonConvert.DeserializeObject<Customer>(cusomterSession != null ? cusomterSession : "");

            if (cusomterSession == null)
            {
                TempData["error"] = "Authentication session has expired!";
                return RedirectToAction("Index", "Home");
            }
            if (customerInfoSession?.CustomerId != customerId || cartList.Count == 0 || cusomterSession == null)
            {
                return RedirectToAction("Error", "Home");
            }

            CreateOrderViewModel infoOrder = new CreateOrderViewModel()
            {
                customerId = customerId,
                paymentId = paymentId,
                firstName = firstName,
                lastName = lastName,
                address = address,
                phone = phone,
                note = note,
            };
           
            if (HandleSaveOrder(customerInfoSession, infoOrder))
            {
                // Clear cart session
                HttpContext.Session.Remove("Cart");
                return RedirectToAction("OrderSuccess");
            }
            else
            {
                TempData["error"] = "Something were wrong!";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public JsonResult SaveAddressNextTime(int customerId, string firstName, string lastName, string address, string phone)
        {
            CustomerAddressViewModel customerAddressViewModel = new CustomerAddressViewModel();
            customerAddressViewModel.CustomerId = customerId;
            customerAddressViewModel.FirstName = firstName;
            customerAddressViewModel.LastName = lastName;
            customerAddressViewModel.Address = address;
            customerAddressViewModel.Phone = phone;

            if (customerAddressService.CreateAddressOfCustomer(customerAddressViewModel, true))
            {
                return Json(new { status = 200 });
            }
            else
            {
                return Json(new { status = 500 });
            }
        }

        [HttpGet("Successfully-order")]
        public IActionResult OrderSuccess()
        {
            return View();
        }


        // Paypal
        private PayPal.Api.Payment payment;
        private PayPal.Api.Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var listItems = new ItemList() { items = new List<Item>() };
            List<CartViewModel> listCart = GetCartSession();
            foreach (var cart in listCart)
            {
                listItems.items.Add(new Item()
                {
                    name = cart.NameItem,
                    currency = "USD",
                    price = cart.CurrentPriceItem.ToString(),
                    quantity = cart.Quantity.ToString(),
                    sku = "sku"
                });
            }

            var payer = new PayPal.Api.Payer() { payment_method = "paypal" };

            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = listCart.Sum(model => model.Quantity * model.CurrentPriceItem).ToString()
            };

            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(), // tax + shipping + subtotal
                details = details
            };

            var transactionList = new List<Transaction>();
            transactionList.Add(new Transaction()
            {
                description = "Shoes shop transaction description",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = listItems
            });

            payment = new PayPal.Api.Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return payment.Create(apiContext);
        }
        private PayPal.Api.Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            payment = new PayPal.Api.Payment() { id = paymentId };
            return payment.Execute(apiContext, paymentExecution);
        }

        public ActionResult PaymentWithPaypal()
        {
            // Paypal
            APIContext apiContext = PaypalConfiguration.GetAPIContext();

            try
            {
                string payerId = HttpContext.Request.Query["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    string baseURL = this.Request.Scheme + "://" + this.Request.Host + "/Cart/PaymentWithPaypal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseURL + "guid=" + guid);

                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;

                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                        }
                    }

                    HttpContext.Session.SetString("guid", createdPayment.id);
                    var temp = HttpContext.Session.GetString("guid");
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = HttpContext.Request.Query["guid"];
                    var paymentId = HttpContext.Request.Query["paymentId"];
                    var executedPayment = ExecutePayment(apiContext, payerId, paymentId);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        //Session.Remove("Cart");
                        return RedirectToAction("Error", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                PaypalLogger.Log("Error: " + ex.Message);
                //Session.Remove("Cart");
                return RedirectToAction("Error", "Home");
            }

            // Clear cart session
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("OrderSuccess");
        }

        [HttpPost]
        public JsonResult SaveOrderPaypal(int paymentId, string firstName, string lastName, string address, string phone, string note)
        {
            // Get customer info data of session
            var cusomterSession = HttpContext.Session.GetString("CustomerInfo");
            var customerInfoSession = JsonConvert.DeserializeObject<Customer>(cusomterSession != null ? cusomterSession : "");

            if (customerInfoSession != null)
            {
                CreateOrderViewModel infoOrder = new CreateOrderViewModel()
                {
                    customerId = customerInfoSession.CustomerId,
                    paymentId = paymentId,
                    firstName = firstName,
                    lastName = lastName,
                    address = address,
                    phone = phone,
                    note = note,
                };

                if (HandleSaveOrder(customerInfoSession, infoOrder)) 
                    return Json(new { status = true });
                else
                    return Json(new { status = false });
            }
                
            else
                return Json(new { status = false });
        }

        public bool HandleSaveOrder(Customer customerLogin, CreateOrderViewModel infoOrderViewModel)
        {
            // Get cart list value
            List<CartViewModel> cartList = GetCartSession();

            // Handle save order
            OrderViewModel order = new OrderViewModel()
            {
                OrderId = Functions.CreateKey("HD"),
                OrderName = infoOrderViewModel.firstName + " " + infoOrderViewModel.lastName,
                OrderDate = DateTime.Now,
                Address = infoOrderViewModel.address,
                Phone = infoOrderViewModel.phone,
                Note = infoOrderViewModel.note,
                TotalMoney = (int)TotalPrice(),
                TotalDiscounted = (int)TotalDiscount()
            };

            // Save order & order detail in Database
            bool saveOderStatus = orderService.CreateNewOrder(order, infoOrderViewModel.customerId, infoOrderViewModel.paymentId);
            bool saveOderDetailStatus = orderService.CreateOrderDetail(order.OrderId, cartList);

            if (saveOderStatus && saveOderDetailStatus) // Save successfull
            {
                // Create url watch quickly order detail
                string url = String.Concat(this.Request.Scheme, "://", this.Request.Host, "/Order-detail/", order.OrderId, "-", customerLogin.CustomerId);

                // Send email new order to customer
                string content = System.IO.File.ReadAllText(Path.Combine(hostEnvironment.WebRootPath, "template\\NewOrder.html"));

                content = content.Replace("{{url}}", url);

                content = content.Replace("{{OrderId}}", order.OrderId);
                content = content.Replace("{{OrderDate}}", order.OrderDate.ToString("g"));
                content = content.Replace("{{TotalMoney}}", TotalPrice().ToString());
                content = content.Replace("{{CustomerName}}", infoOrderViewModel.firstName.Trim() + " " + infoOrderViewModel.lastName.Trim());
                content = content.Replace("{{Phone}}", infoOrderViewModel.phone);
                content = content.Replace("{{Address}}", infoOrderViewModel.address);

                Functions.SendMail(customerLogin.Email, "[Shoes shop] New Order At Footwear", content);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
