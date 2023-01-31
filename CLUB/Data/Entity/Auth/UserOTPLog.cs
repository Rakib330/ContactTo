using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Data.Entity.Auth
{
    public class UserOTPLog:Base
    {
        public string username { get; set; }
        public string receiver { get; set; }
        public string otp { get; set; }
        public DateTime? expireDate { get; set; } = DateTime.Now.AddMinutes(5);
    }
}
