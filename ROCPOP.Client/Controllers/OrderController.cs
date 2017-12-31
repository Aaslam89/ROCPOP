using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ROCPOP.Client.Models.ViewModels;

namespace ROCPOP.Client.Controllers
{
    public class OrderController : Controller
    {
        
        public IActionResult Checkout()
        {
            ViewBag.serializedCart = HttpContext.Session.GetString("Cart");

            OrderViewModel model = new OrderViewModel();
            if (HttpContext.Session.GetString("Cart") != null)
            {
                model.Products = JsonConvert.DeserializeObject<List<SessionCart>>(HttpContext.Session.GetString("Cart"));
            }
            else
            {
                // to do: should probably throw an error
                model.Products = new List<SessionCart>();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Checkout(OrderViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return View(model);
        }
    }
}