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
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("GetDeck").Options;

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
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("GetCard").Options;

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
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("GetCardFromPile").Options;

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
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("UpdateDeckCard").Options;

            using (CardDbContext context = new CardDbContext(options))
            {
                DeckCardMgmtSvc svc = new DeckCardMgmtSvc(context);
                DeckCard card1 = new DeckCard() { CardID = 1, DeckID = 1 };
                await context.DeckCards.AddAsync(card1);
                await context.SaveChangesAsync();

                await svc.UpdateDeckCard(card1.CardID, card1.DeckID, 2);

                var query = await context.DeckCards.FirstOrDefaultAsync(d => d.CardID == 1);

                Assert.Equal(2, query.DeckID);
            }
        }

        [Fact]
        public async Task CanGetAllCards()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("GetAllCards").Options;

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

        [Fact]
        public async Task CanDealGame()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("DealGame").Options;

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
                Deck deck1 = new Deck() { ID = 1, UserID = 10, DeckType = DeckType.Play };
                Deck deck2 = new Deck() { ID = 2, UserID = 2, DeckType = DeckType.Play };
                Deck discard = new Deck() { ID = 3, UserID = 1, DeckType = DeckType.Discard };
                await context.Decks.AddAsync(deck1);
                await context.Decks.AddAsync(deck2);
                await context.Decks.AddAsync(discard);
                await context.SaveChangesAsync();


                await svc.DealGameAsync(10);

                List<DeckCard> query1 = await svc.GetDeck(10, DeckType.Play);
                List<DeckCard> query2 = await svc.GetDeck(2, DeckType.Play);

                bool tests = true;
                if (query2.Contains(query1[0]) || query2.Contains(query1[1]) || query1.Count != 2 || query2.Count != 2)
                {
                    tests = false;
                }

                Assert.True(tests);
            }
        }

        [Fact]
        public async Task CanResetDecks()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("ResetDecks").Options;

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
                Deck deck1 = new Deck() { ID = 1, UserID = 3, DeckType = DeckType.Collect };
                Deck deck2 = new Deck() { ID = 2, UserID = 3, DeckType = DeckType.Play };
                await context.Decks.AddAsync(deck1);
                await context.Decks.AddAsync(deck2);
                DeckCard dc1 = new DeckCard() { CardID = 1, DeckID = 1 };
                DeckCard dc2 = new DeckCard() { CardID = 2, DeckID = 1 };
                DeckCard dc3 = new DeckCard() { CardID = 3, DeckID = 1 };
                DeckCard dc4 = new DeckCard() { CardID = 4, DeckID = 1 };
                await context.DeckCards.AddAsync(dc1);
                await context.DeckCards.AddAsync(dc2);
                await context.DeckCards.AddAsync(dc3);
                await context.DeckCards.AddAsync(dc4);
                await context.SaveChangesAsync();

                List<DeckCard> queryBefore = await svc.GetDeck(3, DeckType.Collect);
                await svc.ResetDecks(3, false);
                List<DeckCard> queryAfter = await svc.GetDeck(3, DeckType.Play);

                List<int> cardsBefore = new List<int>();
                List<int> cardsAfter = new List<int>();
                for(int i = 0; i < cardsBefore.Count; i++)
                {
                    cardsBefore.Add(queryBefore[i].CardID);
                    cardsAfter.Add(queryAfter[i].CardID);
                }

                Assert.Equal(cardsBefore,cardsAfter);
            }
        }

        [Fact]
        public async Task CanDeclareCompWinner()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("CompWinner").Options;

            using (CardDbContext context = new CardDbContext(options))
            {
                DeckCardMgmtSvc svc = new DeckCardMgmtSvc(context);
                User comp = new User() { ID = 2, Name = "Computer" };
                User player = new User() { ID = 3, Name = "Player" };
                await context.Users.AddAsync(comp);
                await context.Users.AddAsync(player);
                Card card1 = new Card() { ID = 1, Rank = Rank.Ace, Suit = Suit.hearts };
                Card card2 = new Card() { ID = 2, Rank = Rank.Ace, Suit = Suit.spades };
                Card card3 = new Card() { ID = 3, Rank = Rank.Ace, Suit = Suit.clubs };
                Card card4 = new Card() { ID = 4, Rank = Rank.Ace, Suit = Suit.diamonds };
                await context.Cards.AddAsync(card1);
                await context.Cards.AddAsync(card2);
                await context.Cards.AddAsync(card3);
                await context.Cards.AddAsync(card4);
                Deck deck1 = new Deck() { ID = 1, UserID = 2, DeckType = DeckType.Collect };
                Deck deck2 = new Deck() { ID = 2, UserID = 2, DeckType = DeckType.Play };
                Deck deck3 = new Deck() { ID = 3, UserID = 3, DeckType = DeckType.Collect };
                Deck deck4 = new Deck() { ID = 4, UserID = 3, DeckType = DeckType.Play };
                await context.Decks.AddAsync(deck1);
                await context.Decks.AddAsync(deck2);
                await context.Decks.AddAsync(deck3);
                await context.Decks.AddAsync(deck4);
                DeckCard dc1 = new DeckCard() { CardID = 1, DeckID = 1 };
                DeckCard dc2 = new DeckCard() { CardID = 2, DeckID = 1 };
                DeckCard dc3 = new DeckCard() { CardID = 3, DeckID = 2 };
                DeckCard dc4 = new DeckCard() { CardID = 4, DeckID = 2 };
                await context.DeckCards.AddAsync(dc1);
                await context.DeckCards.AddAsync(dc2);
                await context.DeckCards.AddAsync(dc3);
                await context.DeckCards.AddAsync(dc4);
                await context.SaveChangesAsync();

                Assert.Equal(comp, await svc.CheckWinner(player));
            }
        }

        [Fact]
        public async Task CanDeclarePlayerWinner()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("PlayerWinner").Options;

            using (CardDbContext context = new CardDbContext(options))
            {
                DeckCardMgmtSvc svc = new DeckCardMgmtSvc(context);
                User comp = new User() { ID = 2, Name = "Computer" };
                User player = new User() { ID = 3, Name = "Player" };
                await context.Users.AddAsync(comp);
                await context.Users.AddAsync(player);
                Card card1 = new Card() { ID = 1, Rank = Rank.Ace, Suit = Suit.hearts };
                Card card2 = new Card() { ID = 2, Rank = Rank.Ace, Suit = Suit.spades };
                Card card3 = new Card() { ID = 3, Rank = Rank.Ace, Suit = Suit.clubs };
                Card card4 = new Card() { ID = 4, Rank = Rank.Ace, Suit = Suit.diamonds };
                await context.Cards.AddAsync(card1);
                await context.Cards.AddAsync(card2);
                await context.Cards.AddAsync(card3);
                await context.Cards.AddAsync(card4);
                Deck deck1 = new Deck() { ID = 1, UserID = 2, DeckType = DeckType.Collect };
                Deck deck2 = new Deck() { ID = 2, UserID = 2, DeckType = DeckType.Play };
                Deck deck3 = new Deck() { ID = 3, UserID = 3, DeckType = DeckType.Collect };
                Deck deck4 = new Deck() { ID = 4, UserID = 3, DeckType = DeckType.Play };
                await context.Decks.AddAsync(deck1);
                await context.Decks.AddAsync(deck2);
                await context.Decks.AddAsync(deck3);
                await context.Decks.AddAsync(deck4);
                DeckCard dc1 = new DeckCard() { CardID = 1, DeckID = 3 };
                DeckCard dc2 = new DeckCard() { CardID = 2, DeckID = 3 };
                DeckCard dc3 = new DeckCard() { CardID = 3, DeckID = 4 };
                DeckCard dc4 = new DeckCard() { CardID = 4, DeckID = 4 };
                await context.DeckCards.AddAsync(dc1);
                await context.DeckCards.AddAsync(dc2);
                await context.DeckCards.AddAsync(dc3);
                await context.DeckCards.AddAsync(dc4);
                await context.SaveChangesAsync();

                Assert.Equal(player, await svc.CheckWinner(player));
            }
        }

        [Fact]
        public async Task CanDeclareNullWinner()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("NullWinner").Options;

            using (CardDbContext context = new CardDbContext(options))
            {
                DeckCardMgmtSvc svc = new DeckCardMgmtSvc(context);
                User comp = new User() { ID = 2, Name = "Computer" };
                User player = new User() { ID = 3, Name = "Player" };
                await context.Users.AddAsync(comp);
                await context.Users.AddAsync(player);
                Card card1 = new Card() { ID = 1, Rank = Rank.Ace, Suit = Suit.hearts };
                Card card2 = new Card() { ID = 2, Rank = Rank.Ace, Suit = Suit.spades };
                Card card3 = new Card() { ID = 3, Rank = Rank.Ace, Suit = Suit.clubs };
                Card card4 = new Card() { ID = 4, Rank = Rank.Ace, Suit = Suit.diamonds };
                await context.Cards.AddAsync(card1);
                await context.Cards.AddAsync(card2);
                await context.Cards.AddAsync(card3);
                await context.Cards.AddAsync(card4);
                Deck deck1 = new Deck() { ID = 1, UserID = 2, DeckType = DeckType.Collect };
                Deck deck2 = new Deck() { ID = 2, UserID = 2, DeckType = DeckType.Play };
                Deck deck3 = new Deck() { ID = 3, UserID = 3, DeckType = DeckType.Collect };
                Deck deck4 = new Deck() { ID = 4, UserID = 3, DeckType = DeckType.Play };
                await context.Decks.AddAsync(deck1);
                await context.Decks.AddAsync(deck2);
                await context.Decks.AddAsync(deck3);
                await context.Decks.AddAsync(deck4);
                DeckCard dc1 = new DeckCard() { CardID = 1, DeckID = 1 };
                DeckCard dc2 = new DeckCard() { CardID = 2, DeckID = 1 };
                DeckCard dc3 = new DeckCard() { CardID = 3, DeckID = 4 };
                DeckCard dc4 = new DeckCard() { CardID = 4, DeckID = 4 };
                await context.DeckCards.AddAsync(dc1);
                await context.DeckCards.AddAsync(dc2);
                await context.DeckCards.AddAsync(dc3);
                await context.DeckCards.AddAsync(dc4);
                await context.SaveChangesAsync();

                Assert.Null(await svc.CheckWinner(player));
            }
        }
    }
}
