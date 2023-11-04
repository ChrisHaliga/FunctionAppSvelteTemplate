using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.IO;

namespace FunctionAppSvelteTemplate
{
    public  class WebEndpoints
    {
        private readonly string _rootFolder;
        private readonly ILogger _logger;
        public WebEndpoints(string rootFolder)
        {
            _rootFolder = rootFolder;
        }

        [FunctionName(nameof(Web))]
        public IActionResult Web([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "web/{*asset}")] HttpRequest req, string asset)
        {
            if (string.IsNullOrEmpty(asset))
                return new RedirectResult("~/web/index.html", true);

            string filePath = $"{_rootFolder}/Frontend/{asset}";

            if (File.Exists(filePath))
            {
                var fileContent = File.ReadAllBytes(filePath);
                string contentType = GetContentType(asset);

                return new FileContentResult(fileContent, contentType);
            }
            else
                return new NotFoundResult();
        }

        private static string GetContentType(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            switch (extension)
            {
                case ".css":
                    return "text/css";
                case ".js":
                    return "application/javascript";
                case ".html":
                    return "text/html";
                case ".json":
                    return "application/json";
                case ".csv":
                    return "text/csv";
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".bmp":
                    return "image/bmp";
                case ".svg":
                    return "image/svg+xml";
                case ".webp":
                    return "image/webp";
                default:
                    return "application/octet-stream";
            }
        }
    }
}
