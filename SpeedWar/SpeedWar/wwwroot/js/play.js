'use strict'
console.log("I'm connected");

var connection = new signalR.HubConnectionBuilder().withUrl("/PlayHub").build();
var pauseGame = true;
document.getElementById("easy-button").disabled = true;
document.getElementById("hard-button").disabled = true;
var slap = false;
var match = false;
var userName;
var holdTime;
var staleTest = "";
var staleCount = 0;
var stale = false;

// game initial setup
// turns off game listeners
connection.start().then(function () {
    event.preventDefault();
    document.getElementById("first-card").disabled = true;
    document.getElementById("userdeck").disabled = true;
    document.getElementById("easy-button").disabled = false;
    document.getElementById("hard-button").disabled = false;
    userName = document.getElementById("player").textContent;})
    .catch(function (err) {
        return console.error(err.toString());
    });


connection.on("ReceiveCard", function (card1Rank, card1Img, card2Rank, card2Img) {
    event.preventDefault(); 

    var DOM_img = document.getElementById("img1");
    DOM_img.src = card1Img;

    var dom_img = document.getElementById("img2");
    dom_img.src = card2Img;


    if (card1Rank === card2Rank) {
        match = true;
    }
    console.log(`match: ${match}, slap: ${slap}`);
    console.log(`#1: ${card1Rank}, #2: ${card2Rank}`);
    setTimeout(function () {
        checkSlap();
    }, holdTime);
})


document.getElementById("easy-button").addEventListener("click", function (event) {
    event.preventDefault();
    holdTime = 1500;
    document.getElementById("first-card").disabled = false;
    document.getElementById("userdeck").disabled = false;
    document.getElementById("easy-button").disabled = true;
    document.getElementById("hard-button").disabled = true;
    $(".diff-button").toggleClass("hidden");
});

document.getElementById("hard-button").addEventListener("click", function (event) {
    event.preventDefault();
    holdTime = 500;
    document.getElementById("first-card").disabled = false;
    document.getElementById("second-card").disabled = false;
    document.getElementById("easy-button").disabled = true;
    document.getElementById("hard-button").disabled = true;
 });

document.getElementById("first-card").addEventListener("click", function (event) {
    event.preventDefault();
    slap = true;
    $("#slap-page").toggleClass("hidden");
    console.log("player slap");
    if (match === true) {
        connection.invoke("Slap", userName, userName);
        match = false;
    }
    slap = false;
    setTimeout(function () {
        $("#slap-page").toggleClass("hidden");
    }, holdTime*5);
});


document.getElementById("userdeck").addEventListener("click", function (event) {
    event.preventDefault();
    console.log("user flip");
    var DOM_img = document.getElementById("img1");
    if (staleTest === DOM_img.src) {
        staleCount++;
        console.log("staleCount: ", staleCount);
    }
    else {
        staleTest = DOM_img.src;
    }
    if (staleCount > 2) {
        console.log("stalemate");
        endGame("Nobody");
    }
    slap = false;
    playerFlip();
    setTimeout(function () {
        compFlip();
    }, holdTime);
});

function compFlip() {
    console.log("comp flip")
    setTimeout(function () {
        console.log("inside compflip");
        connection.invoke("ComputerFlip", userName).catch(function (err) {
            return console.error(err.toString());
        })
    }, holdTime);
};

function checkSlap() {
    console.log("check slap");
    setTimeout(function () {
        if (match && !slap) {
            console.log("line 80");
            compSlap();
        }
    }, holdTime);
}


function compSlap() {
    console.log("comp slap");
    $("#slap-page").toggleClass("hidden");
    connection.invoke("Slap", userName, "computer").catch(function (err) {
        return console.error(err.toString());
    });
    setTimeout(function () {
        $("#slap-page").toggleClass("hidden");
    }, holdTime*5);

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
    endGame(winner);
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

function endGame(winner) {
    console.log(`winner is ${winner}`);
    document.getElementById("first-card").disabled = true;
    document.getElementById("userdeck").disabled = true;
    document.getElementById("easy-button").disabled = false;
    document.getElementById("hard-button").disabled = false;
    document.getElementById("winner-id").textContent = winner;
    $("#winner-page").toggleClass("hidden");
}





