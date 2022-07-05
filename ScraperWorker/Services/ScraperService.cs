using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ScraperWorker.Services
{
    public class ScraperService : IScraperService
    {
       // private readonly static string _websiteUrl = "https://www.xuntos.nl/";
        private readonly static HttpClient _httpClient = new HttpClient();

        // make everything async 
        public  async Task<ScrapedResource> ScrapeWebsiteAsync(string url)
        {
            var htmlDocument = new HtmlDocument();

            var httpResponseMessage = await _httpClient.GetAsync(url);
            await EnsureSuccessStatusCode(httpResponseMessage); 

            var scrapedHtml = await httpResponseMessage.Content.ReadAsStringAsync(); 
            htmlDocument.LoadHtml(scrapedHtml); //loads an HTML document from string

            string xpathLinkedInUrl = "//*[contains(@href, \"www.linkedin.com\")]";
            var LinkedInUrl = htmlDocument.DocumentNode.SelectSingleNode(xpathLinkedInUrl); // this should get the link itself
            if (LinkedInUrl != null)
            {
                var ScrapedResource = new ScrapedResource()
                {
                    LinkedInURL = LinkedInUrl.Attributes["href"].Value
                };

                return ScrapedResource;
            }
            else
            {
                Console.WriteLine("no LinkedInUrl");
            }

            Console.WriteLine(LinkedInUrl);

            return default;
        }

        private static async Task EnsureSuccessStatusCode(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
                return;

            var statusCode = httpResponseMessage.StatusCode;
            var httpContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var reasonPhrase = httpResponseMessage.ReasonPhrase;

            throw new HttpRequestException($"Status Code: ({(int)statusCode}-{statusCode}). Reason Phrase: ({reasonPhrase}). Content: ({httpContent})");
        }

    }
}
