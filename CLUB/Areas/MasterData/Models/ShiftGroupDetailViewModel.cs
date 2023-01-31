using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CLUB.Areas.MasterData.Models
{
    public class ShiftGroupDetailViewModel
    {
        public int shiftMasterId { get; set; }
        [Required]
        public string weekDay { get; set; }
        [Required]
        public string startTime { get; set; }
        [Required]
        public string endTime { get; set; }

        public string holiday { get; set; }
        
        public int shiftGroupMasterId { get; set; }

        public ShiftGroupDetailsLn fLang { get; set; }

        public IEnumerable<ShiftGroupDetail> shiftGroupDetailslist { get; set; }

        public IEnumerable<ShiftGroupMaster> shiftGroupMasterslist { get; set; }
    }
}
