using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class LevelofEducationViewModel
{
    public int loeId { get; set; }
    [Required]
    public string levelofeducationName { get; set; }
    public string levelofeducationNameBn { get; set; }

    public LevelOfEducationLn fLang { get; set; }

    public IEnumerable<LevelofEducation> levelofEducations { get; set; }

    public IEnumerable<Degree> degrees { get; set; }
}

