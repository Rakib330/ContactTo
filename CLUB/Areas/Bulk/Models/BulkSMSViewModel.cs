using CLUB.Data.Entity.Bulk;
using CLUB.Data.Entity.Employee;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.Bulk.Models
{
    public class BulkSMSViewModel
    {
        public int[] ids { get; set; }
        public int[] groups { get; set; }
        public string[] name { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
        public string SMS { get; set; }
        public string Whatsapp { get; set; }
        public string Email { get; set; }
        public IFormFile file { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public IEnumerable<MemberGroup> memberGroups { get; set; }

        public IEnumerable<Receiver> receivers { get; set; }
        public IEnumerable<ReceiverGroup> receiverGroups { get; set; }
    }
}
