using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class EmployeeType : Base
    {
        [Required]
        public string empType { get; set; }
        public string empTypeBn { get; set; }
        public decimal? charge { get; set; }

        public string shortName { get; set; }
    }
}
