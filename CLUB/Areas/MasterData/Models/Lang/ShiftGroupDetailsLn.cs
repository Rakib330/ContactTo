using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.MasterData.Models.Lang
{
    public class ShiftGroupDetailsLn
    {
        public string title { get; set; }        
        public string shiftGroupMaster { get; set; }
        public string weekDay { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string isHoliday { get; set; }
        public string action { get; set; }
    }
}
