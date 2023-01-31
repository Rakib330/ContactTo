using CLUB.Data;
using CLUB.Data.Entity.Bulk;
using CLUB.Services.Bulk.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.Bulk
{
    public class GroupService: IGroupService
    {
        private readonly ApplicationDbContext _context;

        public GroupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveMemberGroup(MemberGroup memberGroup)
        {
            if (memberGroup.Id != 0)
                _context.memberGroups.Update(memberGroup);
            else
                _context.memberGroups.Add(memberGroup);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MemberGroup>> GetMemberGroup()
        {
            return await _context.memberGroups.AsNoTracking().ToListAsync();
        }

        public async Task<MemberGroup> GetMemberGroupById(int id)
        {
            return await _context.memberGroups.FindAsync(id);
        }

        public async Task<bool> DeleteMemberGroupById(int id)
        {
            _context.memberGroups.Remove(_context.memberGroups.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }







        //Receiver
        public async Task<int> SaveReceiver(Receiver Receiver)
        {
            if (Receiver.Id != 0)
                _context.receivers.Update(Receiver);
            else
                _context.receivers.Add(Receiver);
            await _context.SaveChangesAsync();
            return Receiver.Id;
        }

        public async Task<IEnumerable<Receiver>> GetReceiver()
        {
            return await _context.receivers.AsNoTracking().ToListAsync();
        }

        public async Task<Receiver> GetReceiverById(int id)
        {
            return await _context.receivers.FindAsync(id);
        }

        public async Task<bool> DeleteReceiverById(int id)
        {
            var data = _context.rlnreceiverGroups.Where(x => x.receiverId == id).ToList();
            foreach(var sar in data)
            {
                _context.rlnreceiverGroups.Remove(sar);
            }
            _context.receivers.Remove(_context.receivers.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }










        //Receiver Group
        public async Task<int> SaveReceiverGroup(ReceiverGroup ReceiverGroup)
        {
            if (ReceiverGroup.Id != 0)
                _context.receiverGroups.Update(ReceiverGroup);
            else
                _context.receiverGroups.Add(ReceiverGroup);
            await _context.SaveChangesAsync();
            return ReceiverGroup.Id;
        }

        public async Task<IEnumerable<ReceiverGroup>> GetReceiverGroup()
        {
            return await _context.receiverGroups.AsNoTracking().ToListAsync();
        }

        public async Task<ReceiverGroup> GetReceiverGroupById(int id)
        {
            return await _context.receiverGroups.FindAsync(id);
        }

        public async Task<bool> DeleteReceiverGroupById(int id)
        {
            _context.receiverGroups.Remove(_context.receiverGroups.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

    }
}
