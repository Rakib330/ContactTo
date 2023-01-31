using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CLUB.Areas.Employee.Models.Lang;
using CLUB.Data.Entity.Employee;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.Employee.Models
{
    public class LicenseViewModel
    {
        public string employeeID { get; set; }

        public string licenseId { get; set; }

        [Required]
        [Display(Name = "License Number")]
        public string licenseNumber { get; set; }

        [Required]
        [Display(Name = "Place of Issue")]
        public string place { get; set; }

        [Display(Name = "Date Of Issue")]
        public DateTime? dateOfIssue { get; set; }

        [Required]
        [Display(Name = "Date Of Expiry")]
        public DateTime? dateOfExpair { get; set; }

        public string licenseCategory { get; set; }



        public string employeeNameCode { get; set; }

        public License fLang { get; set; }

        public IEnumerable<DrivingLicense> licenses { get; set; }

    }
}
