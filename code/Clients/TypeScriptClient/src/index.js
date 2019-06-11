"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
require("./css/main.css");
require("bootstrap");
require("./scss/app.scss");
var signalR = require("@aspnet/signalr");
var divMessages = document.querySelector("#divMessages");
var tbMessage = document.querySelector("#tbMessage");
var btnSend = document.querySelector("#btnSend");
var username = new Date().getTime();
var btnConnect = document.querySelector('#btnConnect');
var btnDisconnect = document.querySelector('#btnDisconnect');
var divContainer = document.querySelector("#divContainer");
var connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:59207/chathub")
    .build();
var ConnectionStarted = false;
function getConnected() {
    connection.start().then(function () {
        alert("connected to hub!");
        ConnectionStarted = true;
    }).catch(function (err) { return document.write(err); });
}
function closeConnection() {
    connection.stop().then(function () {
        ConnectionStarted = false;
        alert("hub disconnected!");
    }).catch(function (err) { return document.write(err); });
}
btnConnect.addEventListener("click", getConnected);
btnDisconnect.addEventListener("click", closeConnection);
connection.on("messageReceived", function (username, message) {
    var messageContainer = document.createElement("div");
    messageContainer.innerHTML =
        "<div class=\"message-author\">" + username + "</div><div>" + message + "</div>";
    divMessages.appendChild(messageContainer);
    divMessages.scrollTop = divMessages.scrollHeight;
});
tbMessage.addEventListener("keyup", function (e) {
    if (e.keyCode === 13) {
        send();
    }
});
btnSend.addEventListener("click", send);
function send() {
    if (ConnectionStarted) {
        connection.send("newMessage", username, tbMessage.value)
            .then(function () { return tbMessage.value = ""; });
    }
    else {
        alert("hub not connected!");
    }
}
window.onload = function () {
    divContainer.className = "container";
};
//# sourceMappingURL=index.js.map