using CLUB.Data.Entity.Navbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Club.Services.MasterData.Interfaces
{
    public interface IERPModuleService
    {
        Task<int> SaveERPModule(Module eRPModule);
        Task<IEnumerable<Module>> GetAllERPModule();
        Task<Module> GetERPModuleById(int id);
        Task<Module> GetERPModuleByModuleName(string moduleName);
        Task<bool> DeleteERPModuleById(int id);
    }
}
