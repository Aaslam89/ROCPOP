using ROCPOP.Client.Models.DomainModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ROCPOP.Client.Models.ViewModels
{
    public class ProductViewModel: IProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [RegularExpression("[^0-9]", ErrorMessage = "Please enter a number")]
        public int Qty { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductDetailSmall { get; set; }
        public decimal PriceSmall { get; set; }
        public decimal CostSmall { get; set; }
        public string ProductDetailMedium { get; set; }
        public decimal PriceMedium { get; set; }
        public decimal CostMedium { get; set; }
        public string ProductDetailLarge { get; set; }
        public decimal PriceLarge { get; set; }
        public decimal CostLarge { get; set; }
        public DateTime Created { get; set; }
        public int MonthlySalesCount { get ; set ; }
    }
}
