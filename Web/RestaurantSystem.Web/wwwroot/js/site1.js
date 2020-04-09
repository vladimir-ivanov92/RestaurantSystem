// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/restauranthub")
        .build();

    connection.on("ReceiveOrderUpdate", (update) => {
        document.getElementById("status").innerHTML = update;
    });

    connection.on("NewOrder", function (order) {
        document.getElementById("status").innerHTML = "Someone ordered something";
    });

    connection.on("Finished", function () {
        // connection.stop();
    });

    connection.start()
        .catch(err => console.error(err.toString()));
};

setupConnection();

document.getElementById("submit").addEventListener("click", e => {
    e.preventDefault();
    const netAmount = document.getElementById("netAmount").value;

    fetch("/Orders/Order",
        {
            method: "POST",
            body: JSON.stringify({ netAmount }),
            headers: {
                'content-type': 'application/json'
            }
        })
        .then(response => response.text())
        .then(id => connection.invoke("GetUpdateForOrder", parseInt(id)));
});


