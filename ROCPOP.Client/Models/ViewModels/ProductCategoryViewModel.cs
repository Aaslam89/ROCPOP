using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ROCPOP.Client.Models.Abstract;

namespace ROCPOP.Client.Models.ViewModels
{
    public class ProductCategoryViewModel : IProductCategory
    {
        public string CategoryName { get; set; }
        public int CategoryCount { get; set; }
        public int Rank { get; set; }
    }
}
