'use strict'

var connection = new signalR.HubConnectionBuilder().withUrl("/PlayHub").build();

document.getElementById("sendButton").disabled = true;

connection.on("Recieve Card", function (card1Rank, card1Suit, card2Rank, card2Suit) {
    var firstChild = document.getElementById("first-card");
    var secondChild = document.GetElementByID("second-card");
    var card1 = card1Rank + card1Suit;
    var card2 = card2Rank + card2Suit;
    var card1Element = document.createElement("span");
    card1Element.textContent(card1);
    var card2Element = document.createElement("span");
    card2Element.textContent(card2);

    firstChild.appendChild(card1Element);
    secondChild.appendChild(card2Element);
})

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    connection.Invoke("SendCard").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
