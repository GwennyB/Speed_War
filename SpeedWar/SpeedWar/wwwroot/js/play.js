'use strict'
console.log("I'm connected");

var connection = new signalR.HubConnectionBuilder().withUrl("/PlayHub").build();
var pauseGame = true;
document.getElementById("sendbutton").disabled = true;
var slap = false;
var match = false;
var userName;


connection.on("ReceiveCard", function (card1Rank, card1Suit, card2Rank, card2Suit) {
    event.preventDefault(); 

    var DOM_img = document.getElementById("img1");
    DOM_img.src = card1Suit;

    var dom_img = document.getElementById("img2");
    dom_img.src = card2Suit;


    if (card1Rank === card2Rank) {
        match = true;
    }
    console.log(`match: ${match}, slap: ${slap}`);
    console.log(`#1: ${card1Rank}, #2: ${card2Rank}`);
    setTimeout(function () {
        checkSlap();
    }, 200);
})

connection.start().then(function () {
    event.preventDefault(); 
    document.getElementById("sendbutton").disabled = false;
    userName = document.getElementById("player").textContent;
})
    .catch(function (err) {
    return console.error(err.toString());
});


document.getElementById("sendbutton").addEventListener("click", function (event) {
    console.log("did a thing")
    event.preventDefault();
 });

document.getElementById("userdeck").addEventListener("click", function (event) {
    event.preventDefault();
    slap = true;
    console.log("player slap");
    console.log("slap: ", slap);
    console.log("match: ", match);
    if (match === true) {
        connection.invoke("Slap", userName, userName);
        match = false;
    }
    slap = false;
});


document.getElementById("userdeck").addEventListener("click", function (event) {
    console.log("been clicked");
    event.preventDefault();
    slap = false;
    console.log(`BEFORE PLAYER FLIP`);
    playerFlip();
    console.log(`AFTER PLAYER FLIP`);
    setTimeout(function () {
        compFlip();
    }, 200);
});

function compFlip() {
    console.log("start compflip")
    setTimeout(function () {
        console.log("inside compflip");
        connection.invoke("ComputerFlip", userName).catch(function (err) {
            return console.error(err.toString());
        })
    }, 200);
};

function checkSlap() {
    console.log("start checkslap");
    setTimeout(function () {
        if (match && !slap) {
            console.log("line 80");
            compSlap();
        }
    }, 200);
    console.log("end checkslap");
}

function compSlap() {
    console.log("compslap");

    connection.invoke("Slap", userName, "computer").catch(function (err) {
        return console.error(err.toString());
    });
    match = false;
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
    if (!match) {
        connection.invoke("PlayerFlip", userName).catch(function (err) {
            return console.error(err.toString());
        })
    } else {
        checkSlap();
    }

};

connection.on("endGame", function (winner) {
    event.preventDefault();
    console.log(`winner is ${winner}`);
    document.getElementById("sendbutton").disabled = true;
    document.getElementById("first-card").disabled = true;
    document.getElementById("second-card").disabled = true;
    document.getElementById("winner-page").display = block;
});

connection.on("collectCards", function (playerName, card) {
    console.log("COLLECT CARDS ");
    console.log(playerName);
    console.log(card);
    if (playerName == "computer") {
        document.getElementById("computer-collection").src = card;
    }
    else {
        document.getElementById("user-collection").src = card;
    }
})



