using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class Result : Base
    {
        [Required]
        public string resultName { get; set; }
        public string resultNameBn { get; set; }
        
        public string resultShortName { get; set; }
        [Required]
        public decimal resultMaxValue { get; set; }
    }
}
