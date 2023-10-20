using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Mime;

namespace FunctionAppSvelteTemplate
{
    public static class WebEndpoints
    {
        [FunctionName(nameof(Index))]
        public static IActionResult Index(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "index.html")] HttpRequest req,
            ILogger log)
        {
            string filePath = "./Frontend/dist/index.html";

            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                string contentType = "text/html";

                return new ContentResult
                {
                    Content = fileContent,
                    ContentType = contentType,
                    StatusCode = 200
                };
            }
            else
                return new NotFoundResult();
        }

        [FunctionName(nameof(Assets))]
        public static IActionResult Assets(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "assets/{*asset}")] HttpRequest req, string asset,
            ILogger log)
        {
            string filePath = $"./Frontend/dist/assets/{asset}";

            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                string contentType = GetContentType(asset);

                return new ContentResult
                {
                    Content = fileContent,
                    ContentType = contentType,
                    StatusCode = 200
                };
            }
            else
                return new NotFoundResult();
        }

        [FunctionName(nameof(PublicAssets))]
        public static IActionResult PublicAssets(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "{*asset}")] HttpRequest req, string asset,
            ILogger log)
        {
            string filePath = $"./Frontend/dist/{asset}";

            if (File.Exists(filePath))
            {
                byte[] imageBytes = File.ReadAllBytes(filePath);
                string contentType = GetContentType(asset);

                return new FileContentResult(imageBytes, contentType);
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
