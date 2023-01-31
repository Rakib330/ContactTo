using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class OrganizationViewModel
{
    public int organizationId { get; set; }
    [Required]
    public string organizationName { get; set; }
    public string organizationNameBn { get; set; }

    [Required]
    public string organizationType { get; set; }

    public OrganizationLn fLang { get; set; }

    public IEnumerable<Organization> organizations { get; set; }
}

