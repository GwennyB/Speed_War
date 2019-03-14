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
        /// <summary>
        /// create a PlayHub constructor
        /// </summary>
        /// <param name="deckCardManager"></param>
        /// <param name="userManager"></param>
        public PlayHub(IDeckCardManager deckCardManager, IUserManager userManager)
        {
            _deck = deckCardManager;
            _user = userManager;
        }

        /// <summary>
        /// send two cards eventually from player side and computer side to play.js and play.js can use the cards to render them on the page
        /// </summary>
        /// <param name="temp">container temp card will be used when switch two cards, player's name</param>
        /// <param name="username"></param>
        /// <returns></returns>
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
            }
            if (SecondCard.ID != 54)
            {
                card2Img = SecondCard.ImageURL;
                card2Rank = SecondCard.Rank.ToString();
            }
            await Clients.All.SendAsync("ReceiveCard", card1Rank, card1Img, card2Rank, card2Img);
        }
        /// <summary>
        /// deck flip the computer side card, if card not exsit we grab the card from collection pile and flip it 
        /// </summary>
        /// <param name="username">player's name</param>
        /// <returns></returns>
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


        }
        /// <summary>
        /// deck will flip the card by user's ID if the card does not exsit, will grab the collection pile to flip the card 
        /// </summary>
        /// <param name="username">player's name</param>
        /// <returns></returns>
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

        }
        /// <summary>
        /// when two cards(from computer side and user side) have same number, either computer or player can do slap,whoever slap quicker will collect all the cards from deck
        /// </summary>
        /// <param name="playerName">player/user's name</param>
        /// <param name="slapBy">username of player with verified 'slap'</param>
        /// <returns></returns>
        public async Task Slap(string playerName, string slapBy)
        {

            {
                await _deck.Slap(slapBy); // (slapper.ID);
            }
            string loser = (slapBy == playerName) ? "computer" : playerName;
            if (await CheckDecks(loser))
            {
                await Clients.All.SendAsync("endGame", slapBy);

            }
        }
        /// <summary>
        /// check user's deck see if it is empty( get player by username and pass in the player's ID to check if that player's deck is empty)
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>boolean</returns>
        public async Task<bool> CheckDecks(string username)
        {
            var player = await _user.GetUserAsync(username);
            return await _deck.EmptyDecks(player.ID);
        }

    }
}
