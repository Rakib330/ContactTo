using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CLUB.Areas.MasterData.Models
{
    public class ReligionViewModel
    {
        public int religionId { get; set; }
        [Required]
        [Display(Name = "Religion Name")]
        public string name { get; set; }
        public string nameBn { get; set; }

        public string shortName { get; set; }

        public ReligionLn fLang { get; set; }

        public IEnumerable<Religion> religions { get; set; }
    }
}
