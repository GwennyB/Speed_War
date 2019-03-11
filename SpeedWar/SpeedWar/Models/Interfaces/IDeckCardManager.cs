using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWar.Models.Interfaces
{
    interface IDeckCardManager
    {
        Task<List<DeckCard>> GetDeck(int userID);
        Task<DeckCard> GetCard();
        Task UpdateDeckCard();

    }
}
