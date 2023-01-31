using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class Religion:Base
    {
        [Required]
        public string name { get; set; }
        public string nameBn { get; set; }

        public string shortName { get; set; }
    }
}
