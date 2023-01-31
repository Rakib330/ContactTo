using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CLUB.Areas.MasterData.Models
{
    public class SubjectViewModel
    {
        public int subjectId { get; set; }
        [Required]
        public string subjectName { get; set; }
        public string subjectNameBn { get; set; }
        
        public string subjectShortName { get; set; }   
        
        public SubjectInfoLn fLang { get; set; }

        public IEnumerable<Subject> subjects { get; set; }
    }
}
