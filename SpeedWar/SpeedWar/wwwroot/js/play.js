'use strict'

var connection = new signalR.HubConnectionBuilder().withUrl("/PlayHub").build();
var pauseGame = true;
document.getElementById("sendButton").disabled = true;
var slap = false;
var match = false;
var userName;
var userDecksEmpty;
var compDecksEmpty;


connection.on("ReceiveCard", function (card1Rank, card1Suit, card2Rank, card2Suit) {
    event.preventDefault(); 

    console.log(card1Rank);
  
    var li1 = document.getElementById("li1");
    var li2 = document.getElementById("li2");
    var li3 = document.getElementById("li3");
    var li4 = document.getElementById("li4");

    li1.textContent = card1Rank;
    li2.textContent = card1Suit;
    li3.textContent = card2Rank;
    li4.textContent = card2Suit;

    if (card1Rank === card2Rank) {
        match = true;
    }

})

connection.start().then(function () {
    event.preventDefault(); 
    document.getElementById("sendButton").disabled = false;
    userName = document.getElementById("player").textContent;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    event.preventDefault(); 
    var secondRank = document.getElementById("li1").textContent;
    var secondSuit = document.getElementById("li2").textContent;
    console.log(secondSuit);
 });




document.getElementById("first-card").addEventListener("click", function (event) {
    event.preventDefault();
    console.log("player slap");
    console.log("slap: ", slap);
    console.log("match: ", match);
    if (slap === false && match === true) {
        slap = true;
        connection.invoke("Slap", userName, userName);
        match = false;
    }
});


document.getElementById("second-card").addEventListener("click", function (event) {
    event.preventDefault();
    slap = false;
    var secondRank = document.getElementById("li1").textContent;
    var secondSuit = document.getElementById("li2").textContent;
    console.log(secondSuit);
    console.log("match: ", match);
    checkUserDecks(userName)
        .then(function (result) {
            console.log("result: ", result);
            userDecksEmpty = result;
        })

    console.log("userdecksempty: ", userDecksEmpty);
    if (!match && !userDecksEmpty) {
        console.log("inside playerflip");
        connection.invoke("PlayerFlip", userName).catch(function (err) {
            return console.error(err.toString());
        })
    } else if (match) {
        console.log("computer slap");

        connection.invoke("Slap", userName, "computer").catch(function (err) {
            return console.error(err.toString());
        });
        match = false;
    }
    console.log("call compflip")
    compFlip(userName);

});

function compFlip(userName) {
    if (compDecksEmpty) {
        console.log("comp decks empty");
    }
    else if (!match || (match && !slap)) {
        console.log("inside compflip");
        connection.invoke("ComputerFlip", userName).catch(function (err) {
            return console.error(err.toString());
        })
    }
}

function checkUserDecks() {

}

//connection.on("CompSlap", function () {
//    event.preventDefault();

//    slap = true;

//});




