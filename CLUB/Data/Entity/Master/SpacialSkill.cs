using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class SpacialSkill : Base
    {
        [Required]
        public string skilName { get; set; }
        public string skilNameBn { get; set; }

        public string shortName { get; set; }
    }
}
