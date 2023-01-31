using CLUB.Areas.Employee.Models.Lang;
using CLUB.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CLUB.Areas.MasterData.Models
{
    public class LanguageViewModel
    {
        public int languageId { get; set; }
        [Required]
        public string languageName { get; set; }
        public string languageNameBn { get; set; }
        
        public string languageShortName { get; set; }

        public LanguageLn fLang { get; set; }

        public IEnumerable<Language> languages { get; set; }
    }
}
