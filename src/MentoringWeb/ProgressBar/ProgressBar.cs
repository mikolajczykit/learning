using MentoringWeb.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MentoringWeb
{
    public class ProgressBar : IProgressBar
    {
        public static int Progress { get; private set; } = 0;

        private readonly IHubContext<ProgressbarHub> _hubContext;

        public ProgressBar(IHubContext<ProgressbarHub> _hubContext) 
        {
            this._hubContext = _hubContext;
        }

        public async Task SetProgressAsync(int progress) 
        {
            Progress = progress;

            await _hubContext.Clients.All.SendAsync("ReceiveProgress", ProgressBar.Progress);
        }
    }
}
