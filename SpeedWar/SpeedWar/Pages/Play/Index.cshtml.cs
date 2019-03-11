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
        

        public IndexModel(IDeckCardManager deckCardManager)
        {
            _deckCardContext = deckCardManager;
        }


        public void OnGet()
        {
        }
        
        /// <summary>
        /// Takes in the player's user ID. Find's the first card in that player's deck. Changes that card's location to the discard pile. Updates the card.
        /// </summary>
        /// <param name="userID">the id of the user playing</param>
        public async void OnPostFlip(int userID)
        {
            DeckCard deckCard = await _deckCardContext.GetCard(userID);
            deckCard.DeckID = 1;
            await _deckCardContext.UpdateDeckCard(deckCard);
        }
    }
}