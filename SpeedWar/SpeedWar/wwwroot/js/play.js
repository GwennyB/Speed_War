'use strict'

var connection = new signalR.HubConnectionBuilder().withUrl("/PlayHub").build();

document.getElementById("sendButton").disabled = true;

connection.on("RecieveCard", function (card1Rank, card1Suit, card2Rank, card2Suit) {
    console.log(card1Rank);
    var li1 = document.createElement("li");
    var li2 = document.createElement("li");

    li1.textContent = card1Rank
    li2.textContent = card2Rank

    var li3 = document.createElement("li");
    var li4 = document.createElement("li");

    li3.textContent = card1Suit
    li4.textContent = card2Suit
    document.getElementById("first-card").appendChild(li1)
    document.getElementById("second-card").appendChild(li2)
})

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {


    connection.invoke("PlayerFlip").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();


});