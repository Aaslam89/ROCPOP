using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ROCPOP.Client.Models;
using ROCPOP.Client.Services;
using ROCPOP.Client.Models.ViewModels;
using ROCPOP.Client.Models.DomainModels.Service;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ROCPOP.Client.Controllers
{
    public class HomeController : Controller
    {
        private ProductService prodService;
        public HomeController()
        {
            prodService = new ProductService();
        }

        public IActionResult Index()
        {
            ViewBag.serializedCart = HttpContext.Session.GetString("Cart");
            //if (ViewBag.serializedCart == null)
            //{
            //    string[] poop = new string[0];
            //    ViewBag.serializedCart = poop;
            //}
            List<ProductViewModel> model = TopFourProducts();
            return View(model);
        }

        //public IActionResult Nav()
        //{
        //    List<SessionCart> sessionCart;
        //    if (HttpContext.Session.GetString("Cart") != null)
        //    {
        //        sessionCart = JsonConvert.DeserializeObject<List<SessionCart>>(HttpContext.Session.GetString("Cart"));
        //    }
        //    else
        //    {
        //        sessionCart = new List<SessionCart>();
        //    }
        //    return View("Nav", sessionCart);
        //}

        public PartialViewResult PartialIndex()
        {
            List<ProductViewModel> model = TopFourProducts();
            return PartialView("Index", model);
        }

        public List<ProductViewModel> TopFourProducts()
        {
            List<ProductViewModel> model = new List<ProductViewModel>();
            var x = prodService.GetNewestItems(true);
            var y = prodService.GetBestSellers(true);
            foreach (var item in x)
            {
                model.Add(new ProductViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Attribute = "newestItems"
                });
            }
            foreach (var item in y)
            {
                model.Add(new ProductViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Attribute = "bestSeller"
                });
            }
            return model.Distinct().ToList();
        }
        
        public IActionResult About()
        {
            ViewBag.serializedCart = HttpContext.Session.GetString("Cart");

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.serializedCart = HttpContext.Session.GetString("Cart");

            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactForm model)
        {
            ViewBag.serializedCart = HttpContext.Session.GetString("Cart");
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Email email = new Email();
            email.Send(model);

            // redirect to thank you
            return View("Thanks");
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Thanks()
        {
            ViewBag.serializedCart = HttpContext.Session.GetString("Cart");
            ViewData["Message"] = "Your contact page.";

            return View();
        }
    }
}
