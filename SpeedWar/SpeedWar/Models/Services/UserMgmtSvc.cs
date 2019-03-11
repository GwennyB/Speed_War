using SpeedWar.Data;
using SpeedWar.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWar.Models.Services
{
    public class UserMgmtSvc : IUserManager
    {
        private CardDbContext _context { get; }

        public UserMgmtSvc(CardDbContext context)
        {
            _context = context;
        }



    }
}
