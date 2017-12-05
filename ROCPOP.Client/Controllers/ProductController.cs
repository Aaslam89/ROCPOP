using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ROCPOP.Client.Models.ViewModels;
using ROCPOP.Client.Models.DomainModels.Service;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ROCPOP.Client.Controllers
{
    public class ProductController : Controller
    {
        private ProductService prodService;
        public ProductController()
        {
            prodService = new ProductService();
        }

        public IActionResult Index()
        {
            // List<SessionCart> sessionCart = JsonConvert.DeserializeObject<List<SessionCart>>(cart);
            ViewBag.serializedCart = HttpContext.Session.GetString("Cart");
            
            List<ProductViewModel> model = new List<ProductViewModel>();
            var products = prodService.GetProducts();

            foreach (var p in products)
            {
                model.Add(new ProductViewModel() {
                    Id = p.Id
                    , Name = p.Name
                    , ProductTypeId = p.ProductTypeId
                    , ProductTypeName = p.ProductTypeName
                    , ProductDetailSmall = p.ProductDetailSmall
                    , ProductDetailMedium = p.ProductDetailMedium
                    , ProductDetailLarge = p.ProductDetailLarge
                    , PriceSmall = p.PriceSmall
                    , PriceMedium = p.PriceMedium
                    , PriceLarge = p.PriceLarge
                    , CostSmall = p.CostSmall
                    , CostMedium = p.CostMedium
                    , CostLarge = p.CostLarge
                    , MonthlySalesCount = p.MonthlySalesCount
                    , Created = p.Created
                });
            }

            return View(model);
        }

        public IActionResult GetProductOrderForm(int id)
        {
            
            var p = prodService.GetProductById(id);
            ProductViewModel model = new ProductViewModel()
            {
                Id = p.Id
                ,Name = p.Name
                ,ProductTypeId = p.ProductTypeId
                ,ProductTypeName = p.ProductTypeName
                ,ProductDetailSmall = p.ProductDetailSmall
                ,ProductDetailMedium = p.ProductDetailMedium
                ,ProductDetailLarge = p.ProductDetailLarge
                ,PriceSmall = Math.Round(p.PriceSmall, 2)
                ,PriceMedium = Math.Round(p.PriceMedium, 2)
                ,PriceLarge = Math.Round(p.PriceLarge, 2)
                ,CostSmall = p.CostSmall
                ,CostMedium = p.CostMedium
                ,CostLarge = p.CostLarge
                ,MonthlySalesCount = p.MonthlySalesCount
                ,Created = p.Created
            };

            return View("_ProductOrder", model);
        }

        public IActionResult GetProduct(int id)
        {
            var p = prodService.GetProductById(id);
            ProductViewModel model = new ProductViewModel()
            {
                Id = p.Id
                ,Name = p.Name
                ,ProductTypeId = p.ProductTypeId
                ,ProductTypeName = p.ProductTypeName
                ,ProductDetailSmall = p.ProductDetailSmall
                ,ProductDetailMedium = p.ProductDetailMedium
                ,ProductDetailLarge = p.ProductDetailLarge
                ,PriceSmall = Math.Round(p.PriceSmall, 2)
                ,PriceMedium = Math.Round(p.PriceMedium, 2)
                ,PriceLarge = Math.Round(p.PriceLarge, 2)
                ,CostSmall = p.CostSmall
                ,CostMedium = p.CostMedium
                ,CostLarge = p.CostLarge
                ,MonthlySalesCount = p.MonthlySalesCount
                ,Created = p.Created
            };

            return Json(model);
        }

        public IActionResult GetNewestItems(bool topFour)
        {
            List<ProductViewModel> model = new List<ProductViewModel>();
            var products = prodService.GetNewestItems(topFour);

            foreach (var p in products)
            {
                model.Add(new ProductViewModel() {
                     Id = p.Id
                    ,Name = p.Name
                    ,ProductTypeId = p.ProductTypeId
                    ,ProductTypeName = p.ProductTypeName
                    ,ProductDetailSmall = p.ProductDetailSmall
                    ,ProductDetailMedium = p.ProductDetailMedium
                    ,ProductDetailLarge = p.ProductDetailLarge
                    ,PriceSmall = Math.Round(p.PriceSmall, 2)
                    ,PriceMedium = Math.Round(p.PriceMedium, 2)
                    ,PriceLarge = Math.Round(p.PriceLarge, 2)
                    ,CostSmall = p.CostSmall
                    ,CostMedium = p.CostMedium
                    ,CostLarge = p.CostLarge
                    ,MonthlySalesCount = p.MonthlySalesCount
                    ,Created = p.Created
                });
            }
            return Json(model);
        }

        public IActionResult GetBestSellers(bool topFour)
        {
            List<ProductViewModel> model = new List<ProductViewModel>();
            var products = prodService.GetNewestItems(topFour);

            foreach (var p in products)
            {
                model.Add(new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name
                });
            }
            return Json(model);
        }

        [HttpPost]
        public IActionResult StoreSessionCart(string cart)
        {
            List<SessionCart> sessionCart;
            if (!String.IsNullOrEmpty(cart))
            {
                sessionCart = JsonConvert.DeserializeObject<List<SessionCart>>(cart);
                HttpContext.Session.SetString("Cart", cart);
            }
            else
            {
                sessionCart = JsonConvert.DeserializeObject<List<SessionCart>>(HttpContext.Session.GetString("Cart"));
            }
            
            return View("_NavCart", sessionCart);
        }

        public IActionResult Checkout()
        {
            return View();
        }
    }
}