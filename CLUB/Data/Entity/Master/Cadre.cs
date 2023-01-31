using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class Cadre:Base
    {
        [Required]
        public string cadreName { get; set; }
        public string cadreNameBn { get; set; }

        public string cadreShortName { get; set; }
    }
}
