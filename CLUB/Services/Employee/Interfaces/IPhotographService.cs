using CLUB.Data.Entity.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CLUB.Services.Employee.Interfaces
{
    public interface IPhotographService
    {
        Task<bool> SavePhotograph(Photograph photograph);
        Task<IEnumerable<Photograph>> GetPhotographs();
        Task<Photograph> GetPhotographById(int id);
        Task<bool> DeletePhotographById(int id);
        Task<Photograph> GetPhotographByEmpIdAndType(int? empId, string type);
    }
}
