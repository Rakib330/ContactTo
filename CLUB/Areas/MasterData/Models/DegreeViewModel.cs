using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class DegreeViewModel
{
    public string degreeId { get; set; }
    [Required]
    public string degreeName { get; set; }
    public string degreeNameBn { get; set; }
    
    public string degreeShortName { get; set; }
    [Required]
    public int levelofeducationId { get; set; }

    public DegreeInfoLn fLang { get; set; }

    public IEnumerable<Degree> degrees { get; set; }

    public IEnumerable<LevelofEducation> levelofeducationNamelist { get; set; }
}

