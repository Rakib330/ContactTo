using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.MasterData.Models
{
    public class SpacialSkilViewModel
    {
        [Required]
        public string skilId { get; set; }
        public string skilName { get; set; }
        public string skilNameBn { get; set; }

        public string shortName { get; set; }

        public SpacialSkilLn fLang { get; set; }

        public IEnumerable<SpacialSkill> spacialSkills { get; set; }
    }
}
