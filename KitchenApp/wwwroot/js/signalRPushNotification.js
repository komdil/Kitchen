"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/pushNotificationHub").build();
connection.on("Recive", function (notification) {
    console.log(notification);
    $("#selectedMenu").text(notification.selectedMenu);
    $("#message").text(notification.message);
    $('#modalWindow').css({ "display":"block"});
});


connection.on("Connect", function (connectionId) {
    $("#CallerConnectionId").val(connectionId);
});


connection.start().catch(function (err) {
    return console.error(err.toString());
});


$("#btnOk").on("click", function () {
    $('#modalWindow').css({ "display": "none" });

});
$("#btnClose").on("click", function () {
    $('#modalWindow').css({ "display": "none" });

});