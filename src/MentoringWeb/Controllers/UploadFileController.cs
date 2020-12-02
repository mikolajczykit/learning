using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MentoringWeb.Models;
using System.IO;
using System.Threading;

namespace MentoringWeb.Controllers
{
    public class UploadFileController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static Dictionary<string, CancellationTokenSource> CancellationTokens = new Dictionary<string, CancellationTokenSource>();

        public UploadFileController(ILogger<HomeController> logger)
        {
            this._logger = logger;
        }

        public IActionResult Index()
        {
            return View(new UploadFileViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(UploadFileViewModel viewModel, string guid) 
        {
            var cancellationToken = new CancellationTokenSource();
            CancellationTokens.Add(guid, cancellationToken);

            string result = "Data Uploaded Successfully!";
            ProgressBar.Progress = 0;

            long totalSize = viewModel.File.Length;

            var path = @$"C:\\files\\{viewModel.File.FileName}";

            byte[] buffer = new byte[16 * 1024];

            await Task.Run(async () =>
            {
                try
                {
                    using (FileStream output = System.IO.File.Create(path))
                    {
                        using (Stream input = viewModel.File.OpenReadStream())
                        {
                            long totalReadBytes = 0;
                            int readBytes;

                            while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                if (cancellationToken.IsCancellationRequested)
                                    throw new TaskCanceledException();

                                this._logger.LogInformation($"Before await: {Thread.CurrentThread.ManagedThreadId.ToString()}");
                                await output.WriteAsync(buffer, 0, readBytes);
                                this._logger.LogInformation($"After await: {Thread.CurrentThread.ManagedThreadId.ToString()}");
                                totalReadBytes += readBytes;
                                ProgressBar.Progress = (int)((float)totalReadBytes / (float)totalSize * 100.0);
                                await Task.Delay(10);
                            }
                        }
                    }
                }
                catch (TaskCanceledException)
                {
                    result = "Data Uploaded CANCELLED!";
                }
                catch (Exception) 
                {
                    result = "File upload exception!";
                }
            }, cancellationToken.Token);

            return Json(new { message = result });
        }

        [HttpPost]
        public ActionResult Progress()
        {
            return this.Content(ProgressBar.Progress.ToString());
        }

        [HttpPost]
        public IActionResult Cancel(string guid) 
        {
            var cancellationTokenSource = CancellationTokens[guid];
            cancellationTokenSource.Cancel();

            return Json(new { message = "File upload cancelled!" });
        }
    }
}
