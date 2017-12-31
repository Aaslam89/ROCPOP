using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROCPOP.Client.Models.ViewModels
{
    public class PhotoViewModel
    {
        public int PhotoId { get; set; }
        public int PersonId { get; set; }
        public int OrderId { get; set; }
        public byte[] ImageData { get; set; }
        public string fileExtension { get; set; }
    }
}
