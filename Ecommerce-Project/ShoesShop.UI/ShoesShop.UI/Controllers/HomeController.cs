﻿using Microsoft.AspNetCore.Mvc;
using ShoesShop.UI.Models;
using System.Diagnostics;
using ShoesShop.DTO;
using ShoesShop.Service;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Security.Policy;
using ShoesShop.Data;
using ShoesShop.Domain;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ShoesShop.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;
        private readonly ICommentProductService commentProductService;
        private readonly IContactService contactService;
        public HomeController(IProductService productService, ICommentProductService commentProductService, IContactService contactService)
        {
            this.productService = productService;
            this.commentProductService = commentProductService;
            this.contactService = contactService;
            HttpClient client = new HttpClient();
        }

        public ProductViewModel HandleAvgRatingProduct(ProductViewModel productViewModel)
        {
            var productId = productViewModel.ProductId;
            var getComments = commentProductService.GetListCommentOfProductById(productId);
            if (getComments.Count() == 0)
            {
                productViewModel.AvgStar = 0;
            }
            else
            {
                productViewModel.AvgStar = Functions.AverageRatingCalculator(getComments);
            }
            productViewModel.TotalComment = getComments.Count();

            return productViewModel;
        }

        public IActionResult Index()
        {
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();

            productViewModels = productService.GetAllProduct();
            foreach (var i in productViewModels)
                HandleAvgRatingProduct(i);

            // Get Featured Product
            ViewBag.FeaturedProduct = productViewModels.Take(8);


            return View();
        }

        [HttpGet("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                contactService.Create(contactViewModel);
                TempData["success"] = "Your message has been received by us. We will contact you as soon as possible.";

                return RedirectToAction("Index");
            }
            else 
            {
                TempData["error"] = "Error";
            }
            return View();
        }

        [HttpGet("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // Test httpclient
        //public async Task<ActionResult> MyIndex()
        //{
        //    string Baseurl = "https://localhost:7065/api/";
        //    var products = new List<ManufactureViewModel>();
        //    using (var client = new HttpClient())
        //    {
        //        //Send HTTP requests from here.
        //        client.BaseAddress = new Uri(Baseurl);
        //        client.DefaultRequestHeaders.Clear();
        //        //Define request data format
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        //Sending request to find web api REST service resource GetAllManufacture using HttpClient
        //        HttpResponseMessage Res = await client.GetAsync("Manufacture");

        //        if (Res.IsSuccessStatusCode)
        //        {
        //            // Successfully call API

        //            //Storing the response details recieved from web api
        //            var ProdResponse = Res.Content.ReadAsStringAsync().Result;
        //            //Deserializing the response recieved from web api and storing into the Employee list
        //            products = JsonConvert.DeserializeObject<List<ManufactureViewModel>>(ProdResponse);
        //        }
        //        else
        //        {
        //            // Call API failed
        //            // Handle something
        //            TempData["error"] = "API failed";
        //            return RedirectToAction("Index");
        //        }
        //    }

        //    //returning the Manufacture list to view
        //    return View(products);
        //}
    }
}