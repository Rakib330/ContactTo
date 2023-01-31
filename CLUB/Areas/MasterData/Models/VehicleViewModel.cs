using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CLUB.Areas.MasterData.Models
{
    public class VehicleViewModel
    {
        public int vehicleId { get; set; }
        [Required]
        [Display(Name = "Vehicle Type")]
        public string vehicleType { get; set; }
        public string vehicleTypeBn { get; set; }

        public string vehicleShortName { get; set; }

        public VehicleInfoLn fLang { get; set; }
        public IEnumerable<Vehicle> vehicles { get; set; }
    }
}
