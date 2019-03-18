using SpeedWar.Models;
using System;
using Xunit;

namespace UnitTests
{
    public class CardsTests
    {


        /// <summary>
        /// getter-ID
        /// </summary>
        [Fact]
        public void IDGetSet()
        {
            Card card = new Card();
            card.ID = 1;
            Assert.Equal(1, card.ID);
        }
        /// <summary>
        /// setter--ID
        /// </summary>
        [Fact]
        public void IDSet()
        {
            Card card = new Card();
            card.ID = 1;
            card.ID = 2;
            Assert.Equal(2, card.ID);
        }
        /// <summary>
        /// getter-suit
        /// </summary>
        [Fact]
        public void SuitGet()
        {
            Card card = new Card();
            card.Suit = Suit.clubs;
            Assert.Equal(Suit.clubs, card.Suit);
        }
        /// <summary>
        /// setter-suit
        /// </summary>
        [Fact]
        public void SuitSet()
        {
            Card card = new Card();
            card.Suit = Suit.clubs;
            card.Suit = Suit.diamonds;
            Assert.Equal(Suit.diamonds, card.Suit);
        }

        /// <summary>
        /// getter--Rank
        /// </summary>
        [Fact]
        public void RankGet()
        {
            Card card = new Card();
            card.Rank = Rank.Ace;
            Assert.Equal(Rank.Ace, card.Rank);
        }

        /// <summary>
        /// setter-rank
        /// </summary>
        [Fact]
        public void RankSet()
        {
            Card card = new Card();
            card.Rank = Rank.Ace;
            card.Rank = Rank.Jack;
            Assert.Equal(Rank.Jack, card.Rank);
        }


        /// <summary>
        /// ImgUrl-getter
        /// </summary>
        [Fact]
        public void ImgUrlGet()
        {
            Card card = new Card();
            card.ImageURL = "demo/test.jpg";
            Assert.Equal("demo/test.jpg", card.ImageURL);
        }

        /// <summary>
        /// ImgUrl--Setter
        /// </summary>
        [Fact]
        public void ImgUrlSet()
        {
            Card card = new Card();
            card.ImageURL = "demo/test.jpg";
            card.ImageURL = "demo/Newtest.jpg";
            Assert.Equal("demo/Newtest.jpg", card.ImageURL);
        }

    }
}
