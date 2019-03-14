# Introduction 
Speed War emulates the card game of the same name that pits the user (player) against the computer. It uses web sockets (SignalR) to maintain an open connection and constant client-server communication; that is, the user does not need to refresh the page to see game status updates since they are delivered and rendered real-time.
Upon game start, the user and computer begin taking turns flipping cards from their play decks into a common discard pile. When the top 2 cards match in rank, the computer and player can "slap" the card pile to claim all the cards in the pile. The next round begins with the winner flipping the top card of their deck. Game play continues until either the player of the computer is out of cards. 

## Getting Started
### Build
To build and run this page locally (using Visual Studio and SQL Server):
1. Clone the repo locally and compile it.  
2. This application uses User Secrets. Copy this into your secrets.json and replace connection string where indicated:  
    {  
      "ConnectionStrings": {  
        " <--- add your database connection string here ---> "  
      }  
    }  

3. Build the cards database using existing migration (Update-Database).
The application is ready to run via your local/live server or your chosen deployment.  

### Tests
The test suite is built in xUnit. Once the app is built as described above, all tests can be run using the 'Run All' command in the Test Explorer.  
Tests include:
- Getters and Setters of all models.
- CRUD tests of all methods in the User Service and the DeckCard Service.

## Architecture

![db schema](assets/schema.png)



## Credit & Acknowledgement
This project is a collaborative effort by  
  - Clarice Costello: https://github.com/c-costello  
  - Shalom Balaineh: https://github.com/shalina2  
  - Xia Liu: https://github.com/xialiu1988  
  - Gwen Zubatch: https://github.com/GwennyB  

Third party content:  
- [SignalR](https://dotnet.microsoft.com/apps/aspnet/real-time)


## Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

