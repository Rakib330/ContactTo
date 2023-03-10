using CLUB.Data;
using CLUB.Data.Entity.Employee;
using CLUB.Services.Employee.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CLUB.Services.Employee
{
    public class MembershipService : IMembershipService
    {
        private readonly ApplicationDbContext _context;

        public MembershipService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteMembershipInfoById(int id)
        {
            _context.employeeMemberships.Remove(_context.employeeMemberships.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeMembership>> GetMembershipInfo()
        {
            return await _context.employeeMemberships.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EmployeeMembership>> GetMembershipInfoByEmpId(int? empId)
        {
            return await _context.employeeMemberships.Where(x => x.employeeId == empId).Include(x => x.membership).AsNoTracking().ToListAsync();
        }

        public async Task<EmployeeMembership> GetMembershipInfoById(int id)
        {
            return await _context.employeeMemberships.FindAsync(id);
        }

        public async Task<bool> SaveMembershipInfo(EmployeeMembership employeeMembership)
        {
            if (employeeMembership.Id != 0)
                _context.employeeMemberships.Update(employeeMembership);
            else
                _context.employeeMemberships.Add(employeeMembership);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
