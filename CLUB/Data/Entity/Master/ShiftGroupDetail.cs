using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class ShiftGroupDetail : Base
    {       
        
        [Required]
        public string weekDay { get; set; }
        [Required]
        public string startTime { get; set; }
        [Required]
        public string endTime { get; set; }
        
        public bool holiday { get; set; }

        public int shiftGroupMasterId { get; set; }

        public ShiftGroupMaster shiftGroupMaster { get; set; }
    }
}
