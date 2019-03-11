using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWar.Models.Interfaces
{
    interface IDeckCardManager
    {
        Task<Stack<DeckCard>> GetDeck();
        Task<DeckCard> GetCard();
        Task UpdateDeckCard();

    }
}
