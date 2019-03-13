﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpeedWar.Data;

namespace SpeedWar.Migrations
{
    [DbContext(typeof(CardDbContext))]
    [Migration("20190313211727_add-states")]
    partial class addstates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SpeedWar.Models.Card", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Rank");

                    b.Property<int>("Suit");

                    b.HasKey("ID");

                    b.ToTable("Cards");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Rank = 1,
                            Suit = 0
                        },
                        new
                        {
                            ID = 2,
                            Rank = 2,
                            Suit = 0
                        },
                        new
                        {
                            ID = 3,
                            Rank = 3,
                            Suit = 0
                        },
                        new
                        {
                            ID = 4,
                            Rank = 4,
                            Suit = 0
                        },
                        new
                        {
                            ID = 5,
                            Rank = 5,
                            Suit = 0
                        },
                        new
                        {
                            ID = 6,
                            Rank = 6,
                            Suit = 0
                        },
                        new
                        {
                            ID = 7,
                            Rank = 7,
                            Suit = 0
                        },
                        new
                        {
                            ID = 8,
                            Rank = 8,
                            Suit = 0
                        },
                        new
                        {
                            ID = 9,
                            Rank = 9,
                            Suit = 0
                        },
                        new
                        {
                            ID = 10,
                            Rank = 10,
                            Suit = 0
                        },
                        new
                        {
                            ID = 11,
                            Rank = 11,
                            Suit = 0
                        },
                        new
                        {
                            ID = 12,
                            Rank = 12,
                            Suit = 0
                        },
                        new
                        {
                            ID = 13,
                            Rank = 13,
                            Suit = 0
                        },
                        new
                        {
                            ID = 14,
                            Rank = 1,
                            Suit = 1
                        },
                        new
                        {
                            ID = 15,
                            Rank = 2,
                            Suit = 1
                        },
                        new
                        {
                            ID = 16,
                            Rank = 3,
                            Suit = 1
                        },
                        new
                        {
                            ID = 17,
                            Rank = 4,
                            Suit = 1
                        },
                        new
                        {
                            ID = 18,
                            Rank = 5,
                            Suit = 1
                        },
                        new
                        {
                            ID = 19,
                            Rank = 6,
                            Suit = 1
                        },
                        new
                        {
                            ID = 20,
                            Rank = 7,
                            Suit = 1
                        },
                        new
                        {
                            ID = 21,
                            Rank = 8,
                            Suit = 1
                        },
                        new
                        {
                            ID = 22,
                            Rank = 9,
                            Suit = 1
                        },
                        new
                        {
                            ID = 23,
                            Rank = 10,
                            Suit = 1
                        },
                        new
                        {
                            ID = 24,
                            Rank = 11,
                            Suit = 1
                        },
                        new
                        {
                            ID = 25,
                            Rank = 12,
                            Suit = 1
                        },
                        new
                        {
                            ID = 26,
                            Rank = 13,
                            Suit = 1
                        },
                        new
                        {
                            ID = 27,
                            Rank = 1,
                            Suit = 3
                        },
                        new
                        {
                            ID = 28,
                            Rank = 2,
                            Suit = 3
                        },
                        new
                        {
                            ID = 29,
                            Rank = 3,
                            Suit = 3
                        },
                        new
                        {
                            ID = 30,
                            Rank = 4,
                            Suit = 3
                        },
                        new
                        {
                            ID = 31,
                            Rank = 5,
                            Suit = 3
                        },
                        new
                        {
                            ID = 32,
                            Rank = 6,
                            Suit = 3
                        },
                        new
                        {
                            ID = 33,
                            Rank = 7,
                            Suit = 3
                        },
                        new
                        {
                            ID = 34,
                            Rank = 8,
                            Suit = 3
                        },
                        new
                        {
                            ID = 35,
                            Rank = 9,
                            Suit = 3
                        },
                        new
                        {
                            ID = 36,
                            Rank = 10,
                            Suit = 3
                        },
                        new
                        {
                            ID = 37,
                            Rank = 11,
                            Suit = 3
                        },
                        new
                        {
                            ID = 38,
                            Rank = 12,
                            Suit = 3
                        },
                        new
                        {
                            ID = 39,
                            Rank = 13,
                            Suit = 3
                        },
                        new
                        {
                            ID = 40,
                            Rank = 1,
                            Suit = 2
                        },
                        new
                        {
                            ID = 41,
                            Rank = 2,
                            Suit = 2
                        },
                        new
                        {
                            ID = 42,
                            Rank = 3,
                            Suit = 2
                        },
                        new
                        {
                            ID = 43,
                            Rank = 4,
                            Suit = 2
                        },
                        new
                        {
                            ID = 44,
                            Rank = 5,
                            Suit = 2
                        },
                        new
                        {
                            ID = 45,
                            Rank = 6,
                            Suit = 2
                        },
                        new
                        {
                            ID = 46,
                            Rank = 7,
                            Suit = 2
                        },
                        new
                        {
                            ID = 47,
                            Rank = 8,
                            Suit = 2
                        },
                        new
                        {
                            ID = 48,
                            Rank = 9,
                            Suit = 2
                        },
                        new
                        {
                            ID = 49,
                            Rank = 10,
                            Suit = 2
                        },
                        new
                        {
                            ID = 50,
                            Rank = 11,
                            Suit = 2
                        },
                        new
                        {
                            ID = 51,
                            Rank = 12,
                            Suit = 2
                        },
                        new
                        {
                            ID = 52,
                            Rank = 13,
                            Suit = 2
                        });
                });

            modelBuilder.Entity("SpeedWar.Models.Deck", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DeckType");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Decks");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            DeckType = 0,
                            UserID = 1
                        },
                        new
                        {
                            ID = 2,
                            DeckType = 1,
                            UserID = 2
                        },
                        new
                        {
                            ID = 3,
                            DeckType = 2,
                            UserID = 2
                        },
                        new
                        {
                            ID = 4,
                            DeckType = 1,
                            UserID = 3
                        },
                        new
                        {
                            ID = 5,
                            DeckType = 2,
                            UserID = 3
                        },
                        new
                        {
                            ID = 6,
                            DeckType = 1,
                            UserID = 4
                        },
                        new
                        {
                            ID = 7,
                            DeckType = 2,
                            UserID = 4
                        },
                        new
                        {
                            ID = 8,
                            DeckType = 1,
                            UserID = 5
                        },
                        new
                        {
                            ID = 9,
                            DeckType = 2,
                            UserID = 5
                        },
                        new
                        {
                            ID = 10,
                            DeckType = 1,
                            UserID = 6
                        },
                        new
                        {
                            ID = 11,
                            DeckType = 2,
                            UserID = 6
                        });
                });

            modelBuilder.Entity("SpeedWar.Models.DeckCard", b =>
                {
                    b.Property<int>("CardID");

                    b.Property<int>("DeckID");

                    b.HasKey("CardID", "DeckID");

                    b.HasIndex("DeckID");

                    b.ToTable("DeckCards");
                });

            modelBuilder.Entity("SpeedWar.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CurrentUserID");

                    b.Property<bool>("EmptyDecks");

                    b.Property<int?>("FirstCardID");

                    b.Property<string>("Name");

                    b.Property<bool>("PlayerTurn");

                    b.Property<int?>("SecondCardID");

                    b.HasKey("ID");

                    b.HasIndex("CurrentUserID");

                    b.HasIndex("FirstCardID");

                    b.HasIndex("SecondCardID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            EmptyDecks = false,
                            Name = "Discard",
                            PlayerTurn = false
                        },
                        new
                        {
                            ID = 2,
                            EmptyDecks = false,
                            Name = "Computer",
                            PlayerTurn = false
                        },
                        new
                        {
                            ID = 3,
                            EmptyDecks = false,
                            Name = "Clarice",
                            PlayerTurn = false
                        },
                        new
                        {
                            ID = 4,
                            EmptyDecks = false,
                            Name = "Shalom",
                            PlayerTurn = false
                        },
                        new
                        {
                            ID = 5,
                            EmptyDecks = false,
                            Name = "Xia",
                            PlayerTurn = false
                        },
                        new
                        {
                            ID = 6,
                            EmptyDecks = false,
                            Name = "Gwen",
                            PlayerTurn = false
                        });
                });

            modelBuilder.Entity("SpeedWar.Models.Deck", b =>
                {
                    b.HasOne("SpeedWar.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SpeedWar.Models.DeckCard", b =>
                {
                    b.HasOne("SpeedWar.Models.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SpeedWar.Models.Deck", "Deck")
                        .WithMany()
                        .HasForeignKey("DeckID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SpeedWar.Models.User", b =>
                {
                    b.HasOne("SpeedWar.Models.User", "CurrentUser")
                        .WithMany()
                        .HasForeignKey("CurrentUserID");

                    b.HasOne("SpeedWar.Models.Card", "FirstCard")
                        .WithMany()
                        .HasForeignKey("FirstCardID");

                    b.HasOne("SpeedWar.Models.Card", "SecondCard")
                        .WithMany()
                        .HasForeignKey("SecondCardID");
                });
#pragma warning restore 612, 618
        }
    }
}
