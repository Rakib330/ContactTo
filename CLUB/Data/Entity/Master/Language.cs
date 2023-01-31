using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class Language:Base
    {
        [Required]
        public string languageName { get; set; }
        public string languageNameBn { get; set; }

        public string languageShortName { get; set; }
    }
}
