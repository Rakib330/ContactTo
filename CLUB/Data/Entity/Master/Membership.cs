using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class Membership:Base
    {
        [Required]
        public string membershipName { get; set; }
        public string membershipNameBn { get; set; }

        public string membershipShortName { get; set; }
    }
}
