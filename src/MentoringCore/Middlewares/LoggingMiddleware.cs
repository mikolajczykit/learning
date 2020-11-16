using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using Serilog;
using Newtonsoft.Json;
using MentoringCore.Assets;
using Microsoft.Extensions.Configuration;

namespace MentoringCore.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger Logger;

        public LoggingMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this.next = next;
            string logsLocation = $"{configuration.GetValue<string>("Logging:LogsLocation")}requests.txt";
            this.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(@logsLocation, rollingInterval: RollingInterval.Day)
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

