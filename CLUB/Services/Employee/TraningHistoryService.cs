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
    public class TraningHistoryService : ITraningHistoryService
    {
        private readonly ApplicationDbContext _context;

        public TraningHistoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteTraningHistoryById(int id)
        {
            _context.traningLogs.Remove(_context.traningLogs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TraningLog>> GetTraningHistory()
        {
            return await _context.traningLogs.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TraningLog>> GetTraningHistoryByEmpId(int? empId)
        {
            return await _context.traningLogs.Where(x => x.employeeId == empId).Include(x => x.employee).Include(x => x.country).Include(x => x.trainingCategory).Include(x => x.trainingInstitute).AsNoTracking().ToListAsync();
        }

        public async Task<TraningLog> GetTraningHistoryById(int id)
        {
            return await _context.traningLogs.FindAsync(id);
        }

        public async Task<bool> SaveTraningHistory(TraningLog traningLog)
        {
            if (traningLog.Id != 0)
                _context.traningLogs.Update(traningLog);
            else
                _context.traningLogs.Add(traningLog);

            return 1 == await _context.SaveChangesAsync();
        }

        //Wahid 
        public async Task<IEnumerable<TraningLog>> GetTraningHistoryListById(int? empId)
        {
            return await _context.traningLogs.AsNoTracking().Where(x=>x.employeeId==empId).ToListAsync();
        }
        //Wahid
    }
}
