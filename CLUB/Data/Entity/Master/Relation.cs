using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class Relation:Base
    {
        [Required]
        public string relationName { get; set; }
        public string relationNameBn { get; set; }

        public string relationShortName { get; set; }
    }
}
