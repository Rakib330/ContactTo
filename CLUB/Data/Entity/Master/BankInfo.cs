using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Data.Entity.Master
{
    public class BankInfo:Base
    {
        public string bankName { get; set; }
        public int? status { get; set; }
    }
}
