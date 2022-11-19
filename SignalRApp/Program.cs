using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.DependencyInjection;
using SignalRApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.EnableDetailedErrors = true;
    hubOptions.KeepAliveInterval = System.TimeSpan.FromMinutes(1);
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseDeveloperExceptionPage();
//app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.MapHub<ChatHub>("/chat", options =>
{
    options.ApplicationMaxBufferSize = 64;
    options.TransportMaxBufferSize = 64;
    options.LongPolling.PollTimeout = System.TimeSpan.FromMinutes(1);
    options.Transports = HttpTransportType.LongPolling | HttpTransportType.WebSockets;
});

app.MapDefaultControllerRoute();

app.Run();