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
            string card1Img = "null";
            string card2Rank = "null";
            string card2Img = "null";
            if (FirstCard.ID != 53)
            {
                card1Img = FirstCard.ImageURL;
                card1Rank = FirstCard.Rank.ToString();
                //card1Suit = FirstCard.Suit.ToString();
            }
            if (SecondCard.ID != 54)
            {
                card2Img = SecondCard.ImageURL;
                card2Rank = SecondCard.Rank.ToString();
                //card2Suit = SecondCard.Suit.ToString();
            }
            await Clients.All.SendAsync("ReceiveCard", card1Rank, card1Img, card2Rank, card2Img);
        }

        public async Task ComputerFlip(string username)
        {
            User player = await _user.GetUserAsync(username);

            Card temp = await _deck.Flip(2);
            if (temp == null)
            {
                await _deck.ResetDecks(2);
                temp = await _deck.Flip(2);
            }
            if (temp != null)
            {
                await SendCard(temp, username);
            }

            await Task.Delay(1000);

        }

        private async Task<bool> CheckMatch(string username)
        {
            Card FirstCard = await _user.GetFirstCard(username);
            Card SecondCard = await _user.GetSecondCard(username);
            if (FirstCard.Rank == SecondCard.Rank) return true;
            return false;
        }

        public async Task PlayerFlip(string username)
        {
            User player = await _user.GetUserAsync(username);

                Card temp = await _deck.Flip(player.ID);
                if (temp == null)
                {
                    await _deck.ResetDecks(player.ID);
                    temp = await _deck.Flip(player.ID);
                }
                if (temp != null)
                {
                    await SendCard(temp, username);
                }

            await Task.Delay(1000);
        }


        public async Task Slap (string playerName, string slapBy)
        {

            //User player = await _user.GetUserAsync(playerName);
            //User slapper = await _user.GetUserAsync(slapBy);
            //Card FirstCard = await _user.GetFirstCard(playerName);
            //Card SecondCard = await _user.GetSecondCard(playerName);
            //if (FirstCard == SecondCard)
            {
                await _deck.Slap(slapBy); // (slapper.ID);
                await Clients.All.SendAsync("CompSlap");
            }
            //return await _deck.CheckWinner(player.ID);
        }

        public async Task<bool> CheckDecks(string username)
        {
            var player = await _user.GetUserAsync(username);
            return await _deck.EmptyDecks(player.ID);
        }

    }
}
