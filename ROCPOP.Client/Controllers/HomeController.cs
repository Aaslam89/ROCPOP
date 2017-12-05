using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ROCPOP.Client.Models;
using ROCPOP.Client.Services;
using ROCPOP.Client.Models.ViewModels;
using ROCPOP.Client.Models.DomainModels.Service;

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
            List<ProductViewModel> model = TopFourProducts();
            return View(model);
        }

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
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactForm model)
        {
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
            ViewData["Message"] = "Your contact page.";

            return View();
        }
    }
}
