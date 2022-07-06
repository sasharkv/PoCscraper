/* using ScraperWorker.Services;

// ScrapeWebsite(); call it here
// Opdracht: scrape website van xuntos.nl, nos.nl, microsoft.com (has no socials), cabfab.nl, unilever.nl, old.amsterdam for socials
// name of website, socials - instagram, fb, twitter, linkedin, youtube - print console, json object

string url = "https://www.xuntos.nl/";
var scraperService = new ScraperService();
var result = await scraperService.ScrapeWebsiteAsync(url);
Console.WriteLine(result.LinkedInURL);
Console.WriteLine(result.TwitterURL);
Console.WriteLine(result.InstagramURL);
Console.ReadKey();  */

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScraperWorker.Services;


var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, builder) =>
    {
        builder.SetBasePath(Directory.GetCurrentDirectory());
    })
    .ConfigureServices((context, services) =>
    {
        //add your service registrations
        services.AddSingleton<IScraperService, ScraperService>();
    }).Build();

var service = host.Services.GetService<IScraperService>();

string url = "https://www.xuntos.nl/";

if (service == null)
{
    throw new InvalidOperationException("ExampleService not found.");
}

var result = await service.ScrapeWebsiteAsync(url);
Console.WriteLine(result.LinkedInURL);
Console.WriteLine(result.TwitterURL);
Console.WriteLine(result.InstagramURL);

Console.ReadKey();