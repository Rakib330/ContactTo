using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class TravelPurpose:Base
    {
        [Required]
        public string purposeName { get; set; }
        public string purposeNameBn { get; set; }

        public string purposeShortName { get; set; }
    }
}
