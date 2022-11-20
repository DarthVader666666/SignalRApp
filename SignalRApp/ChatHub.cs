using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SignalRApp
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task Send(string message, string to)
        {
            var userName = Context.User.Identity.Name;

            if (Context.UserIdentifier != to)
            {
                await Clients.User(Context.UserIdentifier).SendAsync("Receive", message, userName);
            }

            await Clients.Caller.SendAsync("Receive", message, userName);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify", $"Hello {Context.UserIdentifier}");
            await base.OnConnectedAsync();
        }
    }
}
