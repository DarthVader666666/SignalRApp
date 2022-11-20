using Microsoft.AspNetCore.SignalR;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SignalRApp
{
    public class ChatHub : Hub
    {
        public async Task Send(User user)
        {
            user.Age += 5;
            await this.Clients.All.SendAsync("Receive", user);
        }
    }

    public class User
    { 
        public string Name { get; set; }    

        public int Age { get; set; }
    }
}
