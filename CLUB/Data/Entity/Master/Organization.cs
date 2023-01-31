using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class Organization : Base
    {
        [Required]
        public string organizationName { get; set; }
        public string organizationNameBn { get; set; }

        [Required]
        public string organizationType { get; set; }
    }
}
