using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ScraperWorker.Services;

// What I want to do:
// When we go to localhost/api/ScraperFunctionHttpTrigger -  the ScrapeWebsiteAsync method runs

namespace ScraperFunction
{
    public static class ScraperFunctionHttpTrigger
    {
        [FunctionName(nameof(ScraperFunctionHttpTrigger))] // instead of using a string as a function name ("ScraperFunctionHttpTrigger"), you can use reference to the class
        public static async Task<IActionResult> Run(          // the Route is the url we have in Azure/api/Our function name (or localhost/api/functionname); if you set Route = "hello", it'll be whateverhost/api/hello
            [HttpTrigger(AuthorizationLevel.Function, "get",  Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string url = "https://www.xuntos.nl/";
            var scraperService = new ScraperService();
            var scrapedResource = await scraperService.ScrapeWebsiteAsync(url);

            return new OkObjectResult(scrapedResource);
        }
    }
}
