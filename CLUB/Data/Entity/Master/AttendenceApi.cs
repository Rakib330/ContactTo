using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class AttendenceApi : Base
    {
        
        public string empCode { get; set; }

        public string entryDate { get; set; }

        public int status { get; set; }
    }
}
