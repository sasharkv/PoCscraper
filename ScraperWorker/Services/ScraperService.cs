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

            List<string> listOfSocialUrlsXpaths = new List<string>
                {"//a[contains(@href, \"linkedin.com\")]", 
                "//a[contains(@href, \"twitter.com\")]", 
                "//a[contains(@href, \"instagram.com\")]"};

            List<HtmlNode> linkNodes = new List<HtmlNode>();

            // check if the node exists and add it to the list
            foreach (var xpath in listOfSocialUrlsXpaths)
            {
                var linkNode = htmlDocument.DocumentNode.SelectSingleNode(xpath);
                if (linkNode != null)
                {
                    linkNodes.Add(linkNode);
                }
                else
                {
                    Console.WriteLine("No link node for " + xpath);
                }
            }

            var scrapedResource = new ScrapedResource();

            // add the value of existing nodes to the corresponding attribute of the scrapedResource object
            foreach (var linkNode in linkNodes)
            {
                if (linkNode.Attributes["href"].Value.Contains("linkedin.com"))
                {
                    scrapedResource.LinkedInURL = linkNode.Attributes["href"].Value;
                }
                else if (linkNode.Attributes["href"].Value.Contains("twitter.com"))
                {
                    scrapedResource.TwitterURL = linkNode.Attributes["href"].Value; 
                }
                else if (linkNode.Attributes["href"].Value.Contains("instagram.com"))
                    scrapedResource.InstagramURL = linkNode.Attributes["href"].Value;
            }

            return scrapedResource;
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
