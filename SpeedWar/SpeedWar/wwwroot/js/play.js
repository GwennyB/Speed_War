'use strict'

var connection = new signalR.HubConnectionBuilder().withUrl("/PlayHub").build();
var player = document.getElementById("player").textContent;
connection.invoke("Intro", player);
var playerTurn = true;
document.getElementById("sendButton").disabled = true;


connection.on("RecieveCard", function (card1Rank, card1Suit, card2Rank, card2Suit) {
    console.log(card1Rank);
  
    var li1 = document.getElementById("li1");
    var li2 = document.getElementById("li2");
    var li3 = document.getElementById("li3");
    var li4 = document.getElementById("li4");

    li1.textContent = card1Rank
    li2.textContent = card1Suit
    li3.textContent = card2Suit
    li4.textContent = card2Rank
})

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    var player = document.getElementById("player").textContent;
    connection.invoke("Intro", player);
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    playerTurn = true;
    var userName = document.getElementById("player").textContent;
    var secondRank = document.getElementById("li1").textContent;
    var secondSuit = document.getElementById("li2").textContent;
    console.log(secondSuit);
    connection.invoke("PlayerFlip", secondRank, secondSuit, userName).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault(); 
});


