using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Entities.DTOs
{
    public class CreateOrderDTO
    {
        public int id { get; set; }
        public float totalprice { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }

    }
}
