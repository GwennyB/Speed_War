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
        /// <param name="modelBuilder">  </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // register composite keys
            modelBuilder.Entity<DeckCard>().HasKey(ce => new { ce.CardID, ce.DeckID });

            // DB seed data
            modelBuilder.Entity<Card>().HasData(
                new Card { ID = 1, Rank = Rank.Ace, Suit = Suit.hearts },
                new Card { ID = 1, Rank = Rank.Two, Suit = Suit.hearts },
                new Card { ID = 1, Rank = Rank.Three, Suit = Suit.hearts },
                new Card { ID = 1, Rank = Rank.Four, Suit = Suit.hearts },
                new Card { ID = 1, Rank = Rank.Five, Suit = Suit.hearts },
                new Card { ID = 1, Rank = Rank.Six, Suit = Suit.hearts },
                new Card { ID = 1, Rank = Rank.Seven, Suit = Suit.hearts },
                new Card { ID = 1, Rank = Rank.Eight, Suit = Suit.hearts },
                new Card { ID = 1, Rank = Rank.Nine, Suit = Suit.hearts },
                new Card { ID = 1, Rank = Rank.Ten, Suit = Suit.hearts },
                new Card { ID = 1, Rank = Rank.Jack, Suit = Suit.hearts },
                new Card { ID = 1, Rank = Rank.Queen, Suit = Suit.hearts },
                new Card { ID = 1, Rank = Rank.King, Suit = Suit.hearts },
                new Card { ID = 1, Rank = Rank.Ace, Suit = Suit.spades },
                new Card { ID = 1, Rank = Rank.Two, Suit = Suit.spades },
                new Card { ID = 1, Rank = Rank.Three, Suit = Suit.spades },
                new Card { ID = 1, Rank = Rank.Four, Suit = Suit.spades },
                new Card { ID = 1, Rank = Rank.Five, Suit = Suit.spades },
                new Card { ID = 1, Rank = Rank.Six, Suit = Suit.spades },
                new Card { ID = 1, Rank = Rank.Seven, Suit = Suit.spades },
                new Card { ID = 1, Rank = Rank.Eight, Suit = Suit.spades },
                new Card { ID = 1, Rank = Rank.Nine, Suit = Suit.spades },
                new Card { ID = 1, Rank = Rank.Ten, Suit = Suit.spades },
                new Card { ID = 1, Rank = Rank.Jack, Suit = Suit.spades },
                new Card { ID = 1, Rank = Rank.Queen, Suit = Suit.spades },
                new Card { ID = 1, Rank = Rank.King, Suit = Suit.spades },
                new Card { ID = 1, Rank = Rank.Ace, Suit = Suit.clubs },
                new Card { ID = 1, Rank = Rank.Two, Suit = Suit.clubs },
                new Card { ID = 1, Rank = Rank.Three, Suit = Suit.clubs },
                new Card { ID = 1, Rank = Rank.Four, Suit = Suit.clubs },
                new Card { ID = 1, Rank = Rank.Five, Suit = Suit.clubs },
                new Card { ID = 1, Rank = Rank.Six, Suit = Suit.clubs },
                new Card { ID = 1, Rank = Rank.Seven, Suit = Suit.clubs },
                new Card { ID = 1, Rank = Rank.Eight, Suit = Suit.clubs },
                new Card { ID = 1, Rank = Rank.Nine, Suit = Suit.clubs },
                new Card { ID = 1, Rank = Rank.Ten, Suit = Suit.clubs },
                new Card { ID = 1, Rank = Rank.Jack, Suit = Suit.clubs },
                new Card { ID = 1, Rank = Rank.Queen, Suit = Suit.clubs },
                new Card { ID = 1, Rank = Rank.King, Suit = Suit.clubs },
                new Card { ID = 1, Rank = Rank.Ace, Suit = Suit.diamonds },
                new Card { ID = 1, Rank = Rank.Two, Suit = Suit.diamonds },
                new Card { ID = 1, Rank = Rank.Three, Suit = Suit.diamonds },
                new Card { ID = 1, Rank = Rank.Four, Suit = Suit.diamonds },
                new Card { ID = 1, Rank = Rank.Five, Suit = Suit.diamonds },
                new Card { ID = 1, Rank = Rank.Six, Suit = Suit.diamonds },
                new Card { ID = 1, Rank = Rank.Seven, Suit = Suit.diamonds },
                new Card { ID = 1, Rank = Rank.Eight, Suit = Suit.diamonds },
                new Card { ID = 1, Rank = Rank.Nine, Suit = Suit.diamonds },
                new Card { ID = 1, Rank = Rank.Ten, Suit = Suit.diamonds },
                new Card { ID = 1, Rank = Rank.Jack, Suit = Suit.diamonds },
                new Card { ID = 1, Rank = Rank.Queen, Suit = Suit.diamonds },
                new Card { ID = 1, Rank = Rank.King, Suit = Suit.diamonds }
                );


        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<DeckCard> DeckCards { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
