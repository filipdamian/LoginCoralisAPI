using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Entities
{

    public class Address
    {

        [Key]
        public int id { get; set; }
        public string town { get; set; }
        public int street { get; set; } 
        [ForeignKey("User")]
        public int UserId { get; set; }
        public ICollection<Order> Orders { get; set; }
        public User User { get; set; }

    }
}
