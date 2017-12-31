using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROCPOP.Client.Models.Abstract
{
    public interface IProductCategory
    {
        string CategoryName { get; set; }
        int CategoryCount { get; set; }
        int Rank { get; set; }
    }
}
