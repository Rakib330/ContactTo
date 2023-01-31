using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CLUB.Areas.MasterData.Models
{
    public class ThanaViewModel
    {    
        public int thanaId { get; set; }
        [Display(Name = "Country")]
        public int countryId { get; set; }

        [Display(Name = "Division")]
        public int divisionId { get; set; }

        [Display(Name = "District")]
        public int districtId { get; set; }

        [Required]
        [Display(Name = "Thana Code")]
        public string thanaCode { get; set; }

        [Required]
        [Display(Name = "Thana Name")]
        public string thanaName { get; set; }
        public string thanaNameBn { get; set; }

        public string shortName { get; set; }

        public ThanaInfoLn fLang { get; set; }

        public IEnumerable<Thana> thanas { get; set; }
        public IEnumerable<District> districts { get; set; }
        public IEnumerable<Division> divisions { get; set; }
        public IEnumerable<Country> countries { get; set; }
    }
}
