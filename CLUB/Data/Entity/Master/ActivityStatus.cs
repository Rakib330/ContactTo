using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class ActivityStatus:Base
    {
        [Required]
        public string statusName { get; set; }
        public string statusNameBn { get; set; }

        public string shortName { get; set; }
    }
}
