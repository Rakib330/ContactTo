using CLUB.Areas.Employee.Models.Lang;
using CLUB.Data.Entity.Employee;
using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CLUB.Areas.Employee.Models
{
    public class EmployeeInfoViewModel
    {
        [Required]
        [Display(Name = "Emp ID")]
        public string employeeID { get; set; }

        [Required]
        [Display(Name = "Employee Code")]
        public string employeeCode { get; set; }

        [Required]
        [Display(Name = "National ID")]
        public string nationalID { get; set; }

        [Display(Name = "Birth Identification No")]
        public string birthIdentificationNo { get; set; }

        [Display(Name = "Govt. ID")]
        public string govtID { get; set; }

        [Display(Name = "GPF Nominee Name")]
        public string gpfNomineeName { get; set; }

        [Display(Name = "GPF Acc No")]
        public string gpfAcNo { get; set; }

        [Required]
        [Display(Name = "Name In English")]
        public string nameEnglish { get; set; }
        
        [Display(Name = "Name In Bangla")]
        public string nameBangla { get; set; }

        [Required]
        [Display(Name = "Mother Name In English")]
        public string motherNameEnglish { get; set; }
        
        [Display(Name = "Mother Name In Bangla")]
        public string motherNameBangla { get; set; }

        [Required]
        [Display(Name = "Father Name In English")]
        public string fatherNameEnglish { get; set; }
        
        [Display(Name = "Father Name In Bangla")]
        public string fatherNameBangla { get; set; }

        [Required]
        [Display(Name = "Nationality")]
        public string nationality { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime? dateOfBirth { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Required]
        [Display(Name = "Place of Birth")]
        public string birthPlace { get; set; }

        [Required]
        [Display(Name = "Marital Status")]
        public string maritalStatus { get; set; }

        [Required]
        [Display(Name = "Religion")]
        public string religion { get; set; }
        
        [Required]
        [Display(Name = "Employee Type")]
        public string employeeType { get; set; }
        
        [Required]
        [Display(Name = "Date Of Membership")]
        public DateTime? dateOfMembership { get; set; }

        [Display(Name = "Blood Group")]
        public string bloodGroup { get; set; }

        [Display(Name = "Freedom Fighter")]
        public string freedomFighter { get; set; }

        [Display(Name = "Freedom Fighter No")]
        public string freedomFighterNo { get; set; }
        
        [Display(Name = "Telephone (Office)")]
        public string telephoneOffice { get; set; }

        [Display(Name = "Telephone (Residence)")]
        public string telephoneResidence { get; set; }

        [Display(Name = "Email Address")]
        public string emailAddress { get; set; }

        [Required]
        [Display(Name = "Mobile Number (Office)")]
        public string mobileNumberOffice { get; set; }

        [Display(Name = "Mobile Number (Personal)")]
        public string mobileNumberPersonal { get; set; }

        public string emailAddressPersonal { get; set; }

        public int[] skills { get; set; }

        public string homeDistrict { get; set; }
        public string natureOfRequitment { get; set; }
        public string specialSkill { get; set; }
        public string seniorityNumber { get; set; }
        public string joiningDesignation { get; set; }
        public string designation { get; set; }
        public string tShirtSize { get; set; }
        public string action { get; set; }

        public EmployeeInfoLn fLang { get; set; }
        public ACRLn fLang4 { get; set; }

        public int? post { get; set; }//Hidden Field For Designation, Handle.
        public string employeeNameCode { get; set; }

        public EmployeeInfo employeeInfo { get; set; }
        public IEnumerable<EmployeeInfo> allEmployeeInfos { get; set; }
        public IEnumerable<Religion> religions { get; set; }
        public IEnumerable<EmployeeType> employeeTypes { get; set; }
        public IEnumerable<Designation> designations { get; set; }
        public IEnumerable<Division> divisions { get; set; }
        public IEnumerable<District> districts { get; set; }
        public IEnumerable<Thana> thanas { get; set; }
        public IEnumerable<Degree> degrees { get; set; }
        public IEnumerable<Subject> subjects { get; set; }
        public IEnumerable<Organization> organizations { get; set; }
        public IEnumerable<SpacialSkill> spacialSkills { get; set; }
    }
}
