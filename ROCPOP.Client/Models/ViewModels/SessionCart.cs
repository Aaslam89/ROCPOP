using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROCPOP.Client.Models.ViewModels
{
    public class SessionCart
    {
        public int ProdId { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public int Count { get; set; }
        public int Identifier { get; set; }
    }
}

