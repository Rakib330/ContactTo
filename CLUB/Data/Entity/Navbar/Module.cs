using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Data.Entity.Navbar
{
    public class Module:Base
    {
        public string moduleName { get; set; }
        public string moduleNameBN { get; set; }
        public int? shortOrder { get; set; }
        public string isTeam { get; set; }
    }
}
