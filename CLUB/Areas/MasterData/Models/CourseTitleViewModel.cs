using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.MasterData.Models
{
    public class CourseTitleViewModel
    {
        public string courseTitleId { get; set; }
        [Required]
        public string courseTitle { get; set; }

        public CourseTitleLn fLang { get; set; }

        public IEnumerable<CourseTitle> courseTitles { get; set; }
    }
}
