using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class Vehicle:Base
    {
        [Required]
        public string vehicleType { get; set; }
        public string vehicleTypeBn { get; set; }

        public string vehicleShortName { get; set; }
    }
}
