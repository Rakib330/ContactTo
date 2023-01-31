using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class Award:Base
    {
        [Required]
        public string awardName { get; set; }
        public string awardNameBn { get; set; }

        public string awardShortName { get; set; }
    }
}
