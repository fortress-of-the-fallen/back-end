import * as signalR from '@microsoft/signalr';

// Login with sso
// Open browser
// Connect to SignalR hub

const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5000/hubs/auth") 
    .withAutomaticReconnect()
    .build();

connection.start()
    .then(() => {
        console.log("Connected to SignalR");

        connection.invoke("JoinLoginSessionChannel", "Jx8DmnKqJZvNE-")
            .then(() => {
                console.log("Subscribed successfully");
            })
            .catch(err => {
                console.error("Error while subscribing:", err);
            });
    })
    .catch(err => {
        console.error("Connection failed:", err);
    });

connection.on("Broadcast", (message) => {
    console.log("Received message:", message);
});

