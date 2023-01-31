using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Master
{
    public class EmpAttendenceTemp : Base
    {
        
        public string cardNo { get; set; }

        public string currentDateTime { get; set; }

        public string time { get; set; }

        public string date { get; set; }

        public string timeOut { get; set; }
    }
}
