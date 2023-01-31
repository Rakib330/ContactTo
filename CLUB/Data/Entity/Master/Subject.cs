using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class Subject:Base
    {
        [Required]
        public string subjectName { get; set; }
        public string subjectNameBn { get; set; }

        public string subjectShortName { get; set; }
    }
}
