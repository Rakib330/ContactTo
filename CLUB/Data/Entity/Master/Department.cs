using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class Department:Base
    {
        [Required]
        public string deptCode { get; set; }

        [Required]
        public string deptName { get; set; }
        public string deptNameBn { get; set; }

        public string shortName { get; set; }

    }
}
