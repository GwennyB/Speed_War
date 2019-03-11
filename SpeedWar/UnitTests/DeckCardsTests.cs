using Microsoft.EntityFrameworkCore;
using SpeedWar.Data;
using SpeedWar.Models;
using SpeedWar.Models.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        [Fact]
        public async Task CanGetDeck()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("GetExistingUser").Options;

            using (CardDbContext context = new CardDbContext(options))
            {
                DeckCardMgmtSvc svc = new DeckCardMgmtSvc(context);
                Deck deck = new Deck() { ID = 1, UserID = 1, DeckType = DeckType.Play };
                await context.Decks.AddAsync(deck);
                List<DeckCard> cards = new List<DeckCard>();
                cards.Add(new DeckCard() { CardID = 1, DeckID = 1 });
                await context.DeckCards.AddAsync(cards[0]);
                await context.SaveChangesAsync();

                Assert.Equal(cards, (await svc.GetDeck(1,DeckType.Play)));
            }
        }

        [Fact]
        public async Task CanGetCard()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("GetExistingUser").Options;

            using (CardDbContext context = new CardDbContext(options))
            {
                DeckCardMgmtSvc svc = new DeckCardMgmtSvc(context);
                Deck deck = new Deck() { ID = 1, UserID = 1, DeckType = DeckType.Play };
                await context.Decks.AddAsync(deck);
                DeckCard card = new DeckCard() { CardID = 1, DeckID = 1 };
                await context.DeckCards.AddAsync(card);
                await context.SaveChangesAsync();

                Assert.Equal(card, await svc.GetCard(1, DeckType.Play));
            }
        }

        [Fact]
        public async Task CanGetCardFromPile()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("GetExistingUser").Options;

            using (CardDbContext context = new CardDbContext(options))
            {
                DeckCardMgmtSvc svc = new DeckCardMgmtSvc(context);
                Deck deck = new Deck() { ID = 1, UserID = 1, DeckType = DeckType.Play };
                await context.Decks.AddAsync(deck);
                DeckCard card1 = new DeckCard() { CardID = 1, DeckID = 1 };
                DeckCard card2 = new DeckCard() { CardID = 2, DeckID = 1 };
                DeckCard card3 = new DeckCard() { CardID = 3, DeckID = 1 };
                await context.DeckCards.AddAsync(card1);
                await context.DeckCards.AddAsync(card2);
                await context.DeckCards.AddAsync(card3);
                await context.SaveChangesAsync();

                List<DeckCard> list = new List<DeckCard>();
                list.Add(card1);
                list.Add(card2);
                list.Add(card3);

                Assert.Contains(await svc.GetCard(1, DeckType.Play), list);
            }
        }

        [Fact]
        public async Task CanUpdateDeckCard()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("GetExistingUser").Options;

            using (CardDbContext context = new CardDbContext(options))
            {
                DeckCardMgmtSvc svc = new DeckCardMgmtSvc(context);
                DeckCard card1 = new DeckCard() { CardID = 1, DeckID = 1 };
                await context.DeckCards.AddAsync(card1);
                await context.SaveChangesAsync();

                DeckCard card2 = new DeckCard() { CardID = card1.CardID, DeckID = 2 };
                await svc.UpdateDeckCard(card2);

                var query = await context.DeckCards.FirstOrDefaultAsync(d => d.CardID == 1);

                Assert.Equal(2, query.DeckID);
            }
        }

        [Fact]
        public async Task CanGetAllCards()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("GetExistingUser").Options;

            using (CardDbContext context = new CardDbContext(options))
            {
                DeckCardMgmtSvc svc = new DeckCardMgmtSvc(context);
                Card card1 = new Card() { ID = 1, Rank = Rank.Ace, Suit = Suit.hearts };
                Card card2 = new Card() { ID = 2, Rank = Rank.Ace, Suit = Suit.spades };
                Card card3 = new Card() { ID = 3, Rank = Rank.Ace, Suit = Suit.clubs };
                Card card4 = new Card() { ID = 4, Rank = Rank.Ace, Suit = Suit.diamonds };
                await context.Cards.AddAsync(card1);
                await context.Cards.AddAsync(card2);
                await context.Cards.AddAsync(card3);
                await context.Cards.AddAsync(card4);
                await context.SaveChangesAsync();

                List<Card> list = new List<Card>();
                list.Add(card1);
                list.Add(card2);
                list.Add(card3);
                list.Add(card4);

                var query = await svc.GetAllCardsAsync();

                Assert.Equal(list, query);
            }
        }



    }
}
