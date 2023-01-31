using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class TrainingInstitute:Base
    {
        [Required]
        public string trainingInstituteName { get; set; }
        public string trainingInstituteNameBn { get; set; }

        public string trainingInstituteShortName { get; set; }
    }
}
