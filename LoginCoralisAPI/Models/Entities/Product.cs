using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Entities
{
    public class Product
    {
        public int id { get; set; }
        public string productname { get; set; }
        public int quantity { get; set; }
        public float unitprice { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; }

    }
}
