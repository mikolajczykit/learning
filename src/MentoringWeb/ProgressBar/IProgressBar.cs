using MentoringWeb.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentoringWeb
{
    public interface IProgressBar
    {
        Task SetProgressAsync(int progress);
    }
}
