using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.Auth.Models
{
    public class NavbarWithRoleVm
    {
        public int? Id { get; set; }
        public string nameOption { get; set; }
        public int? parentID { get; set; }
        public int? isParent { get; set; }
        public int? moduleId { get; set; }
        public bool? status { get; set; }
        public string applicationRoleId { get; set; }
    }
}
