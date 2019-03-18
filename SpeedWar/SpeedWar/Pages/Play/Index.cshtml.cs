using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpeedWar.Models;
using SpeedWar.Models.Interfaces;

namespace SpeedWar.Pages.Play
{
    public class IndexModel : PageModel
    {
        private IDeckCardManager _deckCardContext;
        private IUserManager _userContext;

        public User Player { get; set; }
        public bool GameStart { get; set; }

        public IndexModel(IDeckCardManager deckCardManager, IUserManager userManager)
        {
            _deckCardContext = deckCardManager;
            _userContext = userManager;
            GameStart = true;
        }

        public async Task OnGet(User player )
        {
            if (GameStart == true)
            {
                Player = player;
                await _userContext.UpdateFirstCard(player.Name, 53);
                await _userContext.UpdateSecondCard(player.Name, 54);
                await _deckCardContext.DealGameAsync(Player.ID);
            }
        }

        ///// <summary>
        ///// Takes in the player's user ID. Find's the first card in that player's deck. Changes that card's location to the discard pile. Updates the card. If card is null, calls EndGame method. 
        ///// </summary>
        ///// <param name="userID">the id of the user playing</param>
        //public async Task OnPost()
        //{

        //}


        //private void EndGame(string v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}