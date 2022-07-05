using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperWorker.Services
{
    internal interface IScraperService
    {
        Task ScrapeWebsite(string url);
    }
}
