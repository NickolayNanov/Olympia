"use strict";

let connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

connection.on("ReceiveMessage", () => {
    document.getElementById("messageInput").value = "";
});

connection.on("ReceiveMessage", function (currentUser, message) {

    let msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    let encodedMsg = currentUser + ": " + msg;
    let li = document.createElement("li");
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

    let msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    let encodedMsg = currentUser + ": " + msg;
    let li = document.createElement("li");

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

connection.start().then(function () {
    document.getElementById("loadPreviousMessages").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("loadPreviousMessages").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    connection.invoke("LoadPreviousMessages", user).catch(function (err) {
        return console.error(err.toString());
    }).then(() => {
        isFinished = true;
    });

    event.preventDefault();
});