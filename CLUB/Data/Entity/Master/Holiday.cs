using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class Holiday : Base
    {
        [Required]
        public string weeklyHoliday { get; set; }
        [Required]
        public string holidayName { get; set; }
        public string holidayNameBn { get; set; }
        [Required]
        public int year { get; set; }
    }
}
