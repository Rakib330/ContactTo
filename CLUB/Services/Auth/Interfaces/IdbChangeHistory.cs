
using CLUB.Data.Entity.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.Auth.Interfaces
{
    public interface IdbChangeHistory
    {
        Task<int> SaveUserLogHistory(UserLogHistory userLogHistory);
    }
}
