using Microsoft.EntityFrameworkCore;
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

        public async Task<List<DeckCard>> GetDeck(int userID, DeckType deckType)
        {
            Deck deck = await _context.Decks.FirstOrDefaultAsync(d => d.UserID == userID && d.DeckType == deckType);
            List<DeckCard> cardDeckRaw = await _context.DeckCards.ToListAsync();
            List<DeckCard> cardDeck = cardDeckRaw.Where(d => d.DeckID == deck.ID).ToList();
            return cardDeck;
        }

        public async Task<DeckCard> GetCard(int userID, DeckType deckType)
        {
            List<DeckCard> cardDeck = await GetDeck(userID, deckType);
            DeckCard deckCard = cardDeck.First();
            return deckCard;
        }

        public async Task UpdateDeckCard(DeckCard deckCard)
        {
            _context.DeckCards.Update(deckCard);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Card>> GetAllCardsAsync()
        {
            return await _context.Cards.ToListAsync<Card>();
        }

        public async Task DealGameAsync(int ID /*, int ID2 */) // de-comment to add 2nd player
        {
            List<Card> cards = await GetAllCardsAsync();
            Random random = new Random();
            int rnd;
            Deck player = await _context.Decks.FirstOrDefaultAsync(d => d.UserID == ID && d.DeckType == DeckType.Play);
            Deck computer = await _context.Decks.FirstOrDefaultAsync(d => d.UserID == 1 && d.DeckType == DeckType.Play);
            Deck current = player;
            while (cards.Count > 0)
            {
                rnd = random.Next(0, cards.Count - 1);
                await _context.DeckCards.AddAsync(new DeckCard() { CardID = cards[rnd].ID, DeckID = current.ID });
                cards.Remove(cards[rnd]);
                current = (current == player) ? computer : player;
            }
        }

    }
}
