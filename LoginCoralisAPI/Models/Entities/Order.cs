using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Entities
{
    public class Order
    {
        public int id { get; set; }
        public float totalprice { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; }

    }
}
