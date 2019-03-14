'use strict'

var connection = new signalR.HubConnectionBuilder().withUrl("/PlayHub").build();
//var player = document.getElementById("player").textContent;
//connection.invoke("Intro", player);
var playerTurn = true;
document.getElementById("sendButton").disabled = true;


connection.on("ReceiveCard", function (card1Rank, card1Suit, card2Rank, card2Suit) {
    event.preventDefault(); 

    console.log(card1Rank);
  
    var li1 = document.getElementById("li1");
    var li2 = document.getElementById("li2");
    var li3 = document.getElementById("li3");
    var li4 = document.getElementById("li4");

    var DOM_img = document.createElement("img");
    DOM_img.src = card1Suit;

    var dom_img = document.createElement("img");
    dom_img.src = card2Suit;

    li2.appendChild(DOM_img);
    li4.appendChild(dom_img);
    //li1.textContent = card1Rank;
    //li2.textContent = card1Suit;
    //li3.textContent = card2Rank;
    //li4.textContent = card2Suit;
})

connection.start().then(function () {
    event.preventDefault(); 
    document.getElementById("sendButton").disabled = false;
    //var player = document.getElementById("player").textContent;
    //connection.invoke("Intro", player);
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    event.preventDefault(); 

    playerTurn = true;
    var userName = document.getElementById("player").textContent;
    var secondRank = document.getElementById("li1").textContent;
    var secondSuit = document.getElementById("li2").textContent;
    console.log(secondSuit);
    connection.invoke("PlayerFlip", userName).catch(function (err) {
        return console.error(err.toString());
    });
});


