using SpeedWar.Models;
using System;
using Xunit;

namespace UnitTests
{
    public class CardsTests
    {
        [Fact]
        public void IDGetSet()
        {
            Card card = new Card();
            card.ID = 1;
            Assert.Equal(1, card.ID);
        }

        [Fact]
        public void SuitGetSet()
        {
            Card card = new Card();
            card.Suit = Suit.clubs;
            Assert.Equal(Suit.clubs, card.Suit);
        }

        [Fact]
        public void RankGetSet()
        {
            Card card = new Card();
            card.Rank = Rank.Ace;
            Assert.Equal(Rank.Ace, card.Rank);
        }

    }
}
