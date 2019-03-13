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
        public async Task SendCard(Card temp)
        {
            SecondCard = FirstCard;
            FirstCard = temp;
            string card1Rank = "null";
            string card1Suit = "null";
            string card2Rank = "null";
            string card2Suit = "null";
            if (FirstCard != null)
            {
                card1Rank = FirstCard.Rank.ToString();
                card1Suit = FirstCard.Suit.ToString();
            }
            if (SecondCard != null)
            {
                card2Rank = SecondCard.Rank.ToString();
                card2Suit = SecondCard.Suit.ToString();
            }
            await Clients.All.SendAsync("ReceiveCard", card1Rank, card1Suit, card2Rank, card2Suit);
        }

        public async Task ComputerFlip(string secondRank, string secondSuit)
        {
            //while (PlayerTurn == false)
            //{
                FirstCard = await _deckCardManager.Flip(2);

   
                await Task.Delay(1000);
                await SendCard(temp);
            //}
        }

        public async Task PlayerFlip(string secondRank, string secondSuit, string userName)
        {
            PlayerTurn = true;
            User user = await _userManager.GetUserAsync(userName);
            FirstCard = await _deckCardManager.Flip(user.ID);

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
            PlayerTurn = false;
            await ComputerFlip(card1Rank, card1Suit);
        }



    }
}
