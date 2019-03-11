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

        public PlayHub(IDeckCardManager deckCardManager)
        {
            _deckCardManager = deckCardManager;
        }
        //TO-DO: Scaffold PlayHub
        public async Task PlayCard(Deck deck)
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
            await Clients.All.SendAsync("recieve message", card1Rank, card1Suit, card2Rank, card2Suit);
        }
    }
}
