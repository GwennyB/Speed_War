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
        /// <summary>
        /// Deck ID get-set
        /// </summary>
        [Fact]
        public void DeckIDGetSet()
        {
            DeckCard card = new DeckCard();
            card.DeckID = 1;
            Assert.Equal(1, card.DeckID);
        }

        /// <summary>
        /// Card ID get-set
        /// </summary>
        [Fact]
        public void CardIDGetSet()
        {
            DeckCard card = new DeckCard();
            card.CardID = 1;
            Assert.Equal(1, card.CardID);
        }

        /// <summary>
        /// deck get-set
        /// </summary>
        [Fact]
        public void DeckGetSet()
        {
            DeckCard card = new DeckCard();
            Deck deck = new Deck();
            card.Deck = deck;
            Assert.Equal(deck, card.Deck);
        }

        /// <summary>
        /// card get
        /// </summary>
        [Fact]
        public void CardGetSet()
        {
            DeckCard card = new DeckCard();
            Card refCard = new Card();
            card.Card = refCard;
            Assert.Equal(refCard, card.Card);
        }

        /// <summary>
        /// get deck by UserID and DeckType
        /// </summary>
        /// <returns> completed task </returns>
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

                Assert.Equal(cards, (await svc.GetDeck(1, DeckType.Play)));
            }
        }

        /// <summary>
        /// get card when no pile
        /// </summary>
        /// <returns> completed task </returns>
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

        /// <summary>
        /// get card when it's in a pile
        /// </summary>
        /// <returns> completed task </returns>
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

        /// <summary>
        /// can move card from one deck to another
        /// </summary>
        /// <returns> completed task </returns>
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

        /// <summary>
        /// can get all cards in the database
        /// </summary>
        /// <returns> completed task </returns>
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

        /// <summary>
        /// can deal a new game
        /// </summary>
        /// <returns> completed task </returns>
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
                Deck deck3 = new Deck() { ID = 4, UserID = 10, DeckType = DeckType.Collect };
                Deck deck4 = new Deck() { ID = 5, UserID = 2, DeckType = DeckType.Collect };
                await context.Decks.AddAsync(deck1);
                await context.Decks.AddAsync(deck2);
                await context.Decks.AddAsync(deck3);
                await context.Decks.AddAsync(deck4);
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

        /// <summary>
        /// can clear all cards from a specified deck
        /// </summary>
        /// <returns> completed task </returns>
        [Fact]
        public async Task CanCleanDeck()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("CleanDeck").Options;

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
                Deck deck = new Deck() { ID = 1, UserID = 3, DeckType = DeckType.Collect };
                await context.Decks.AddAsync(deck);
                DeckCard dc1 = new DeckCard() { CardID = 1, DeckID = 1 };
                DeckCard dc2 = new DeckCard() { CardID = 2, DeckID = 1 };
                DeckCard dc3 = new DeckCard() { CardID = 3, DeckID = 1 };
                DeckCard dc4 = new DeckCard() { CardID = 4, DeckID = 1 };
                await context.DeckCards.AddAsync(dc1);
                await context.DeckCards.AddAsync(dc2);
                await context.DeckCards.AddAsync(dc3);
                await context.DeckCards.AddAsync(dc4);
                await context.SaveChangesAsync();

                await svc.CleanDeck(deck);

                Assert.Empty(await svc.GetDeck(deck.UserID, deck.DeckType));
            }
        }

        /// <summary>
        /// can compare cards for rank match
        /// </summary>
        [Fact]
        public void CanCompareCardsTrue()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("CompareCardsTrue").Options;

            using (CardDbContext context = new CardDbContext(options))
            {
                DeckCardMgmtSvc svc = new DeckCardMgmtSvc(context);
                Card card1 = new Card() { ID = 1, Rank = Rank.Ace, Suit = Suit.hearts };
                Card card2 = new Card() { ID = 2, Rank = Rank.Ace, Suit = Suit.spades };

                Assert.True(svc.CompareCards(card1, card2));
            }
        }

        /// <summary>
        /// can compare cards for rank mis-match
        /// </summary>
        [Fact]
        public void CanCompareCardsFalse()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("CompareCardsFalse").Options;

            using (CardDbContext context = new CardDbContext(options))
            {
                DeckCardMgmtSvc svc = new DeckCardMgmtSvc(context);
                Card card1 = new Card() { ID = 1, Rank = Rank.Ace, Suit = Suit.hearts };
                Card card2 = new Card() { ID = 2, Rank = Rank.King, Suit = Suit.spades };

                Assert.False(svc.CompareCards(card1, card2));
            }
        }

        /// <summary>
        /// can return card on flip from populated deck
        /// </summary>
        /// <returns> completed task </returns>
        [Fact]
        public async Task CanFlipReturnCard()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("FlipReturnCard").Options;

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
                Deck deck = new Deck() { ID = 1, UserID = 3, DeckType = DeckType.Play };
                await context.Decks.AddAsync(deck);
                DeckCard dc1 = new DeckCard() { CardID = 1, DeckID = 1 };
                DeckCard dc2 = new DeckCard() { CardID = 2, DeckID = 1 };
                DeckCard dc3 = new DeckCard() { CardID = 3, DeckID = 1 };
                DeckCard dc4 = new DeckCard() { CardID = 4, DeckID = 1 };
                await context.DeckCards.AddAsync(dc1);
                await context.DeckCards.AddAsync(dc2);
                await context.DeckCards.AddAsync(dc3);
                await context.DeckCards.AddAsync(dc4);
                await context.SaveChangesAsync();

                Assert.NotNull(await svc.Flip(deck.UserID));
            }
        }

        /// <summary>
        /// can return null when flip called on empty deck
        /// </summary>
        /// <returns> completed task </returns>
        [Fact]
        public async Task CanFlipReturnNull()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("FlipReturnNull").Options;

            using (CardDbContext context = new CardDbContext(options))
            {
                DeckCardMgmtSvc svc = new DeckCardMgmtSvc(context);
                Deck deck = new Deck() { ID = 1, UserID = 3, DeckType = DeckType.Play };
                await context.Decks.AddAsync(deck);
                await context.SaveChangesAsync();

                var query = await svc.Flip(deck.UserID);
                Assert.Null(query);
            }
        }

        /// <summary>
        /// can move all cards in specified 'collect' deck to same user's 'play' deck
        /// </summary>
        /// <returns> completed task </returns>
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
                await svc.ResetDecks(3);
                List<DeckCard> queryAfter = await svc.GetDeck(3, DeckType.Play);

                List<int> cardsBefore = new List<int>();
                List<int> cardsAfter = new List<int>();
                for (int i = 0; i < cardsBefore.Count; i++)
                {
                    cardsBefore.Add(queryBefore[i].CardID);
                    cardsAfter.Add(queryAfter[i].CardID);
                }

                Assert.Equal(cardsBefore, cardsAfter);
            }
        }

        /// <summary>
        /// can move cards from 'discard' deck to slapping user's 'collect' deck
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CanSlap()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("Slap").Options;

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
                Deck deck1 = new Deck() { ID = 1, UserID = 1, DeckType = DeckType.Discard };
                Deck deck2 = new Deck() { ID = 2, UserID = 3, DeckType = DeckType.Collect };
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
                User comp = new User() { ID = 1, Name = "computer" };
                User player = new User() { ID = 3, Name = "person" };
                await context.Users.AddAsync(comp);
                await context.Users.AddAsync(player);
                await context.SaveChangesAsync();

                List<DeckCard> queryBefore = await svc.GetDeck(1, DeckType.Discard);
                await svc.Slap("person");
                List<DeckCard> queryAfter = await svc.GetDeck(3, DeckType.Collect);

                List<int> cardsBefore = new List<int>();
                List<int> cardsAfter = new List<int>();
                for (int i = 0; i < cardsBefore.Count; i++)
                {
                    cardsBefore.Add(queryBefore[i].CardID);
                    cardsAfter.Add(queryAfter[i].CardID);
                }

                Assert.Equal(cardsBefore, cardsAfter);
            }
        }

    }
}
