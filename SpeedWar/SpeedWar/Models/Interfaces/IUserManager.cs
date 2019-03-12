using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWar.Models.Interfaces
{
    public interface IUserManager
    {
        string CurrentUserName { get; set; }
        Task<User> GetUserAsync(string name);
    }
}
