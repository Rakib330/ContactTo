using CLUB.Data.Entity.Navbar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.Auth.Models
{
    [NotMapped]
    public class NavbarViewModel
    {
        public int? Id { get; set; }

        public string nameOption { get; set; }

        public string nameOptionBangla { get; set; }

        public string controller { get; set; }

        public string action { get; set; }

        public string area { get; set; }

        public string imageClass { get; set; }

        public string activeLi { get; set; }

        public bool status { get; set; }

        public int parentID { get; set; }

        public int? bandID { get; set; }

        public int? isParent { get; set; }

        public int? displayOrder { get; set; }

        public int? moduleId { get; set; }

        public IEnumerable<Navbars> navbarsbyparent { get; set; }
        public IEnumerable<Navbars> navbars { get; set; }
        public IEnumerable<Module> ERPModules { get; set; }

        [NotMapped]
        public IEnumerable<string> userRoles { get; set; }
    }
}
