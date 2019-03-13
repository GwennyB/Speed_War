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
        private IUserManager _user;

        public PlayHub(IDeckCardManager deckCardManager, IUserManager userManager)
        {
            _deck = deckCardManager;
            _user = userManager;
            //PlayerTurn = true;
        }

        //public async Task Intro(string username)
        //{
        //    CurrentUser = await _userManager.GetUserAsync(username);
        //}


        public async Task SendCard(Card temp, string username)
        {
            Card FirstCard = await _user.GetFirstCard(username);
            Card SecondCard = await _user.GetSecondCard(username);
            await _user.UpdateSecondCard(username, FirstCard.ID);
            await _user.UpdateFirstCard(username, temp.ID);
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

        public async Task ComputerFlip(string username)
        {
            bool PlayerTurn = await _user.GetPlayerTurn(username);
            Card FirstCard = await _user.GetFirstCard(username);
            Card SecondCard = await _user.GetSecondCard(username);
            bool EmptyDecks = await _deck.EmptyDecks(2);
            while ( PlayerTurn== false)
            {
                if (FirstCard.Rank == SecondCard.Rank)
                {
                    await Task.Delay(1000);
                    if (PlayerTurn == false)
                    {
                        await Slap("computer");
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
                    SendCard(temp, username);
                }
   
            }
        }

        public async Task PlayerFlip(string username)
        {
            User CurrentUser = await _user.GetUserAsync(username);
            bool PlayerTurn = await _user.GetPlayerTurn(username);
            Card FirstCard = await _user.GetFirstCard(username);
            Card SecondCard = await _user.GetSecondCard(username);
            bool EmptyDecks = await _deck.EmptyDecks(CurrentUser.ID);

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
                    SendCard(temp, username);
                }
            }

            if (PlayerTurn == true)
            {
                PlayerTurn = false;
                ComputerFlip(username);
            }

        }


        public async Task Slap (string username)
        {
            User CurrentUser = await _user.GetUserAsync(username);
            bool PlayerTurn = await _user.GetPlayerTurn(username);
            Card FirstCard = await _user.GetFirstCard(username);
            Card SecondCard = await _user.GetSecondCard(username);
            PlayerTurn = true;
            if (FirstCard == SecondCard)
            {
                await _deck.Slap(CurrentUser.ID);
            }
        }


    }
}
