using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Classes
{
    public class AccessHub : Hub
    {
        public async Task SendRequest(Request request)
        {
            await Clients.Group("Receptionists")
                .SendAsync("ReceiveNotification", request);
        }

        public override async Task OnConnectedAsync()
        {
            // Add every new connection to the Receptionists group
            await Groups.AddToGroupAsync(Context.ConnectionId, "Receptionists");
            await base.OnConnectedAsync();
        }
    }
}
