using SpeedWar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

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
    }
}
