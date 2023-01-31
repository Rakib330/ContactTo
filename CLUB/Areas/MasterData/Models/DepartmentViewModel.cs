﻿using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CLUB.Areas.MasterData.Models
{
    public class DepartmentViewModel
    {
        public string departmentId { get; set; }
        [Required]
        [Display(Name = "Department Code")]
        public string deptCode { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        public string deptName { get; set; }
        public string deptNameBn { get; set; }

        public string shortName { get; set; }

        public DepartmentInfoLn fLang { get; set; }

        public IEnumerable<Department> departments { get; set; }
    }
}
