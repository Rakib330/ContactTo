using CLUB.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Data.Entity.Bulk
{
    public class RlnReceiverGroup: Base
    {
        public int? receiverGroupId { get; set; }
        public ReceiverGroup receiverGroup { get; set; }

        public int? receiverId { get; set; }
        public Receiver receiver { get; set; }

        public string type { get; set; }
    }
}
