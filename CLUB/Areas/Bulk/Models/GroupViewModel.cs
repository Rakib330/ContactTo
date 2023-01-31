using CLUB.Data.Entity.Bulk;
using CLUB.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.Bulk.Models
{
    public class GroupViewModel
    {
        public int groupId { get; set; }
        public string name { get; set; }
        public int[] ids { get; set; }

        public string subject { get; set; }
        public string description { get; set; }

        public string SMS { get; set; }
        public string Email { get; set; }

        public int[] receiverId { get; set; }
        public string[] receiverName { get; set; }
        public string[] receiverEmail { get; set; }
        public string[] receiverMobile { get; set; }
        public string[] receiverDesignation { get; set; }
        public string[] receiverDepartment { get; set; }
        public string[] receiverCompany { get; set; }
        public string[] receiverProfession { get; set; }

        public IEnumerable<MemberGroup> memberGroups { get; set; }
        public IEnumerable<ReceiverGroup> receiverGroups { get; set; }

        public RlnMemberGroup rlnMemberGroup { get; set; }

        public IEnumerable<RlnMemberGroup> rlnMemberGroups { get; set; }

        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public IEnumerable<Receiver> receivers { get; set; }
    }
    public class ReceiverViewModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string company { get; set; }
        public string profession { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }

        
        public IEnumerable<ReceiverGroup> receiverGroups { get; set; }
        public IEnumerable<Receiver> receivers { get; set; }
        public Receiver receiver { get; set; }
    }
}
