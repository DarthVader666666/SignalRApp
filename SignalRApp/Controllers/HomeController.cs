using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalRApp.Controllers
{
    public class HomeController : Controller
    {
        IHubContext<ChatHub> hubContext;

        public HomeController(IHubContext<ChatHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task Create(string product, string connectionId)
        {
            await hubContext.Clients.AllExcept(connectionId).SendAsync("Notify", $"Добавлено: {product} - {DateTime.Now.ToShortTimeString()}");
            await hubContext.Clients.Client(connectionId).SendAsync("Notify", $"Ваш товар добавлен!");
        }
    }
}
