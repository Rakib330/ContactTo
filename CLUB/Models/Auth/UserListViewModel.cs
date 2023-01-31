using CLUB.Areas.Auth.Models;
using CLUB.Data.Entity;
using CLUB.Data.Entity.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Models.Auth
{
    public class UserListViewModel
    {
        //public IEnumerable<AspNetUsersViewModel> aspNetUsersViewModels { get; set; }
        public IEnumerable<ApplicationRoleViewModel> userRoles { get; set; }
        public IEnumerable<UserBackup> userBackups { get; set; }
        //public IEnumerable<UserBackUpViewModel> userBackUpViewModels { get; set; }
        public IEnumerable<ApplicationUser> aspnetUsers { get; set; }
        public IEnumerable<GetAllUserViewModel> aspNetUsersViewModels { get; set; }

        public IEnumerable<GetAllUserViewNewModel> aspNetUsersViewNewModels { get; set; }

        public ApplicationUser userInfo { get; set; }
        public string userId { get; set; }
		public string securityCode { get; set; }
        public IEnumerable<ApplicationRoleViewModel> userRolesbynew { get; set; }
    }

	//public class GetAllUserViewModel
 //   {
 //       public string aspnetId { get; set; }
 //       public string UserName { get; set; }
 //       public int? UserTypeId { get; set; }
 //       public string Email { get; set; }
 //       public string branchName { get; set; }
 //       public int? empId { get; set; }
 //       public string firstName { get; set; }
 //       public string lastName { get; set; }
 //       public string roleId { get; set; }
 //   }
    public class ClearDataVm
    {
        public int? status { get; set; }
    }
    public class GetAllUserViewNewModel
    {
        public string aspnetId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? empId { get; set; }
        public string nameEnglish { get; set; }
        public string roleId { get; set; }
        public string employeeCode { get; set; }
        public string phone { get; set; }

    }
}
