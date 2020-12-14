using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Business.Services.Interfaces;
using System.Collections.Generic;

namespace Web.Models
{
    public class MainHub : Hub
    {
        public MainHub()
        {

        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task ChangeInLocation(int locationId)
        {
            await Clients.All.SendAsync("ChangeInLocation", locationId);
        }

        public async Task ChangeInNotification(int hostId)
        {
            await Clients.All.SendAsync("ChangeInNotification", hostId);
        }

        public async Task SendClientMessage(string message)
        {
            await Clients.All.SendAsync("SendClientMessage", message);
        }
        public async Task<string> GetConnectionId()
        {
            return Context.ConnectionId;
        }

    }
}
