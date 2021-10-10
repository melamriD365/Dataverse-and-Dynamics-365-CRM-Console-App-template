using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace Dataverse_and_Dynamics_365_CRM_Console_App_template
{
    public class CrmServiceProvider : ICrmServiceProvider
    {
        private readonly AppConfig appConfig;
        private readonly ILogger loggerService;
        private readonly IHostApplicationLifetime applicationLifetime;
        private CrmServiceClient service;

        public CrmServiceProvider(IOptions<AppConfig> _appConfig, ILogger<AppWorker> _logger, IHostApplicationLifetime _appLifetime)
        {
            appConfig = _appConfig.Value;
            loggerService = _logger;
            applicationLifetime = _appLifetime;
            CrmServiceClient crmSvc = null;
            try
            {
                loggerService.LogInformation("Trying to connect to Dataverse/Dynamics 365 CRM");
                crmSvc = new CrmServiceClient(new Uri(appConfig.SERVER_URL), appConfig.CLIENT_ID, appConfig.CLIENT_SECRET, false, "");
                if (crmSvc != null)
                {
                    var whoAmIResponse = crmSvc.Execute(new WhoAmIRequest());
                    if (whoAmIResponse != null)
                    {
                        loggerService.LogInformation("Connection OK....");
                        service = crmSvc;
                    }
                    else
                    {
                        loggerService.LogError("Connection KO....");
                        applicationLifetime.StopApplication();
                    }
                }
            }
            catch (Exception e)
            {
                this.loggerService.LogError("Connection KO....: {0}", e.Message);
                applicationLifetime.StopApplication();
            }
        }

        public CrmServiceClient getService()
        {
            return this.service;
        }
    }
}
