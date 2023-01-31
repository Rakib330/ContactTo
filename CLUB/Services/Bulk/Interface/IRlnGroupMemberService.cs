using CLUB.Data.Entity.Bulk;
using CLUB.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.Bulk.Interface
{
   public interface IRlnGroupMemberService
    {
        Task<bool> SaveRlnMemberGroup(RlnMemberGroup rlnMemberGroup);
        Task<IEnumerable<RlnMemberGroup>> GetRlnMemberGroup();
        Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoByGroupId(int groupId);
        Task<IEnumerable<RlnMemberGroup>> GetRlnMemberGroupByGroupId(int groupId);
        Task<RlnMemberGroup> GetRlnMemberGroupById(int id);
        Task<bool> DeleteRlnMemberGroupById(int id);






        Task<bool> SaveRlnReceiverGroup(RlnReceiverGroup rlnReceiverGroup);
        Task<IEnumerable<RlnReceiverGroup>> GetRlnReceiverGroup();
        Task<IEnumerable<RlnReceiverGroup>> GetRlnReceiverGroupByGroupId(int groupId);
        Task<RlnReceiverGroup> GetRlnReceiverGroupById(int id);
        Task<bool> DeleteRlnReceiverGroupById(int id);
    }
}
