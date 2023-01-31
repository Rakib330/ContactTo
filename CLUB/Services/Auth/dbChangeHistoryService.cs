
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data;
using CLUB.Data.Entity.Auth;
using CLUB.Services.Auth.Interfaces;

namespace CLUB.Services.Auth
{
    public class dbChangeHistoryService: IdbChangeHistory
    {
        private readonly ApplicationDbContext _context;

        public dbChangeHistoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveUserLogHistory(UserLogHistory userLogHistory)
        {
            try
            {
                if (userLogHistory.Id != 0)
                {
                    _context.userLogHistories.Update(userLogHistory);
                }
                else
                {
                    _context.userLogHistories.Add(userLogHistory);
                }

                await _context.SaveChangesAsync();
                return userLogHistory.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
