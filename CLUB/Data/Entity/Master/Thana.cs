using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class Thana:Base
    {
        [Required]
        public string thanaCode { get; set; }

        [Required]
        public string thanaName { get; set; }
        public string thanaNameBn { get; set; }

        public string shortName { get; set; }

        public int districtId { get; set; }

        public District district { get; set; }
    }
}
