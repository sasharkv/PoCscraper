using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperWorker.Services
{
    public interface IScraperService
    {
        Task<ScrapedResource> ScrapeWebsiteAsync(string url);
    }
}
