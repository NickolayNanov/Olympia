"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", () => {
    document.getElementById("messageInput").value = "";
});

connection.on("ReceiveMessage", function (currentUser, message) {

    isFinished = true;
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = currentUser + ": " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    li.classList.add(["list-group-item"])
    document.getElementById("messagesList").appendChild(li);

});

let isFinished = false;

connection.on("LoadMessage", (currentUser, message) => {

    let ul = document.getElementById("messagesList").getElementsByTagName("li")
    if (isFinished != false) {
        if (ul.length != 0) {
            return;
        }
    }

    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = currentUser + ": " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    li.classList.add(["list-group-item"])
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});