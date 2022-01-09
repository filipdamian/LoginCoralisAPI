
using LoginCoralisAPI.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Entities
{
    public class User: IdentityUser<int>
    {
        //am facut inainte de laboratul cu autentificare clasa user fara sa stiu ca identityuser implementeaza campuri precum name/phone/password 
        //am sa le las totusi pentru testare 
        //in mod normal trebuie scoase si folosite cele din identityuser
        //in implementarea propriuzisa sunt folosite atributele din identityuser
        public User() : base() { }
        //public string id { get; set; }
        public string  name { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public Address Address { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
