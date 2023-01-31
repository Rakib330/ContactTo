using CLUB.Data;
using CLUB.Data.Entity.Employee;
using CLUB.Services.Employee.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.Employee
{
    public class PassportInfoService: IPassportInfoService
    {
        private readonly ApplicationDbContext _context;

        public PassportInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SavePassportInfo(PassportDetails passportDetail)
        {
            if (passportDetail.Id != 0)
                _context.passportDetails.Update(passportDetail);
            else
                _context.passportDetails.Add(passportDetail);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PassportDetails>> GetPassportInfo()
        {
            return await _context.passportDetails.AsNoTracking().ToListAsync();
        }

        public async Task<PassportDetails> GetPassportInfoById(int id)
        {
            return await _context.passportDetails.FindAsync(id);
        }

        public async Task<bool> DeletePassportInfoById(int id)
        {
            _context.passportDetails.Remove(_context.passportDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PassportDetails>> GetPassportInfoByEmpId(int? empId)
        {
            return await _context.passportDetails.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }
    }
}
