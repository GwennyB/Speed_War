using SpeedWar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class DecksTests
    {
        [Fact]
        public void DeckIDGetSet()
        {
            Deck deck = new Deck();
            deck.ID = 1;
            Assert.Equal(1, deck.ID);
        }

        [Fact]
        public void UserIDGetSet()
        {
            Deck deck = new Deck();
            deck.UserID = 1;
            Assert.Equal(1, deck.UserID);
        }

        [Fact]
        public void DeckTypeGetSet()
        {
            Deck deck = new Deck();
            deck.DeckType = DeckType.Collect;
            Assert.Equal(DeckType.Collect, deck.DeckType);
        }

        [Fact]
        public void UserGetSet()
        {
            Deck deck = new Deck();
            User user = new User();
            deck.User = user;
            Assert.Equal(user, deck.User);
        }

    }

}
