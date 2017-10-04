using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROCPOP.Client.Models.DomainModels
{
    public class Orders
    {
        public int Id { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public DateTime CancelDate { get; set; }
        public decimal OrderTotal { get; set; }
        public int CustomerId { get; set; }
        public Photo Photo { get; set; }
    }
}
