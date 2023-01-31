using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.MasterData.Models
{
    public class BookCategoryViewModel
    {
        public string bookCategoryId { get; set; }
        [Required]
        public string bookCategoey { get; set; }

        public string bookCategoeyBn { get; set; }
        
        public string bookCategoryShortName { get; set; }

        public BookCategoryLn fLang { get; set; }

        public IEnumerable<BookCategory> bookCategories { get; set; }
    }
}
