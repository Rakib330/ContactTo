using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using CLUB.Data.Entity.Master;
using CLUB.Areas.MasterData.Models.Lang;

namespace CLUB.Areas.MasterData.Models
{
    public class TrainingCategoryViewModel
    {
        public int trnCategoryId { get; set; }
        [Required]
        public string trainingCategoryName { get; set; }
        public string trainingCategoryNameBn { get; set; }
        
        public string trainingCategoryShortName { get; set; }

        public TrainingCategoryLn fLang { get; set; }

        public IEnumerable<TrainingCategory> trainingCategories { get; set; }
    }
}
