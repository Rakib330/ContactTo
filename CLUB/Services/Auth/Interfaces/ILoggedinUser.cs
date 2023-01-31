using CLUB.Areas.Auth.Models;
using CLUB.Models.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CLUB.Services.Auth.Interfaces
{
    public interface ILoggedinUser
    {
        Task<string>usersOrganization(string name);
        Task<List<string>> UsersRoles(string name);
        Task<int> UserEmpId(string name);
        int UserAuthId(string id);
        string UserAuthUrl(int id);
        Task<IEnumerable<GetAllUserViewModel>> GetAllUserInfos();
        Task<string> GetRoleNatureByUserId(string userName);
        Task<IEnumerable<GetAllUserViewNewModel>> GetAllUserInfosForProxyUser(string userName, string rolename);
    }
}
