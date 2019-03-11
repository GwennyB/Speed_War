using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWar.Models.Interfaces
{
    public interface IUserManager
    {
        Task<User> GetUserAsync(string name);
    }
}
