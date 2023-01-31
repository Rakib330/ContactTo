using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CLUB.Areas.Employee.Models.Lang;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Employee;

namespace CLUB.Areas.Employee.Models
{
    public class MembershipViewModel
    {
        public string employeeID { get; set; }

        public string membershipId { get; set; }

        [Required]
        [Display(Name = "Name of Organization")]
        public string nameOrganization { get; set; }

        [Required]
        [Display(Name = "Membership Type")]
        public string membershipType { get; set; }

        [Display(Name = "Remarks")]
        public string remarks { get; set; }

        public Membership fLang { get; set; }

        public IEnumerable<EmployeeMembership> employeeMemberships { get; set; }
        public IEnumerable<Data.Entity.Master.Membership> memberships { get; set; }
    }
}
