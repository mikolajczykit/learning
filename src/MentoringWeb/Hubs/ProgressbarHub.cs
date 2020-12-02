using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentoringWeb.Hubs
{
    public class ProgressbarHub : Hub
    {
        public async Task SendProgress() 
        {
            await Clients.All.SendAsync("ReceiveProgress", ProgressBar.Progress);
        }
    }
}
