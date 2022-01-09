using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Entities
{
    public class ProductOrder
    {
        public int productId { get; set; }
        public int orderId { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }

    }
}
