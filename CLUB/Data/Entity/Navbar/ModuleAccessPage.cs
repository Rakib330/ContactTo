
using CLUB.Data.Entity.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Data.Entity.Navbar
{
    public class ModuleAccessPage:Base
    {
        public int? eRPModuleId { get; set; }
        public Module eRPModule { get; set; }
        public string applicationRoleId { get; set; }
        public ApplicationRole applicationRole { get; set; }
    }
}
