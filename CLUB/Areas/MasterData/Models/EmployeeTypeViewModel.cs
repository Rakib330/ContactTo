using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CLUB.Areas.MasterData.Models
{
    public class EmployeeTypeViewModel
    {
        public int empTypeId { get; set; }
        [Required]
        [Display(Name = "Employee Type")]
        public string empType { get; set; }
        public string empTypeBn { get; set; }

        public decimal? charge { get; set; }

        public string shortName { get; set; }

        public EmployeeTypeLn fLang { get; set; }

        public IEnumerable<EmployeeType> employeeTypes { get; set; }
    }
}
