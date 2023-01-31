using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class TrainingCategory:Base
    {
        [Required]
        public string trainingCategoryName { get; set; }
        public string trainingCategoryNameBn { get; set; }

        public string trainingCategoryShortName { get; set; }
    }
}
