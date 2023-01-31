using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class Degree : Base
    {
        [Required]
        public string degreeName { get; set; }
        public string degreeNameBn { get; set; }

        public string degreeShortName { get; set; }

        public int levelofeducationId { get; set; }

        public LevelofEducation levelofeducation { get; set; }
    }
}
