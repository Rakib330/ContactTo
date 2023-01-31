using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CLUB.Areas.MasterData.Models
{
    public class MunicipilityLocationViewModel
    {
        public int municipalityId { get; set; }
        [Required]
        [Display(Name = "Municipility Location Name")]
        public string locationName { get; set; }
        public string locationNameBn { get; set; }

        public string shortName { get; set; }

        public MunicipalityInfoLn fLang { get; set; }

        public IEnumerable<MunicipilityLocation> municipilityLocations { get; set; }
    }
}
