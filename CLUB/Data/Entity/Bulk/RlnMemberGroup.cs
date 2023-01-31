using CLUB.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Data.Entity.Bulk
{
    public class RlnMemberGroup:Base
    {
        public int? memberGroupId { get; set; }
        public MemberGroup memberGroup { get; set; }

        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public string type { get; set; }
    }
}
