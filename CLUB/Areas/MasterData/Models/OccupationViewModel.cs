using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CLUB.Areas.MasterData.Models
{
    public class OccupationViewModel
    {
        public int occupationId { get; set; }
        [Required]
        public string occupationName { get; set; }
        public string occupationNameBn { get; set; }
        
        public string occupationShortName { get; set; }

        public OccupationLn fLang { get; set; }

        public IEnumerable<Occupation> occupations { get; set; }
    }
}
