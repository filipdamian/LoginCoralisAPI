﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Entities.DTOs
{
    public class UserDTO
    {
       // public string id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public Address Address { get; set; }
        public UserDTO(User Account)
        {
            //this.id = Account.id;
            this.name = Account.name;
            this.phone = Account.phone;
            this.password = Account.password;
            this.Address = Account.Address;

        }
    }
}
