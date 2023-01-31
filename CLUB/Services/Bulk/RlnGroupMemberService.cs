using CLUB.Data;
using CLUB.Data.Entity.Bulk;
using CLUB.Data.Entity.Employee;
using CLUB.Services.Bulk.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.Bulk
{
    public class RlnGroupMemberService: IRlnGroupMemberService
    {
        private readonly ApplicationDbContext _context;

        public RlnGroupMemberService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveRlnMemberGroup(RlnMemberGroup rlnMemberGroup)
        {
            if (rlnMemberGroup.Id != 0)
                _context.rlnMemberGroups.Update(rlnMemberGroup);
            else
                _context.rlnMemberGroups.Add(rlnMemberGroup);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RlnMemberGroup>> GetRlnMemberGroup()
        {
            return await _context.rlnMemberGroups.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RlnMemberGroup>> GetRlnMemberGroupByGroupId(int groupId)
        {
            return await _context.rlnMemberGroups.Where(x=>x.memberGroupId== groupId).Include(x=>x.memberGroup).Include(x=>x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EmployeeInfo>> GetEmployeeInfoByGroupId(int groupId)
        {
            List<int?> ids = await _context.rlnMemberGroups.Where(x => x.memberGroupId == groupId).Select(x=>x.employeeId).ToListAsync();
            return await _context.employeeInfos.Where(x=> !ids.Contains(x.Id)).AsNoTracking().ToListAsync();
            //return await _context.employeeInfos.Where(x => x.orgType == "ministry").Where(x=> !ids.Contains(x.Id)).AsNoTracking().ToListAsync();
        }

        public async Task<RlnMemberGroup> GetRlnMemberGroupById(int id)
        {
            return await _context.rlnMemberGroups.FindAsync(id);
        }

        public async Task<bool> DeleteRlnMemberGroupById(int id)
        {
            _context.rlnMemberGroups.Remove(_context.rlnMemberGroups.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }














        public async Task<bool> SaveRlnReceiverGroup(RlnReceiverGroup rlnReceiverGroup)
        {
            if (rlnReceiverGroup.Id != 0)
                _context.rlnreceiverGroups.Update(rlnReceiverGroup);
            else
                _context.rlnreceiverGroups.Add(rlnReceiverGroup);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RlnReceiverGroup>> GetRlnReceiverGroup()
        {
            return await _context.rlnreceiverGroups.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RlnReceiverGroup>> GetRlnReceiverGroupByGroupId(int groupId)
        {
            return await _context.rlnreceiverGroups.Where(x => x.receiverGroupId == groupId).Include(x => x.receiverGroup).AsNoTracking().ToListAsync();
        }

        public async Task<RlnReceiverGroup> GetRlnReceiverGroupById(int id)
        {
            return await _context.rlnreceiverGroups.FindAsync(id);
        }

        public async Task<bool> DeleteRlnReceiverGroupById(int id)
        {
            _context.rlnreceiverGroups.Remove(_context.rlnreceiverGroups.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
