using CLUB.Data.Entity.Navbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Models.Settings
{
    public class ModuleViewModel
    {
		public int id { get; set; }
		public string nameEnglish { get; set; }
		public string nameBangla { get; set; }
		public int sortOrder { get; set; }
		public IEnumerable<Module> eRPModules { get; set; }
	}
}
