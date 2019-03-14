'use strict'
console.log("I'm connected");

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

    //console.log(card1Rank);
  
    //var li1 = document.getElementById("li1");
    //var li2 = document.getElementById("li2");
    //var li3 = document.getElementById("li3");
    //var li4 = document.getElementById("li4");

    var DOM_img = document.getElementById("img1");
    DOM_img.src = card1Suit;

    var dom_img = document.getElementById("img2");
    dom_img.src = card2Suit;

    //li1.textContent = card1Rank;
    //li2.textContent = card1Suit;
    //li3.textContent = card2Rank;
    //li4.textContent = card2Suit;

    if (card1Rank === card2Rank) {
        match = true;
    }

})

connection.start().then(function () {
    event.preventDefault(); 
    document.getElementById("sendButton").disabled = false;
    userName = document.getElementById("player").textContent;
})
    .catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    console.log("did a thing")
    event.preventDefault();
 });


document.getElementById("first-card").addEventListener("click", function (event) {
    event.preventDefault();
    slap =true;
    console.log("player slap");
    console.log("slap: ", slap);
    console.log("match: ", match);
    if (match === true) {
        connection.invoke("Slap", userName, userName);
        match = false;
    }
    slap = false;
});


document.getElementById("userDeck").addEventListener("click", function (event) {
    console.log("been clicked");
    event.preventDefault();
    slap = false;
    //var secondRank = document.getElementById("li1").textContent;
    //var secondSuit = document.getElementById("li2").textContent;
    //console.log(secondSuit);
    console.log("match: ", match);
    console.log("slap: ", slap);
    if (!match) {
        playerFlip();
    } else if (match && !slap) {
        compSlap();
    }
    compFlip();
    if (match && !slap) {
        compSlap();
    }
});

function compFlip() {
    console.log("compflip")
    if (compDecksEmpty) {
        console.log("comp decks empty");
    }
    else if (!match || (match && !slap)) {
        console.log("inside compflip");
        connection.invoke("ComputerFlip", userName).catch(function (err) {
            return console.error(err.toString());
        })
    }
};

function compSlap() {
    console.log("compslap");

    connection.invoke("Slap", userName, "computer").catch(function (err) {
        return console.error(err.toString());
    });
    match = false;
};

function playerFlip() {
    console.log("playerflip");
    connection.invoke("PlayerFlip", userName).catch(function (err) {
        return console.error(err.toString());
    })
};

