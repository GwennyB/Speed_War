﻿using Microsoft.AspNetCore.SignalR;
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
        }

        public async Task SendCard(Card temp, string username)
        {
            Card FirstCard = await _user.GetFirstCard(username);
            Card SecondCard = await _user.GetSecondCard(username);
            SecondCard = FirstCard;
            FirstCard = temp;
            await _user.UpdateSecondCard(username, SecondCard.ID);
            await _user.UpdateFirstCard(username, FirstCard.ID);

            string card1Rank = "null";
            string card1Suit = "null";
            string card2Rank = "null";
            string card2Suit = "null";
            if (FirstCard.ID != 53)
            {
                card1Rank = FirstCard.Rank.ToString();
                card1Suit = FirstCard.Suit.ToString();
            }
            if (SecondCard.ID != 54)
            {
                card2Rank = SecondCard.Rank.ToString();
                card2Suit = SecondCard.Suit.ToString();
            }
            await Clients.All.SendAsync("ReceiveCard", card1Rank, card1Suit, card2Rank, card2Suit);
        }

        public async Task ComputerFlip(string username)
        {
            User player = await _user.GetUserAsync(username);
            bool playerTurn = player.PlayerTurn;
            Card FirstCard = await _user.GetFirstCard(username);
            Card SecondCard = await _user.GetSecondCard(username);
            bool EmptyDecks = await _deck.EmptyDecks(player.ID);
            while ( playerTurn == false)
            {
                if (FirstCard.Rank == SecondCard.Rank)
                {
                    await Task.Delay(1000);
                    if (playerTurn == false)
                    {
                        await Task.Delay(1000);
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
                    await Task.Delay(4000);
                    await SendCard(temp, username);
                }
   
            }
        }

        public async Task PlayerFlip(string username)
        {
            User player = await _user.GetUserAsync(username);
            bool playerTurn = player.PlayerTurn;
            Card FirstCard = await _user.GetFirstCard(username);
            Card SecondCard = await _user.GetSecondCard(username);
            bool EmptyDecks = await _deck.EmptyDecks(player.ID);

            
            if ((FirstCard.ID != 53 && SecondCard.ID != 54 && FirstCard.Rank == SecondCard.Rank))
            {
                await Slap(player.Name, "computer"); 
            }

            else
            {
                Card temp = await _deck.Flip(player.ID);
                if (temp == null)
                {
                    await _deck.ResetDecks(2);
                    temp = await _deck.Flip(player.ID);
                }
                else
                {
                    await SendCard(temp, username);
                }
            }

            if ( playerTurn == true)
            {
                await _user.UpdatePlayerTurn(username, false);
            }

        }


        public async Task Slap (string playerName, string slapBy)
        {
            if (slapBy != "computer")
            {
                await Clients.All.SendAsync("PauseGame");
            }
            await _user.UpdatePlayerTurn(playerName, true);
            User slapper = await _user.GetUserAsync(slapBy);
            Card FirstCard = await _user.GetFirstCard(playerName);
            Card SecondCard = await _user.GetSecondCard(playerName);
            if (FirstCard == SecondCard)
            {
                await _deck.Slap(slapper.ID);
            }
        }


    }
}
