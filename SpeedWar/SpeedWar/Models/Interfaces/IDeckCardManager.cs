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
        Task UpdateDeckCard(int cardID, int oldDeckID, int newDeckID);
        Task<List<Card>> GetAllCardsAsync();
        Task DealGameAsync(int ID);
        Task CleanDeck(Deck deck);
        bool CompareCards(Card last, Card next);
        Task<Card> Flip(int ID);
        //void EndGame(int ID);
        Task ResetDecks(int ID);
        Task Slap(int ID);
        Task<User> CheckWinner(int ID);
        Task<bool> EmptyDecks(int ID);
    }
}
