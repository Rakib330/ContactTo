using CLUB.Data.Entity.Employee;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Data.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string org { get; set; }
    }
}
