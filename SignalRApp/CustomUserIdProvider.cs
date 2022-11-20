using Microsoft.AspNetCore.SignalR;

namespace SignalRApp
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity!.Name;
        }
    }
}
