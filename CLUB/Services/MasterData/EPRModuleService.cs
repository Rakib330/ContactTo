using Club.Services.MasterData.Interfaces;
using CLUB.Data;
using CLUB.Data.Entity.Navbar;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Club.Services.MasterData
{
    public class EPRModuleService: IERPModuleService
    {

        private readonly ApplicationDbContext _context;

        public EPRModuleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveERPModule(Module eRPModule)
        {
            if (eRPModule.Id != 0)
                _context.modules.Update(eRPModule);
            else
                _context.modules.Add(eRPModule);
            await _context.SaveChangesAsync();
            return eRPModule.Id;
        }

        public async Task<IEnumerable<Module>> GetAllERPModule()
        {
            return await _context.modules.ToListAsync();
        }

        public async Task<Module> GetERPModuleById(int id)
        {
            return await _context.modules.FindAsync(id);
        }

        public async Task<Module> GetERPModuleByModuleName(string moduleName)
        {
            return await _context.modules.Where(x => x.moduleName == moduleName).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteERPModuleById(int id)
        {
            _context.modules.Remove(_context.modules.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
