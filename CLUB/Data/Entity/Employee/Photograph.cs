using System;
using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Employee
{
    public class Photograph : Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        [Required]
        public string url { get; set; }

        public string remarks { get; set; }

        [Required]
        public string type { get; set; }

    }
}
