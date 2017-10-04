using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ROCPOP.Client.Models;
using ROCPOP.Client.Services;
using ROCPOP.Client.Models.ViewModels;

namespace ROCPOP.Client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
