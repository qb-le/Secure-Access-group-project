using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Logic.Classes;

namespace Logic.Classes
{
    public class AccessHub : Hub
    {
        // When a new connection is made
        public override async Task OnConnectedAsync()
        {
            // Add everyone to the Receptionists group if they are receptionists
            // (optional: check some claim or role)
            await Groups.AddToGroupAsync(Context.ConnectionId, "Receptionists");
            await base.OnConnectedAsync();
        }

        // Add an employee to their own group by email
        public async Task RegisterEmployee(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, email);
            }
        }

        // Notify receptionists of a new access request
        public async Task SendRequest(Request request)
        {
            await Clients.Group("Receptionists").SendAsync("ReceiveNotification", request);
        }

        // Send a real-time access decision to an employee
        public async Task NotifyEmployee(string email, string message)
        {
            if (!string.IsNullOrEmpty(email))
            {
                await Clients.Group(email).SendAsync("ReceiveAccessNotification", message);
            }
        }
    }
}
