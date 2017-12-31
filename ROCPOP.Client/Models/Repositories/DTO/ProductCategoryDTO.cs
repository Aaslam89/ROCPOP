using ROCPOP.Client.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROCPOP.Client.Models.Repositories.DTO
{
    public class ProductCategoryDTO : IProductCategory
    {
        public string CategoryName { get; set; }
        public int CategoryCount { get; set; }
        public int Rank { get; set; }
    }
}
