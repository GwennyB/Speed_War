using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWar.Models
{
    public class Card
    {
        public int ID { get; set; }
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }
        public string ImageURL { get; set; }
    }

    public enum Suit
    {
        hearts,
        spades,
        diamonds,
        clubs
    }

    public enum Rank
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13
    }
}
