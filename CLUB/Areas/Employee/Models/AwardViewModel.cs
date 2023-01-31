using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CLUB.Areas.Employee.Models.Lang;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Employee;

namespace CLUB.Areas.Employee.Models
{
    public class AwardViewModel
    {
        public string employeeID { get; set; }

        public string awardId { get; set; }

        [Required]
        [Display(Name = "Award Name")]
        public string awardName { get; set; }

        [Display(Name = "Perpose")]
        public string perpose { get; set; }

        [Display(Name = "Date")]
        public DateTime? txtAwardDate { get; set; }

        public string action { get; set; }

        public string employeeNameCode { get; set; }

        public Award fLang { get; set; }

        public IEnumerable<EmployeeAward> awards { get; set; }
    }
}
