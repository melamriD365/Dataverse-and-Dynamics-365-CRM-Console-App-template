using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace Dataverse_and_Dynamics_365_CRM_Console_App_template
{
    class Program
    {
        static void Main(string[] args)
        {
            var build = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
               {
                   services.Configure<AppConfig>(new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables(prefix: "CRM_").Build());
                   services.AddSingleton<ICrmServiceProvider, CrmServiceProvider>();
                   services.AddHostedService<AppWorker>();
               }).UseConsoleLifetime().Build();

            build.RunAsync();
            Console.ReadLine();
        }
    }
}
