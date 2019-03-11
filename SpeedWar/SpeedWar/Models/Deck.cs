using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWar.Models
{
    public class Deck
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public DeckType DeckType { get; set; }

        // Navigation Properties
        public User User { get; set; }
    }

    public enum DeckType
    {
        Discard,
        Play,
        Collect

    }
}
