# Requirements

## Vision
 Tyranny of War is a "speed" War card game that pits the user(player) against the computer. It is designed to be a demonstration of SignalR.  
 The basic idea is upon start of the game the user and computer begin taking turns flipping cards from their deck into a discard pile. When matching cards are flipped up, the computer and player can "slap" the card pile. Whoever slaps first gets the cards in the pile. The next round begins with the winner flipping the top card of their deck. The game continues until either the player of the computer is out of cards.

## Scope
### In Scope
- A simple "speed" war game including flipping a deck of cards.
- Users have the ability to start a game against the computer.
- Basic game play including flipping and slapping cards.
- At the end of the game, the user is told if they won or lost, and is given an option to start another game.
### Out of Scope
- Will not become a gambling game.
- No paid access.

## MVP
- A user can start a game.
- User has the ability to flip cards from their deck, or slap the discard pile.
- Computer flips cards on a timer, and listens for matching cards using SignalR. 
- If two cards match, the computer "slaps" the pile after a defined delay.
- At the end of the game, a winner is declared. 

## Stretch
- Add ability to change difficulty, that changes the computer's set timing.
- Add ability to add multiple players.
- Add a "score". Tell the player how many games against the computer they have won or lost.

## Functional Requirements
- Upon start of the game, a deck of 52 cards instantiated and dealt between player and computer.
- Cards can be placed into the discard pile, play pile, or collect pile. Those piles can belong to a user (player and computer) or no one (the discard pile).
- The computer listens for matches and "slaps" using SignalR.
- Repository Design Pattern is utilized.
- Razor Pages are utilized.

## Non-Functional Requirements
- Testability -  Full Coverage tests of getters and setters, and crud capabilities.
- Accessibility - The application should be easy to use for everyone, including children. Font and buttons should be large, and easy to read and/or click. A user should be able to understand game play after playing through one game.

## Data-Flow
- User lands on a start page. Upon clicking "Start" the user is directed to the game page.
- Once the user lands on the game page, a deck is dealt between the user and computer.
- To start game play, the user "flips" the top card of their deck. This triggers the computer to start listening for matches, and to begin flipping cards ever x seconds. 
- As the user and computer flip cards, if a match of two cards appears, the user has the ability to "slap" the card. If the user slaps the card first, the discard deck is added to the player deck. After x seconds, if the player has not slapped the card, the computer will "slap" the deck and take the discard pile into it's deck.
- The next round begins with the winner of the previous round flipping a card.
- The game continues until the user or computer is out of cards.
- At the end of the game, a winner or loser is declared. 