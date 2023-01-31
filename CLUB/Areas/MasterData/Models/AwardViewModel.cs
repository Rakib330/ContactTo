using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CLUB.Areas.MasterData.Models
{
    public class AwardViewModel
    {
        [Required]
        public string awardId { get; set; }
        public string awardName { get; set; }
        public string awardNameBn { get; set; }
        
        public string awardShortName { get; set; }

        public AwardLn fLang { get; set; }

        public IEnumerable<Award> awards { get; set; }
    }
}
