using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class LevelofEducation:Base
    {
        [Required]
        public string levelofeducationName { get; set; }
        public string levelofeducationNameBn { get; set; }
    }
}
