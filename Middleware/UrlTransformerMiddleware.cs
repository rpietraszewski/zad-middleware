using Microsoft.AspNetCore.Http;
using Shyjus.BrowserDetection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UAParser;
using zad_middleware.Pages;

namespace zad_middleware.Middleware
{
    public class UrlTransformerMiddleware
    {
        private RequestDelegate _next;
        private readonly ILogger _logger;
        public UrlTransformerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public string domain = "https://www.mozilla.org/pl/firefox/new/";
        public async Task Invoke(HttpContext context )
        {
            var userAgent = context.Request.Headers["User-Agent"];
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);
            if (c.UA.Family == "InternetExplorer" || c.UA.Family == "EdgeChromium" || c.UA.Family == "Edge")
            {
                context.Response.Redirect(domain,true);
            }
            await _next(context);
        }
    }
    public static class UrlTransformerMiddlewareExtensions
    {
        public static IApplicationBuilder UseUrlTransformerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UrlTransformerMiddleware>();
        }
    }
}
