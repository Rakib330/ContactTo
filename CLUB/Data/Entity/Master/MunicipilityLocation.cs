using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class MunicipilityLocation:Base
    {
        [Required]
        public string locationName { get; set; }
        public string locationNameBn { get; set; }

        public string shortName { get; set; }
    }
}
