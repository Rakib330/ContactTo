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
    public class DrivingLicenseInfoService: IDrivingLicenseService
    {
        private readonly ApplicationDbContext _context;

        public DrivingLicenseInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveDrivingLicenseInfo(DrivingLicense drivingLicense)
        {

            if (drivingLicense.Id != 0)
                _context.drivingLicenses.Update(drivingLicense);
            else
                _context.drivingLicenses.Add(drivingLicense);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DrivingLicense>> GetDrivingLicenseInfo()
        {
            return await _context.drivingLicenses.AsNoTracking().ToListAsync();
        }

        public async Task<DrivingLicense> GetDrivingLicenseById(int id)
        {
            return await _context.drivingLicenses.FindAsync(id);
        }

        public async Task<bool> DeleteDrivingLicenseById(int id)
        {
            _context.drivingLicenses.Remove(_context.drivingLicenses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DrivingLicense>> GetDrivingLicenseByEmpId(int? empId)
        {
            return await _context.drivingLicenses.Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }
    }
}
