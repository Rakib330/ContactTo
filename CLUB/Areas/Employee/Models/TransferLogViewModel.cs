using CLUB.Areas.Employee.Models.Lang;
using CLUB.Data.Entity.Employee;
using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.Employee.Models
{
    public class TransferLogViewModel
    {
        public string employeeID { get; set; }

        public string transfarID { get; set; }

        public string workStation { get; set; }

        public DateTime? fromDate { get; set; }

        public DateTime? toDate { get; set; }

        [Required]
        public int grade { get; set; }

        [Required]
        public string designation { get; set; }

        public string department { get; set; }

        public string employeeNameCode { get; set; }

        public TransferLogLn fLang { get; set; }

        public IEnumerable<SalaryGrade> salaryGrade { get; set; }

        public IEnumerable<TransferLog> transferLogs { get; set; }
        
    }
}
