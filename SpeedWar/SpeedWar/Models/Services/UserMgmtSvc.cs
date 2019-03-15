using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// gets and returns user by username
        /// if no user exists with submitted username, creates new user
        /// </summary>
        /// <param name="name"> name entered by user </param>
        /// <returns> user associated with submitted name </returns>
        public async Task<User> GetUserAsync(string name)
        {
            if (name == null)
            {
                name = "Default";
            }
            User user = await _context.Users.FirstOrDefaultAsync(s => s.Name.ToLower() == name.ToLower());

            if (user == null)
            {
                user = await CreateUserAsync(name);
            }

            return user;
        }


        /// <summary>
        /// ***SUPPORTS GetUserAsync()***
        /// Creates new user and builds user's decks
        /// </summary>
        /// <param name="name"> name entered by user </param>
        /// <returns> new user created with submitted name </returns>
        private async Task<User> CreateUserAsync(string name)
        {
            await _context.Users.AddAsync(new User() { Name = name });
            await _context.SaveChangesAsync();
            User user = await _context.Users.FirstOrDefaultAsync(s => s.Name.ToLower() == name.ToLower());
            Deck play = new Deck() { UserID = user.ID, DeckType = DeckType.Play };
            Deck collect = new Deck() { UserID = user.ID, DeckType = DeckType.Collect };
            await _context.Decks.AddAsync(play);
            await _context.Decks.AddAsync(collect);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Card> GetFirstCard(string username)
        {

            var query = await _context.Users.FirstOrDefaultAsync(u => u.Name == username);
            int cardID = query.FirstCard;
            var card = await _context.Cards.FirstOrDefaultAsync(c => c.ID == cardID);
            return card;
        }

        public async Task<Card> GetSecondCard(string username)
        {
            var query = await _context.Users.FirstOrDefaultAsync(u => u.Name.ToLower() == username.ToLower());
            var card = await _context.Cards.FirstOrDefaultAsync(c => c.ID == query.SecondCard);
            return card;
        }

        public async Task<bool> GetPlayerTurn(string username)
        {
            return (await _context.Users.FirstOrDefaultAsync(u => u.Name.ToLower() == username.ToLower())).PlayerTurn;
        }

        public async Task UpdateFirstCard(string username, int cardID)
        {
            var query = await _context.Users.FirstOrDefaultAsync(u => u.Name.ToLower() == username.ToLower());
            query.FirstCard = cardID;
            _context.Users.Update(query);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSecondCard(string username, int cardID)
        {
            var query = await _context.Users.FirstOrDefaultAsync(u => u.Name.ToLower() == username.ToLower());
            query.SecondCard = cardID;
            _context.Users.Update(query);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePlayerTurn(string username, bool turn)
        {
            var query = await _context.Users.FirstOrDefaultAsync(u => u.Name.ToLower() == username.ToLower());
            query.PlayerTurn = turn;
            _context.Users.Update(query);
            await _context.SaveChangesAsync();
        }
    }
}
