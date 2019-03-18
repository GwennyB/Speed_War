using SpeedWar.Data;
using SpeedWar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using SpeedWar.Models.Services;

namespace UnitTests
{
    public class UsersTests
    {

        /// <summary>
        /// getter-ID
        /// </summary>
        [Fact]
        public void IDGet()
        {
            User user = new User();
            user.ID = 1;
            Assert.Equal(1, user.ID);
        }

        /// <summary>
        /// setter-ID
        /// </summary>
        [Fact]
        public void IDSet()
        {
            User user = new User();
            user.ID = 1;
            user.ID = 2;
            Assert.Equal(2, user.ID);
        }

        /// <summary>
        /// getter-Name
        /// </summary>
        [Fact]
        public void NameGet()
        {
            User user = new User();
            user.Name = "Test";
            Assert.Equal("Test", user.Name);
        }


        /// <summary>
        /// setter-Name
        /// </summary>
        [Fact]
        public void NameSet()
        {
            User user = new User();
            user.Name = "Test";
            user.Name = "NewTest";
            Assert.Equal("NewTest", user.Name);
        }

        /// <summary>
        /// getter-FirstCard
        /// </summary>
        [Fact]
        public void FirstCardGet()
        {
            User user = new User();
            user.FirstCard = 99;
            Assert.Equal(99, user.FirstCard);
        }


        /// <summary>
        /// setter-FirstCard
        /// </summary>
        [Fact]
        public void FirstCardSet()
        {
            User user = new User();
            user.FirstCard = 99;
            user.FirstCard = 9;
            Assert.Equal(9, user.FirstCard);
        }


        /// <summary>
        /// getter-SecondCard
        /// </summary>
        [Fact]
        public void SecondCardGet()
        {
            User user = new User();
            user.SecondCard = 99;
            Assert.Equal(99, user.SecondCard);
        }

        /// <summary>
        /// setter-SecondCard
        /// </summary>
        [Fact]
        public void SecondCardSet()
        {
            User user = new User();
            user.SecondCard = 99;
            user.SecondCard = 19;
            Assert.Equal(19, user.SecondCard);
        }

        /// <summary>
        /// test can get an existing user
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CanGetExistingUser()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("GetExistingUser").Options;

            using (CardDbContext context = new CardDbContext(options))
            {
                UserMgmtSvc svc = new UserMgmtSvc(context);
                await context.Users.AddAsync(new User() { Name = "test" });

                Assert.Equal("test", (await svc.GetUserAsync("test")).Name);
            }
        }

        /// <summary>
        /// can get new user(create account)
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CanGetNewUser()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("GetNewUser").Options;

            using (CardDbContext context = new CardDbContext(options))
            {
                UserMgmtSvc svc = new UserMgmtSvc(context);

                Assert.Equal("test", (await svc.GetUserAsync("test")).Name);
            }
        }

        /// <summary>
        /// test can create play deck
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CanCreatePlayDeck()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("GetNewUser").Options;

            using (CardDbContext context = new CardDbContext(options))
            {
                UserMgmtSvc svc = new UserMgmtSvc(context);

                User user = await svc.GetUserAsync("test");
                var query = await context.Decks.FirstOrDefaultAsync(d => d.UserID == user.ID && d.DeckType == DeckType.Play);
                Assert.NotNull(query);
            }
        }

        /// <summary>
        /// test can create collect deck
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CanCreateCollectDeck()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("GetNewUser").Options;

            using (CardDbContext context = new CardDbContext(options))
            {
                UserMgmtSvc svc = new UserMgmtSvc(context);

                User user = await svc.GetUserAsync("test");
                var query = await context.Decks.FirstOrDefaultAsync(d => d.UserID == user.ID && d.DeckType == DeckType.Collect);
                Assert.NotNull(query);
            }
        }


        /// <summary>
        /// test can update firstcard
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CanUpdateFirstCard()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("TestUserCards").Options;

            using (CardDbContext context = new CardDbContext(options))
            {
                UserMgmtSvc svc = new UserMgmtSvc(context);

                User user = await svc.GetUserAsync("test");
                user.FirstCard = 1;
                await svc.UpdateFirstCard("test", 1);
                Assert.Equal(1, user.FirstCard);
            }
        }

        /// <summary>
        /// test can update second card
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CanUpdateSecondCard()
        {
            DbContextOptions<CardDbContext> options = new DbContextOptionsBuilder<CardDbContext>().UseInMemoryDatabase("TestUserCards").Options;

            using (CardDbContext context = new CardDbContext(options))
            {
                UserMgmtSvc svc = new UserMgmtSvc(context);

                User user = await svc.GetUserAsync("test");
                user.SecondCard = 1;
                await svc.UpdateFirstCard("test", 1);
                Assert.Equal(1, user.SecondCard);
            }
        }

    }
}
