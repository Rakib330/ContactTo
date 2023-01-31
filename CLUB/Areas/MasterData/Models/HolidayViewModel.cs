using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.MasterData.Models
{
    public class HolidayViewModel
    {
        [Required]
        public string holidayName { get; set; }
        public string holidayNameBn { get; set; }

        [Required]
        public string weeklyHoliday { get; set; }

        [Required]
        public string year { get; set; }

        public IEnumerable<Holiday> holidays { get; set; }
    }
}
