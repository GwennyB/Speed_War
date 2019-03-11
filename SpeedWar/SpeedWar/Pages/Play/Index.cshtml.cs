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

        [BindProperty]
        public User Player { get; set; }
        public bool GameStart { get; set; }


        public IndexModel(IDeckCardManager deckCardManager)
        {
            _deckCardContext = deckCardManager;
            GameStart = true;
        }

        
        public async void OnGet()
        {
            if (GameStart == true)
            {
                //await _deckCardContext.DealGameAsync(Player.ID);
            }
        }

        /// <summary>
        /// Takes in the player's user ID. Find's the first card in that player's deck. Changes that card's location to the discard pile. Updates the card. If card is null, calls EndGame method. 
        /// </summary>
        /// <param name="userID">the id of the user playing</param>
        public async void OnPost()
        {
            GameStart = false;
            int userID = Player.ID;
            var check = await _deckCardContext.GetDeck(userID, DeckType.Play);
            if (check.Count == 0)
            {
                EndGame("Computer");
            }
            DeckCard deckCard = await _deckCardContext.GetCard(userID, DeckType.Play);
            if (deckCard != null)
            {
                deckCard.DeckID = 1;
                await _deckCardContext.UpdateDeckCard(deckCard);
            }
            else
            {
                EndGame("Computer");
            }
        }

        /// <summary>
        /// Find's the first card in the computer's deck. Changes that cards location to the discard pile. Updates the card. If card is null, calls EndGame method. 
        /// </summary>
        public async void ComputerFlip()
        {
            var check = await _deckCardContext.GetDeck(2, DeckType.Play);
            if (check.Count == 0)
            {
                EndGame("Player");
            }
            DeckCard deckCard = await _deckCardContext.GetCard(2, DeckType.Play);
            deckCard.DeckID = 1;
            await _deckCardContext.UpdateDeckCard(deckCard);
        }

        private void EndGame(string v)
        {
            throw new NotImplementedException();
        }
    }
}