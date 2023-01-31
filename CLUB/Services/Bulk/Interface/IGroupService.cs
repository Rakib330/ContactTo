using CLUB.Data.Entity.Bulk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.Bulk.Interface
{
   public interface IGroupService
    {
        Task<bool> SaveMemberGroup(MemberGroup memberGroup);
        Task<IEnumerable<MemberGroup>> GetMemberGroup();
        Task<MemberGroup> GetMemberGroupById(int id);
        Task<bool> DeleteMemberGroupById(int id);



        Task<int> SaveReceiver(Receiver Receiver);
        Task<IEnumerable<Receiver>> GetReceiver();
        Task<Receiver> GetReceiverById(int id);
        Task<bool> DeleteReceiverById(int id);





        Task<int> SaveReceiverGroup(ReceiverGroup ReceiverGroup);
        Task<IEnumerable<ReceiverGroup>> GetReceiverGroup();
        Task<ReceiverGroup> GetReceiverGroupById(int id);
        Task<bool> DeleteReceiverGroupById(int id);
    }
}
