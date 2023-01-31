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
    public class PhotographService : IPhotographService
    {
        private readonly ApplicationDbContext _context;

        public PhotographService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeletePhotographById(int id)
        {
            _context.photographs.Remove(_context.photographs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<Photograph> GetPhotographByEmpIdAndType(int? empId, string type)
        {
            return await _context.photographs.Where(x => x.type == type && x.employeeId == empId).FirstOrDefaultAsync();
        }

        public async Task<Photograph> GetPhotographById(int id)
        {
            return await _context.photographs.FindAsync(id);
        }

        public async Task<IEnumerable<Photograph>> GetPhotographs()
        {
            return await _context.photographs.AsNoTracking().ToListAsync();
        }

        public async Task<bool> SavePhotograph(Photograph photograph)
        {
            if (photograph.Id != 0)
                _context.photographs.Update(photograph);
            else
                _context.photographs.Add(photograph);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
