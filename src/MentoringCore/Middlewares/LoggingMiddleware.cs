using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using Serilog;
using Newtonsoft.Json;
using MentoringCore.Assets;

namespace MentoringCore.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger Logger;
        public LoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
            this.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(@"C:\logs\requests.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await next.Invoke(context);
            stopWatch.Stop();

            this.Logger.Information($"Request {JsonConvert.SerializeObject(new RequestInformationDto(context))} elapsed in {stopWatch.ElapsedMilliseconds} ms");
        }
    }
}

