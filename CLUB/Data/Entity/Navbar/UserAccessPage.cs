
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Auth;

namespace CLUB.Data.Entity.Navbar
{
    public class UserAccessPage:Base
    {
        public int? navbarId { get; set; }
        public Navbars navbar { get; set; }
        public int? isAccess { get; set; }
        public string applicationRoleId { get; set; }
        public ApplicationRole applicationRole { get; set; }
    }
}
