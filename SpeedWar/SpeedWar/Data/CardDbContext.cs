using Microsoft.EntityFrameworkCore;
using SpeedWar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWar.Data
{
    public class CardDbContext : DbContext
    {
        public CardDbContext(DbContextOptions<CardDbContext> options) : base(options)
        {

        }


        /// <summary>
        /// overrides (DbContext virtual) method that builds out basic API structure
        /// maps composite keys
        /// </summary>
        /// <param name="modelBuilder"> ModelBuilder API context </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // register composite keys
            modelBuilder.Entity<DeckCard>().HasKey(ce => new { ce.CardID, ce.DeckID });

            // DB seed data
            modelBuilder.Entity<Card>().HasData(
                new Card { ID = 1, Rank = Rank.Ace, Suit = Suit.hearts },
                new Card { ID = 2, Rank = Rank.Two, Suit = Suit.hearts },
                new Card { ID = 3, Rank = Rank.Three, Suit = Suit.hearts },
                new Card { ID = 4, Rank = Rank.Four, Suit = Suit.hearts },
                new Card { ID = 5, Rank = Rank.Five, Suit = Suit.hearts },
                new Card { ID = 6, Rank = Rank.Six, Suit = Suit.hearts },
                new Card { ID = 7, Rank = Rank.Seven, Suit = Suit.hearts },
                new Card { ID = 8, Rank = Rank.Eight, Suit = Suit.hearts },
                new Card { ID = 9, Rank = Rank.Nine, Suit = Suit.hearts },
                new Card { ID = 10, Rank = Rank.Ten, Suit = Suit.hearts },
                new Card { ID = 11, Rank = Rank.Jack, Suit = Suit.hearts },
                new Card { ID = 12, Rank = Rank.Queen, Suit = Suit.hearts },
                new Card { ID = 13, Rank = Rank.King, Suit = Suit.hearts },
                new Card { ID = 14, Rank = Rank.Ace, Suit = Suit.spades },
                new Card { ID = 15, Rank = Rank.Two, Suit = Suit.spades },
                new Card { ID = 16, Rank = Rank.Three, Suit = Suit.spades },
                new Card { ID = 17, Rank = Rank.Four, Suit = Suit.spades },
                new Card { ID = 18, Rank = Rank.Five, Suit = Suit.spades },
                new Card { ID = 19, Rank = Rank.Six, Suit = Suit.spades },
                new Card { ID = 20, Rank = Rank.Seven, Suit = Suit.spades },
                new Card { ID = 21, Rank = Rank.Eight, Suit = Suit.spades },
                new Card { ID = 22, Rank = Rank.Nine, Suit = Suit.spades },
                new Card { ID = 23, Rank = Rank.Ten, Suit = Suit.spades },
                new Card { ID = 24, Rank = Rank.Jack, Suit = Suit.spades },
                new Card { ID = 25, Rank = Rank.Queen, Suit = Suit.spades },
                new Card { ID = 26, Rank = Rank.King, Suit = Suit.spades },
                new Card { ID = 27, Rank = Rank.Ace, Suit = Suit.clubs },
                new Card { ID = 28, Rank = Rank.Two, Suit = Suit.clubs },
                new Card { ID = 29, Rank = Rank.Three, Suit = Suit.clubs },
                new Card { ID = 30, Rank = Rank.Four, Suit = Suit.clubs },
                new Card { ID = 31, Rank = Rank.Five, Suit = Suit.clubs },
                new Card { ID = 32, Rank = Rank.Six, Suit = Suit.clubs },
                new Card { ID = 33, Rank = Rank.Seven, Suit = Suit.clubs },
                new Card { ID = 34, Rank = Rank.Eight, Suit = Suit.clubs },
                new Card { ID = 35, Rank = Rank.Nine, Suit = Suit.clubs },
                new Card { ID = 36, Rank = Rank.Ten, Suit = Suit.clubs },
                new Card { ID = 37, Rank = Rank.Jack, Suit = Suit.clubs },
                new Card { ID = 38, Rank = Rank.Queen, Suit = Suit.clubs },
                new Card { ID = 39, Rank = Rank.King, Suit = Suit.clubs },
                new Card { ID = 40, Rank = Rank.Ace, Suit = Suit.diamonds },
                new Card { ID = 41, Rank = Rank.Two, Suit = Suit.diamonds },
                new Card { ID = 42, Rank = Rank.Three, Suit = Suit.diamonds },
                new Card { ID = 43, Rank = Rank.Four, Suit = Suit.diamonds },
                new Card { ID = 44, Rank = Rank.Five, Suit = Suit.diamonds },
                new Card { ID = 45, Rank = Rank.Six, Suit = Suit.diamonds },
                new Card { ID = 46, Rank = Rank.Seven, Suit = Suit.diamonds },
                new Card { ID = 47, Rank = Rank.Eight, Suit = Suit.diamonds },
                new Card { ID = 48, Rank = Rank.Nine, Suit = Suit.diamonds },
                new Card { ID = 49, Rank = Rank.Ten, Suit = Suit.diamonds },
                new Card { ID = 50, Rank = Rank.Jack, Suit = Suit.diamonds },
                new Card { ID = 51, Rank = Rank.Queen, Suit = Suit.diamonds },
                new Card { ID = 52, Rank = Rank.King, Suit = Suit.diamonds }
                );

            modelBuilder.Entity<User>().HasData(
                new User { ID = 1, Name = "Discard"},
                new User { ID = 2, Name = "Computer" },
                new User { ID = 3, Name = "Clarice" },
                new User { ID = 4, Name = "Shalom" },
                new User { ID = 5, Name = "Xia" },
                new User { ID = 6, Name = "Gwen" }
                );

            modelBuilder.Entity<Deck>().HasData(
                new Deck { ID = 1, UserID = 1, DeckType = DeckType.Discard },
                new Deck { ID = 2, UserID = 2, DeckType = DeckType.Play },
                new Deck { ID = 3, UserID = 2, DeckType = DeckType.Collect },
                new Deck { ID = 4, UserID = 3, DeckType = DeckType.Play },
                new Deck { ID = 5, UserID = 3, DeckType = DeckType.Collect },
                new Deck { ID = 6, UserID = 4, DeckType = DeckType.Play },
                new Deck { ID = 7, UserID = 4, DeckType = DeckType.Collect },
                new Deck { ID = 8, UserID = 5, DeckType = DeckType.Play },
                new Deck { ID = 9, UserID = 5, DeckType = DeckType.Collect },
                new Deck { ID = 10, UserID = 6, DeckType = DeckType.Play },
                new Deck { ID = 11, UserID = 6, DeckType = DeckType.Collect }
                );
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<DeckCard> DeckCards { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
