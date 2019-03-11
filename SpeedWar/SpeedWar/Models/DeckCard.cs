using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWar.Models
{
    public class DeckCard
    {
        public int CardID { get; set; }
        public int DeckID { get; set; }

        // Navigation Properties
        public Card Card { get; set; }
        public Deck Deck { get; set; }
    }
}
