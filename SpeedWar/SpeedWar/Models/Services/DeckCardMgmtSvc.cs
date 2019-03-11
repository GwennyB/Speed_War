using SpeedWar.Data;
using SpeedWar.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedWar.Models.Services
{
    public class DeckCardMgmtSvc : IDeckCardManager
    {
        private CardDbContext _context { get; }

        public DeckCardMgmtSvc(CardDbContext context)
        {
            _context = context;
        }

        public async Task<List<DeckCard>> GetDeck(int userID)
        {
            Deck deck = _context.Decks.FirstOrDefault(d => d.UserID == userID);
            List<DeckCard> cardDeckRaw = _context.DeckCards.ToList();
            List<DeckCard> cardDeck = cardDeckRaw.Where(d => d.DeckID == deck.ID).ToList();
            return cardDeck;
        }

        public Task<DeckCard> GetCard()
        {
            throw new NotImplementedException();
        }

        public Task UpdateDeckCard()
        {
            throw new NotImplementedException();
        }
    }
}
