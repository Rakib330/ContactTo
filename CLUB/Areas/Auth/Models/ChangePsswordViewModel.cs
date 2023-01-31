using CLUB.Data.Entity;
using CLUB.Data.Entity.Navbar;
using CLUB.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.Auth.Models
{
    public class ChangePsswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class GetAllUserViewModel
    {
        public string aspnetId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? empId { get; set; }
        public string nameEnglish { get; set; }
        public string roleId { get; set; }
    }
    public class OTPViewModel2
    {
        public string otpCode { get; set; }
        public string userName { get; set; }
        public ApplicationUser userInfo { get; set; }
    }
    public class OTPCodeVarificationViewModel
    {
        public string otpCode { get; set; }
        public int? isVarified { get; set; }
        //public OTPVarifiedLn oLang { get; set; }
    }
    public class OtpVerification2
    {
        public string userName { get; set; }
        public string otpCode { get; set; }
    }
}
