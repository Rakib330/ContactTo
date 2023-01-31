using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Data.Entity.Bulk
{
    public class BulkHistory : Base
    {
        public string number { get; set; }
        public string text { get; set; }
        public int? employeeId { get; set; }
        public int? groupId { get; set; }
        public int type { get; set; } // From Group = 1 Or Individuals = 2

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }
    }
}
