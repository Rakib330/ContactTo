using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CLUB.Areas.Employee.Models.Lang;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data.Entity.Master;
using CLUB.Data.Entity.Employee;

namespace CLUB.Areas.Employee.Models
{
    public class TrainingViewModel
    {
        public int employeeID { get; set; }

        public int trainingLogID { get; set; }

        [Required]
        public DateTime? fromDate { get; set; }

        [Required]
        public DateTime? toDate { get; set; }
        
        public string country { get; set; }

        [Required]
        public string category { get; set; }

        [Required]
        public string trainingTitle { get; set; }

        [Required]
        public string institute { get; set; }

        public string sponsoringAgency { get; set; }

        public string remarks { get; set; }

        public string employeeNameCode { get; set; }

        public TrainingLn fLang { get; set; }

        public IEnumerable<Country> countries { get; set; }

        public IEnumerable<TrainingCategory> trainingCategories { get; set; }

        public IEnumerable<TrainingInstitute> trainingInstitutes { get; set; }

        public IEnumerable<TraningLog> traningLogs { get; set; }
    }
}
