using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Entities.DTOs
{
    public class AddressDTO
    {
        public int id { get; set; }
        public string town { get; set; }
        public int street { get; set; } // ar trebui sa fie string aici revin 
        public int UserId { get; set; }
        public List<Order> Orders { get; set; }
        public User User { get; set; }
        public AddressDTO(Address Address)
        {
            this.id = Address.id;
            this.town = Address.town;
            this.street = Address.street;
            this.UserId = Address.UserId;
            this.Orders = new List<Order>();
            this.User = Address.User;



        }
    }
}
