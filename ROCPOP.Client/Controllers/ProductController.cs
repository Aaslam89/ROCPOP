using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ROCPOP.Client.Models.ViewModels;
using ROCPOP.Client.Models.DomainModels.Service;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ROCPOP.Client.Models.Repositories.DTO;

namespace ROCPOP.Client.Controllers
{
    public class ProductController : Controller
    {
        private ProductService prodService;
        public ProductController()
        {
            prodService = new ProductService();
        }

        public IActionResult Products()
        {
            // List<SessionCart> sessionCart = JsonConvert.DeserializeObject<List<SessionCart>>(cart);
            ViewBag.serializedCart = HttpContext.Session.GetString("Cart");

            ProductCategoryListVM model = new ProductCategoryListVM();
            var products = prodService.GetProducts();
            var prodCategories = prodService.GetProductCategories();

            foreach (var p in products)
            {
                model.Products.Add(new ProductViewModel() {
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
            int total = prodCategories.First().CategoryCount;
            foreach (var c in prodCategories)
            {
                model.ProductCategories.Add(new ProductCategoryViewModel(){
                    CategoryName = c.CategoryName,
                    CategoryCount = c.CategoryCount,
                    Rank = c.Rank
                });
            }
            model.ProductCategories.Add(new ProductCategoryViewModel()
            {
                CategoryName = "Best Sellers",
                CategoryCount = total,
            });
            model.ProductCategories.Add(new ProductCategoryViewModel()
            {
                CategoryName = "Newest Items",
                CategoryCount = total,
            });

            return View(model);
        }

        public PartialViewResult ProductPartial(string category)
        {
            List<ProductDTO> dto = new List<ProductDTO>();
            List<ProductViewModel> model = new List<ProductViewModel>();
            List<string> categories = prodService.GetCategories();
            switch (category)
            {
                case "All":
                    dto = prodService.GetProducts();
                    break;
                case "Best Sellers":
                    dto = prodService.GetBestSellers(false);
                    break;
                case "Newest Items":
                    dto = prodService.GetNewestItems(false);
                    break;
                default:
                    if (categories.Contains(category))
                        dto = prodService.GetProductByCategory(category);
                    else
                        dto = prodService.GetProducts();
                    break;
            }

            foreach (var d in dto)
            {
                model.Add(new ProductViewModel() {
                    Id = d.Id,
                    Name = d.Name
                });
            }

            return PartialView("_ProductList", model);
        }

        public IActionResult Cart()
        {
            ViewBag.serializedCart = HttpContext.Session.GetString("Cart");
            List<SessionCart> sessionCart;
            int poop = 0;
            if (HttpContext.Session.GetString("Cart") != null)
            {
                sessionCart = JsonConvert.DeserializeObject<List<SessionCart>>(HttpContext.Session.GetString("Cart"));
                foreach (var item in sessionCart)
                {
                    item.Identifier = ++poop;
                }
            }
            else
            {
                sessionCart = new List<SessionCart>();
            }

            return View(sessionCart);
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

        [HttpPost]
        public IActionResult GetSessionCart()
        {
            List<SessionCart> sessionCart;
            if (HttpContext.Session.GetString("Cart") != null)
            {
                sessionCart = JsonConvert.DeserializeObject<List<SessionCart>>(HttpContext.Session.GetString("Cart"));              
            }
            else
            {
                sessionCart = new List<SessionCart>();
            }

            return Json(sessionCart);
        }
    }
}