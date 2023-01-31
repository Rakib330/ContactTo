
using CLUB.Areas.Auth.Models;
using CLUB.Data.Entity.Navbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Models.Auth
{
    public class ApplicationRoleViewModel
    {
        public string RoleId { get; set; }
        public string PreRoleId { get; set; }
        public string[] roleIdList { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
        public string EuserName { get; set; }
        public string RoleName { get; set; }
        public string RoleNature { get; set; }
        public int? moduleId { get; set; }
        public string description { get; set; }
        public string moduleName { get; set; }
        public IEnumerable<Module> eRPModules { get; set; }
        public IEnumerable<ApplicationRoleViewModel> roleViewModels { get; set; }

		public IEnumerable<NavbarWithRoleVm> navbars { get; set; }
		public IEnumerable<Navbars> accessedNavbars { get; set; }
		public IEnumerable<Module> modules { get; set; }

        public int?[] navbarId { get; set; }
        public string aspnetroleId { get; set; }

    }
}
