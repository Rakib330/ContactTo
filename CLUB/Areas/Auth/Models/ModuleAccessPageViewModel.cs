using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.Auth.Models
{
    public class ModuleAccessPageViewModel
    {
        public int? eRPModuleId { get; set; }
        public string eRPModuleName { get; set; }

        public int active { get; set; }
    }
}
