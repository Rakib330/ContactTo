using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CLUB.Data.Entity.Master;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Data.Entity.Employee
{
    public class EmployeeLanguage : Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        [MaxLength(50)]
        public string reading { get; set; }

        [MaxLength(50)]
        public string writing { get; set; }

        [MaxLength(50)]
        public string speaking { get; set; }

        public int? languageId { get; set; }
        public Language language { get; set; }

        [MaxLength(100)]
        public string proficiency { get; set; }
    }
}
