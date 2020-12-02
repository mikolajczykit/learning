﻿using MentoringWeb.Hubs;
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
        //private readonly ProgressbarHub _hub;

        public ProgressBar(IHubContext<ProgressbarHub> _hubContext) 
        {
            this._hubContext = _hubContext;
            //this._hub = _hub;
        }

        public async Task SetProgressAsync(int progress) 
        {
            Progress = progress;
            //await this._hub.SendProgressToListener();
            await _hubContext.Clients.All.SendAsync("ReceiveProgress", ProgressBar.Progress);
        }
    }
}
