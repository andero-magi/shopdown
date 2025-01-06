"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

const sendbutton = document.getElementById("sendbutton")
const messageIn = document.getElementById("messageinput")
const userIn = document.getElementById("userinput")
const messageList = document.getElementById("messagelist")

sendbutton.disable = true

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li")
    li.textContent = `${user}: ${message}`
    messageList.appendChild(li)
})

connection.start().then(function () {
    sendbutton.disabled = false
}.catch(function (err) {
    return console.error(err.toString())
}

sendbutton.addEventListener("click", function (event) {
    connection.invoke("SendMessage", userIn.value, messageIn.value).catch(function (err) {
        return console.error(err.toString())
    })
    event.preventDefault()
};