using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROCPOP.Client.Models.DomainModels
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ExtPrice { get; set; }
        public decimal UnitCost { get; set; }
        public decimal ExtCost { get; set; }
        public decimal Tax { get; set; }
        public decimal Amount { get; set; }
        public int ProductId { get; set; }
        public int ProductDetailId { get; set; }
        public int OrderId { get; set; }
        public int PromotionId { get; set; }
    }
}
