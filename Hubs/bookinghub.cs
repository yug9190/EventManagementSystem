using Microsoft.AspNetCore.SignalR;

namespace EventManagementSystem.Hubs
{
    /// <summary>
    /// SignalR Hub is a tool that handles real-time messages.
    /// This hub pushes booking updates to all connected clients.
    /// </summary>
    public class BookingHub : Hub
    {
        // Optional: You can override OnConnectedAsync or OnDisconnectedAsync
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "System", "You are now connected to BookingHub.");
            await base.OnConnectedAsync();
        }

        // Optional: Broadcast a booking message to all clients
        public async Task BroadcastBookingUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveBookingUpdate", message);
        }

        // You can also target specific users or groups if needed
    }
}
