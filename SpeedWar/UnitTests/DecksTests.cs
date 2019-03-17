using SpeedWar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class DecksTests
    {/// <summary>
     /// Getter--DeckID
     /// </summary>
        [Fact]
        public void DeckIDGet()
        {
            Deck deck = new Deck();
            deck.ID = 1;
            Assert.Equal(1, deck.ID);
        }
        /// <summary>
        /// Setter--DeckID
        /// </summary>
        [Fact]
        public void DeckIDSet()
        {
            Deck deck = new Deck();
            deck.ID = 1;
            deck.ID = 3;
            Assert.Equal(3, deck.ID);
        }

        /// <summary>
        /// getter-UserID
        /// </summary>
        [Fact]
        public void UserIDGet()
        {
            Deck deck = new Deck();
            deck.UserID = 1;
            Assert.Equal(1, deck.UserID);
        }
        /// <summary>
        /// setter-UserID
        /// </summary>
        [Fact]
        public void UserIDSet()
        {
            Deck deck = new Deck();
            deck.UserID = 1;
            deck.UserID = 3;
            Assert.Equal(3, deck.UserID);
        }
        /// <summary>
        /// getter-DeckType
        /// </summary>
        [Fact]
        public void DeckTypeGet()
        {
            Deck deck = new Deck();
            deck.DeckType = DeckType.Collect;
            Assert.Equal(DeckType.Collect, deck.DeckType);
        }

        /// <summary>
        /// setter-DeckType
        /// </summary>
        [Fact]
        public void DeckTypeSet()
        {
            Deck deck = new Deck();
            deck.DeckType = DeckType.Collect;
            deck.DeckType = DeckType.Discard;
            Assert.Equal(DeckType.Discard, deck.DeckType);
        }

        /// <summary>
        /// navi to user
        /// </summary>
        [Fact]
        public void UserNav()
        {
            Deck deck = new Deck();
            User user = new User();
            deck.User = user;
            Assert.Equal(user, deck.User);
        }

    }

}
