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

        /// <summary>
        /// collects and returns a specified deck as a list
        /// </summary>
        /// <param name="userID"> deck owner's ID </param>
        /// <param name="deckType"> deck type (ie - 'play', 'collect', 'discard' </param>
        /// <returns> card deck, formatted as list </returns>
        public async Task<List<DeckCard>> GetDeck(int userID, DeckType deckType)
        {
            Deck deck = await _context.Decks.FirstOrDefaultAsync(d => d.UserID == userID && d.DeckType == deckType);
            List<DeckCard> cardDeck = await _context.DeckCards.Where(d => d.DeckID == deck.ID).ToListAsync();
            return cardDeck;
        }

        /// <summary>
        /// returns a random card from specified deck
        /// simulates returning top card from a shuffled deck
        /// </summary>
        /// <param name="userID"> deck owner's ID </param>
        /// <param name="deckType"> deck type (ie - 'play', 'collect', 'discard' </param>
        /// <returns> randomly selected card from specified deck </returns>
        public async Task<DeckCard> GetCard(int userID, DeckType deckType)
        {
            List<DeckCard> cardDeck = await GetDeck(userID, deckType);
            Random random = new Random();
            int rnd = random.Next(0, cardDeck.Count - 1);
            DeckCard deckCard = cardDeck[rnd];
            return deckCard;
        }

        /// <summary>
        /// updates the deck-location of a card
        /// </summary>
        /// <param name="deckCard"> new card-deck assignment </param>
        /// <returns> completed task </returns>
        public async Task UpdateDeckCard(DeckCard deckCard)
        {
            DeckCard query = await _context.DeckCards.FirstOrDefaultAsync(d => d.CardID == deckCard.CardID && d.DeckID != deckCard.DeckID);
            if(query != null)
            {
                _context.DeckCards.Remove(query);
            }
            await _context.DeckCards.AddAsync(deckCard);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// gets all cards and returns as a list
        /// </summary>
        /// <returns> list of all cards </returns>
        public async Task<List<Card>> GetAllCardsAsync()
        {
            return await _context.Cards.ToListAsync();
        }

        /// <summary>
        /// 'deals' a new game by splitting all cards randomly between player and computer
        /// </summary>
        /// <param name="ID"> player's UserID </param>
        /// <returns> task completed </returns>
        public async Task DealGameAsync(int ID /*, int ID2 */) // de-comment to add 2nd player
        {
            List<Card> cards = await GetAllCardsAsync();
            Random random = new Random();
            int rnd;
            Deck player = await _context.Decks.FirstOrDefaultAsync(d => d.UserID == ID && d.DeckType == DeckType.Play);
            Deck computer = await _context.Decks.FirstOrDefaultAsync(d => d.UserID == 2 && d.DeckType == DeckType.Play);
            await CleanDeck(player);
            await CleanDeck(computer);
            Deck current = player;
            while (cards.Count > 0)
            {
                rnd = random.Next(0, cards.Count - 1);
                await _context.DeckCards.AddAsync(new DeckCard() { CardID = cards[rnd].ID, DeckID = current.ID });
                await _context.SaveChangesAsync();
                DeckCard test = await _context.DeckCards.FirstOrDefaultAsync(c => c.CardID == cards[rnd].ID && c.DeckID == current.ID);
                cards.Remove(cards[rnd]);
                current = (current == player) ? computer : player;
            }
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Takes in a deck, clears the cards out of the deck.
        /// </summary>
        /// <param name="deck">Any Deck</param>
        /// <returns>No return, saves changes</returns>
        public async Task CleanDeck(Deck deck)
        {
            var cards = await GetDeck(deck.UserID, deck.DeckType);
            foreach (var card in cards)
            {
                _context.DeckCards.Remove(card);
            }
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// compares ranks of top 2 cards in discard pile
        /// returns 'true' if matching, returns 'false' if not matching
        /// </summary>
        /// <param name="last"> 2nd card in discard pile </param>
        /// <param name="next"> top card in discard pile </param>
        /// <returns> 'true' if last.Rank matches next.Rank, 'false' otherwise </returns>
        public bool CompareCards(Card last, Card next)
        {
            if (last.Rank == next.Rank)
            {
                return true;
            }
            return false;
        }


        public async Task Flip(int ID)
        {
            var check = await GetDeck(ID, DeckType.Play);
            if (check.Count == 0)
            {
                EndGame(ID);
            }
            DeckCard deckCard = await GetCard(ID, DeckType.Play);
            deckCard.DeckID = 1;
            await UpdateDeckCard(deckCard);
        }

        private void EndGame(int ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Moves all cards from specified player's 'Collect' deck to same player's 'Play' deck
        /// (used to reset decks when 'Play' deck runs empty)
        /// </summary>
        /// <param name="ID"> User ID of player whose decks need reset </param>
        /// <returns> completed task </returns>
        public async Task ResetDecks(int ID)
        {
            List<DeckCard> collect = await GetDeck(ID, DeckType.Collect);
            int playID = (await _context.Decks.FirstOrDefaultAsync(d => d.UserID == ID)).ID;
            DeckCard temp = new DeckCard();
            foreach (DeckCard card in collect)
            {
                temp.CardID = card.CardID;
                temp.DeckID = playID;
                await UpdateDeckCard(temp);
            }
        }
    }
}
