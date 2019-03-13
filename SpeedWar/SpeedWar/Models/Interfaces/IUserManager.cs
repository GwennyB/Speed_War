using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWar.Models.Interfaces
{
    public interface IUserManager
    {
        Task<User> GetUserAsync(string name);

        Task<Card> GetFirstCard(string username);
        Task<Card> GetSecondCard(string username);
        Task<bool> GetPlayerTurn(string username);
        Task<Card> UpdateFirstCard(string username);
        Task<Card> UpdateSecondCard(string username);
        Task<bool> UpdatePlayerTurn(string username);
    }
}
