using Microsoft.AspNetCore.SignalR;
using SpeedWar.Models;
using SpeedWar.Models.Interfaces;
using SpeedWar.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWar.Hubs
{
    public class PlayHub : Hub
    {
        private IDeckCardManager _deckCardManager;
        private IUserManager _userManager;

        public bool PlayerTurn { get; set; }
        public User CurrentUser { get; set; }
        public Card FirstCard { get; set; }
        public Card SecondCard { get; set; }



        public PlayHub(IDeckCardManager deckCardManager, IUserManager userManager)
        {
            _deckCardManager = deckCardManager;
            _userManager = userManager;
            PlayerTurn = true;
        }

        public async Task Intro(string username)
        {
            CurrentUser = await _userManager.GetUserAsync(username);
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

        public async Task PlayerFlip(string secondRank, string secondSuit)
        {
            FirstCard = await _deckCardManager.Flip(CurrentUser.ID);

            string card1Rank = "null";
            string card1Suit = "null";
            if (FirstCard != null)
            {
                card1Rank = FirstCard.Rank.ToString();
                card1Suit = FirstCard.Suit.ToString();
            }

            string card2Rank = secondRank;
            string card2Suit = secondSuit;
            await SendCard(card1Rank, card1Suit, card2Rank, card2Suit);
        }
    }
}
