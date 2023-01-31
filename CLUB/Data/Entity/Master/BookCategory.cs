using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class BookCategory:Base
    {
        [Required]
        public string bookCategoey { get; set; }
        public string bookCategoeyBn { get; set; }

        public string bookCategoryShortName { get; set; }
    }
}
