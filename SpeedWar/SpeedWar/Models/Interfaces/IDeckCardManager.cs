using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWar.Models.Interfaces
{
    public interface IDeckCardManager
    {
        Task<List<DeckCard>> GetDeck(int userID, DeckType deckType);
        Task<DeckCard> GetCard(int userID, DeckType deckType);
        Task UpdateDeckCard(DeckCard deckCard);
        Task DealGameAsync(int ID);
        Task<List<Card>> GetAllCardsAsync();
        Task Flip(int ID);

    }
}
