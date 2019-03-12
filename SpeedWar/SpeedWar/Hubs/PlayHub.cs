using Microsoft.AspNetCore.SignalR;
using SpeedWar.Models;
using SpeedWar.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWar.Hubs
{
    public class PlayHub : Hub
    {
        private IDeckCardManager _deckCardManager;

        public bool PlayerTurn { get; set; }
        public User CurrentUser { get; set; }
        public DeckCard FirstCard { get; set; }
        public DeckCard SecondCard { get; set; }

        public PlayHub(IDeckCardManager deckCardManager, User player)
        {
            _deckCardManager = deckCardManager;
            CurrentUser = player;
            PlayerTurn = true;
        }


        //TO-DO: Scaffold PlayHub
        public async Task SendCard()
        {
            List<DeckCard> discard = await _deckCardManager.GetDeck(1, DeckType.Discard);
            DeckCard deckCard1 = discard[discard.Count - 1];
            DeckCard deckCard2 = discard[discard.Count - 2];
            Card card1 = deckCard1.Card;
            Card card2 = deckCard2.Card;
            var card1Rank = card1.Rank;
            var card1Suit = card1.Suit;
            var card2Rank = card2.Rank;
            var card2Suit = card2.Suit;
            await Clients.All.SendAsync("Recieve Card", card1Rank, card1Suit, card2Rank, card2Suit);
        }

        public async Task ComputerFlip()
        {
            while (PlayerTurn == false)
            {

                await Task.Delay(50);
                await _deckCardManager.Flip(2);
            }
        }

        public async Task PlayerFlip()
        {
            await _deckCardManager.Flip(CurrentUser.ID);
        }
    }
}
