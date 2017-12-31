using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROCPOP.Client.Models.ViewModels
{
    public class ProductCategoryListVM
    {
        public ProductCategoryListVM()
        {
            ProductCategories = new List<ProductCategoryViewModel>();
            Products = new List<ProductViewModel>();
        }
        public List<ProductCategoryViewModel> ProductCategories { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
