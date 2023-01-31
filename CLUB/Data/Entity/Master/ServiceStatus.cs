using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class ServiceStatus:Base
    {
        [Required]
        public string statusName { get; set; }
        public string statusNameBn { get; set; }

        public string statusShortName { get; set; }
    }
}
