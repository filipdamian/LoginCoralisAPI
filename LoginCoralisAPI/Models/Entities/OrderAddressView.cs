using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Models.Entities
{
    public class OrderAddressView
    {
        public int id { get; set; }
        public float totalprice { get; set; }
        public string town { get; set; }
        public int street { get; set; }
    }
}
