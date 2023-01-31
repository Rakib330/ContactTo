using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class Designation:Base
    {
        [Required]
        public string designationCode { get; set; }

        [Required]
        public string designationName { get; set; }

        public string designationNameBN { get; set; }
  
        public string shortName { get; set; }

        //public int? empType { get; set; }
    }
}
