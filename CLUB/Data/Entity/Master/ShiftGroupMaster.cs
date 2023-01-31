using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class ShiftGroupMaster : Base
    {
        [Required]
        public string shiftName { get; set; }
        public string shiftNameBn { get; set; }
    }
}
