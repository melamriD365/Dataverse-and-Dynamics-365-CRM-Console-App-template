using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dataverse_and_Dynamics_365_CRM_Console_App_template
{
    public class AppWorker : BackgroundService
    {
        private readonly ILogger loggerService;
        private readonly IHostApplicationLifetime applicationLifetime;
        private ICrmServiceProvider crmServiceProvider;

        public AppWorker(ILogger<AppWorker> _logger, IHostApplicationLifetime _appLifetime, ICrmServiceProvider _crmServiceProvider)
        {
            loggerService = _logger;
            applicationLifetime = _appLifetime;
            crmServiceProvider = _crmServiceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() => loggerService.LogInformation("AppWorker is stopping..."));
            if (!stoppingToken.IsCancellationRequested)
            {
                var crmServiceClient = crmServiceProvider.getService();
                //Implement your logic here
                loggerService.LogInformation("AppWorker is working...");
                //TODO


                //Stop the worker
                applicationLifetime.StopApplication();
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
