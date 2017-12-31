using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROCPOP.Client.Models.Abstract
{
    public interface IProduct
    {
        int Id { get; set; }
        string Name { get; set; }
        int ProductTypeId { get; set; }
        string ProductTypeName { get; set; }
        string ProductDetailSmall { get; set; }
        decimal PriceSmall { get; set; }
        decimal CostSmall { get; set; }
        string ProductDetailMedium { get; set; }
        decimal PriceMedium { get; set; }
        decimal CostMedium { get; set; }
        string ProductDetailLarge { get; set; }
        decimal PriceLarge { get; set; }
        decimal CostLarge { get; set; }
        int MonthlySalesCount { get; set; }
        DateTime Created { get; set; }
    }
}
