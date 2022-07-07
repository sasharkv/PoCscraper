using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ScraperWorker.Services;

// https://docs.microsoft.com/en-ca/azure/azure-functions/functions-dotnet-dependency-injection
//
// We need this class to register services for DI.
//
// To set it up we need to:
// 1. make sure it inherits from FunctionStartup class from the Microsoft.Azure.Functions.Extensions NuGet package.
// 2. add an assembly directly to specify the class that acts as the Functions startup.
//

[assembly: FunctionsStartup(typeof(ScraperFunction.Startup))]

namespace ScraperFunction
{
    // The Startup class runs once the Azure Functions host starts up and builds up the ServiceCollection used to resolve dependencies any time they are needed
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // now register the IScraperService into the ServiceCollection used for Functions
            builder.Services.AddSingleton<IScraperService, ScraperService>();
        }
    }
}
