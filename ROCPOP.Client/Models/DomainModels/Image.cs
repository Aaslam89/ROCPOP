using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROCPOP.Client.Models.DomainModels
{
    public class Image
    {
        public byte[] Img { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
    }
}
