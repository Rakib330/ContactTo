﻿using CLUB.Data.Entity.Master;
using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Employee
{
    public class EducationalQualification : Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public string institution { get; set; }

        public int? resultId { get; set; }
        public Result result { get; set; }

        public string majorGroup { get; set; }

        public string grade { get; set; }

        public int? passingYear { get; set; }

        public int? degreeId { get; set; }
        public Degree degree { get; set; }

        public int? organizationId { get; set; }
        public Organization organization { get; set; }

        public int? reldegreesubjectId { get; set; }
        public RelDegreeSubject reldegreesubject { get; set; }

    }
}
