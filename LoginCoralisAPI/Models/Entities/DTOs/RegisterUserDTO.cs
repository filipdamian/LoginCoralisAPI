using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Models.Entities.DTOs
{
    public class RegisterUserDTO
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
