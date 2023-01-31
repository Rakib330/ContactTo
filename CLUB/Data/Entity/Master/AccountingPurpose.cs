using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Data.Entity.Master
{
    public class AccountingPurpose : Base
    {
        public string name { get; set; }
        public int? isActive { get; set; }
    }
}
