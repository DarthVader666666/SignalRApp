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
        public async Task<IActionResult> Create(string product)
        {
            await this.hubContext.Clients.All.SendAsync("Notify", $"Added: {product} - {DateTime.Now.ToShortTimeString()}");
            return this.RedirectToAction("Index");
        }
    }
}
