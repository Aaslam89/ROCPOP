using ROCPOP.Client.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROCPOP.Client.Models.ViewModels
{
    public class ShoppingCart
    {
        public Customer Customer { get; set; }
        public Orders Order{ get; set; }
        public List<OrderDetail> Details { get; set; }
    }
}
