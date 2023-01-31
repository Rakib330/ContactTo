using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CLUB.Areas.MasterData.Models
{
    public class TrainingInstituteViewModel
    {
        public int trnInstituteId { get; set; }
        [Required]
        public string trainingInstituteName { get; set; }
        public string trainingInstituteNameBn { get; set; }

        public string trainingInstituteShortName { get; set; }

        public TrainigInstituteLn fLang { get; set; }

        public IEnumerable<TrainingInstitute> trainingInstitutes { get; set; }
    }
}
