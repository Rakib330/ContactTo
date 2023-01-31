using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class SalaryGrade:Base
    {
        [Required]
        public string gradeName { get; set; }

        [Required]
        public decimal basicAmount { get; set; }

        [Required]
        public string payScale { get; set; }

        [Required]
        public decimal currentBasic { get; set; }
    }
}
