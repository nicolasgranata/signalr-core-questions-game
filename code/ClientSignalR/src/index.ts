import "./css/main.css";
import * as $ from "jquery";
import 'bootstrap';
import './scss/app.scss';
import * as signalR from "@aspnet/signalr";

const divMessages: HTMLDivElement = document.querySelector("#divMessages");
const tbMessage: HTMLInputElement = document.querySelector("#tbMessage");
const btnSend: HTMLButtonElement = document.querySelector("#btnSend");
const username = new Date().getTime();
const btnConnect: HTMLButtonElement = document.querySelector('#btnConnect');
const btnDisconnect: HTMLButtonElement = document.querySelector('#btnDisconnect');
const divContainer: HTMLDivElement = document.querySelector("#divContainer");

const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:59207/chathub")
    .build();

let ConnectionStarted: boolean = false;

function getConnected() {
    connection.start().then(function () {
        alert("connected to hub!");
        ConnectionStarted = true;
    }).catch(err => document.write(err));
}

function closeConnection() {
    connection.stop().then(function () {
        ConnectionStarted = false;
        alert("hub disconnected!");
    }).catch(err => document.write(err));
}

btnConnect.addEventListener("click", getConnected);
btnDisconnect.addEventListener("click", closeConnection);

connection.on("messageReceived", (username: string, message: string) => {
    let messageContainer = document.createElement("div");

    messageContainer.innerHTML =
        `<div class="message-author">${username}</div><div>${message}</div>`;

    divMessages.appendChild(messageContainer);
    divMessages.scrollTop = divMessages.scrollHeight;
});

tbMessage.addEventListener("keyup", (e: KeyboardEvent) => {
    if (e.keyCode === 13) {
        send();
    }
});

btnSend.addEventListener("click", send);

function send() {
    if (ConnectionStarted) {
        connection.send("newMessage", username, tbMessage.value)
            .then(() => tbMessage.value = "");
    }
    else {
        alert("hub not connected!");
    }
}

window.onload = function () {
    divContainer.className = "container";
}