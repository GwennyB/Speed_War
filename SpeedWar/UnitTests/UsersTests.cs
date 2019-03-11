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
        [Fact]
        public void IDGetSet()
        {
            User user = new User();
            user.ID = 1;
            Assert.Equal(1, user.ID);
        }

        [Fact]
        public void NameGetSet()
        {
            User user = new User();
            user.Name = "Test";
            Assert.Equal("Test", user.Name);
        }

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
    }
}
