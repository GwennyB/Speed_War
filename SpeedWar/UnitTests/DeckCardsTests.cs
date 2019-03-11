using SpeedWar.Models;
using Xunit;

namespace UnitTests
{
    public class DeckCardsTests
    {
        [Fact]
        public void DeckIDGetSet()
        {
            DeckCard card = new DeckCard();
            card.DeckID = 1;
            Assert.Equal(1, card.DeckID);
        }

        [Fact]
        public void CardIDGetSet()
        {
            DeckCard card = new DeckCard();
            card.CardID = 1;
            Assert.Equal(1, card.CardID);
        }

        [Fact]
        public void DeckGetSet()
        {
            DeckCard card = new DeckCard();
            Deck deck = new Deck();
            card.Deck = deck;
            Assert.Equal(deck, card.Deck);
        }

        [Fact]
        public void CardGetSet()
        {
            DeckCard card = new DeckCard();
            Card refCard = new Card();
            card.Card = refCard;
            Assert.Equal(refCard, card.Card);
        }
    }
}
