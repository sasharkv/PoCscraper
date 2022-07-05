using ScraperWorker.Services;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// ScrapeWebsite(); call it here
// Opdracht: scrape website van xuntos.nl, nos.nl, microsoft.com (has no socials), cabfab.nl, unilever.nl, old.amsterdam for socials
// save in csv (name of website, socials - instagram, fb, twitter, linkedin, youtube) or just print console, json object

string url = "https://www.xuntos.nl/";
var scraperService = new ScraperService();
var result = await scraperService.ScrapeWebsiteAsync(url);
Console.WriteLine(result.LinkedInURL);
Console.WriteLine(result.TwitterURL);
Console.WriteLine(result.InstagramURL);
Console.ReadKey();  
