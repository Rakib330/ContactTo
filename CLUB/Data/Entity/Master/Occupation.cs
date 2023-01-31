using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class Occupation:Base
    {
        [Required]
        public string occupationName { get; set; }
        public string occupationNameBn { get; set; }

        public string occupationShortName { get; set; }
    }
}
