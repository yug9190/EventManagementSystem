"use strict";

// Create a SignalR connection to the hub
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/bookingHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

// Start the connection
connection.start().then(() => {
    console.log("✅ Connected to BookingHub via SignalR");
}).catch(err => {
    console.error("❌ SignalR Connection Error: ", err.toString());
});

// Listen for booking updates from the server
connection.on("ReceiveBookingUpdate", function (message) {
    console.log("📡 Booking Update Received:", message);

    // Display a toast or update DOM
    const notification = document.getElementById("bookingNotification");
    if (notification) {
        notification.innerText = message;
        notification.classList.remove("d-none");
    } else {
        alert("New Booking Update: " + message);
    }
});
