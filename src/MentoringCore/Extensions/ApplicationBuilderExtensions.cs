using MentoringCore.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentoringCore.Extensions
{
    public static class ApplicationBuilderExtensions
    {            
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<LoggingMiddleware>();
        }
    }
}
