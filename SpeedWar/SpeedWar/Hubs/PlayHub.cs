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
        public async Task SendCard(string card1Rank, string card1Suit, string card2Rank, string card2Suit)
        {
            await Clients.All.SendAsync("RecieveCard", card1Rank, card1Suit, card2Rank, card2Suit);
        }

        public async Task ComputerFlip()
        {
            while (PlayerTurn == false)
            {
                SecondCard = FirstCard;
                FirstCard = await _deckCardManager.Flip(2);
                await SendCard(FirstCard.Rank.ToString(), FirstCard.Suit.ToString(), SecondCard.Rank.ToString(), SecondCard.Suit.ToString());
            }
        }

        public async Task PlayerFlip()
        {

            SecondCard = FirstCard;
            FirstCard = await _deckCardManager.Flip(3);

            string card1Rank = "null";
            string card1Suit = "null";
            if (FirstCard != null)
            {
                card1Rank = FirstCard.Rank.ToString();
                card1Suit = FirstCard.Suit.ToString();
            }

            string card2Rank = "";
            string card2Suit = "";
            if (SecondCard != null)
            {
                card2Rank = SecondCard.Rank.ToString();
                card2Suit = SecondCard.Suit.ToString();
            }
            await SendCard(card1Rank, card1Suit, card2Rank, card2Suit);
        }
    }
}
