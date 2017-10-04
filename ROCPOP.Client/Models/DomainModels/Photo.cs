using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROCPOP.Client.Models.DomainModels
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public int PersonId { get; set; }
        public int OrderId { get; set; }
        public bool IsCust { get; set; }
    }
}
