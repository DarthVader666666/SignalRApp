using Microsoft.AspNetCore.SignalR;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SignalRApp
{
    public class ChatHub : Hub
    {
        public async Task Send(string message, string userName)
        { 
            await this.Clients.All.SendAsync("Receive", message, userName);
        }

        public override async Task OnConnectedAsync()
        {
            var context = this.Context.GetHttpContext();

            if (context.Request.Cookies.ContainsKey("name"))
            {
                string userName;

                if (context.Request.Cookies.TryGetValue("name", out userName))
                {
                    Debug.WriteLine($"name = {userName}");
                }
            }

            Debug.WriteLine($"UserAgent = {context.Request.Headers["User-Agent"]}");
            Debug.WriteLine($"RemoteIpAddress = {context.Connection.RemoteIpAddress.ToString()}");

            await this.Clients.All.SendAsync("Notify", $"{this.Context.ConnectionId} entered chat");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await this.Clients.All.SendAsync("Notify", $"{this.Context.ConnectionId} left chat");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
