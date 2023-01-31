using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CLUB.Areas.MasterData.Models
{
    public class TravelPurposeViewModel
    {
        public int travelPurposeId { get; set; }
        [Required]
        public string purposeName { get; set; }
        public string purposeNameBn { get; set; }

        public string purposeShortName { get; set; }

        public TravelPurposeLn fLang { get; set; }

        public IEnumerable<TravelPurpose> travelPurposes { get; set; }
    }
}
