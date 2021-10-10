using Microsoft.Xrm.Tooling.Connector;

namespace Dataverse_and_Dynamics_365_CRM_Console_App_template
{
    public interface ICrmServiceProvider
    {
        CrmServiceClient getService();
    }
}