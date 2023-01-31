using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.MasterData.Models
{
    public class ServiceStatusViewModel
    {
        public int serviceStatusId { get; set; }
        [Required]
        public string statusName { get; set; }
        public string statusNameBn { get; set; }
        
        public string statusShortName { get; set; }

        public ServiceStatusLn fLang { get; set; }

        public IEnumerable<ServiceStatus> serviceStatus { get; set; }
    }
}
