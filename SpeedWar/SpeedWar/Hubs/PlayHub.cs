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
        private IDeckCardManager _deck;
        private IUserManager _userManager;

        public bool PlayerTurn { get; set; }
        public User CurrentUser { get; set; }
        public Card FirstCard { get; set; }
        public Card SecondCard { get; set; }
        public bool EmptyDecks { get; set; }

        public PlayHub(IDeckCardManager deckCardManager, IUserManager userManager)
        {
            _deck = deckCardManager;
            _userManager = userManager;
            //PlayerTurn = true;
        }

        public async Task Intro(string username)
        {
            CurrentUser = await _userManager.GetUserAsync(username);
        }


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

        public async Task ComputerFlip()
        {
            while (PlayerTurn == false)
            {
                if (FirstCard.Rank == SecondCard.Rank)
                {
                    await Task.Delay(1000);
                    if (PlayerTurn == false)
                    {
                        await Slap(2);
                    }
                }

                Card temp = await _deck.Flip(2);

                if (EmptyDecks == false && temp == null)
                {
                    await _deck.ResetDecks(2);
                    temp = await _deck.Flip(2);
                    if (temp == null)
                    {
                        EmptyDecks = true;
                    }
                }
                else if (temp != null)
                {
                    SendCard(temp);
                }
   
            }
        }

        public async Task PlayerFlip()
        {

            if ((FirstCard != null && SecondCard != null && FirstCard.Rank != SecondCard.Rank) || (FirstCard == null || SecondCard == null))
            {
                Card temp = await _deck.Flip(CurrentUser.ID);
                if (temp == null)
                {
                    await _deck.ResetDecks(2);
                    temp = await _deck.Flip(CurrentUser.ID);
                }
                else
                {
                    SendCard(temp);
                }
            }

            if (PlayerTurn == true)
            {
                PlayerTurn = false;
                ComputerFlip();
            }

        }


        public async Task Slap (int ID)
        {
            PlayerTurn = true;
            if (FirstCard == SecondCard)
            {
                await _deck.Slap(ID);
            }
        }


    }
}
