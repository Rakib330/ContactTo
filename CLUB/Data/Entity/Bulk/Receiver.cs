using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Data.Entity.Bulk
{
    public class Receiver:Base
    {
        public string name { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string company { get; set; }
        public string profession { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string receiverCode { get; set; }
        public string receiverType { get; set; }
        public string status { get; set; }
    }
}
