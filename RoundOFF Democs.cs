using System;
using Microsoft.Xrm.Sdk;

namespace PracticePlugins1
{
    public class RoundOFF_Democs : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = (IOrganizationService)serviceFactory.CreateOrganizationService(context.UserId);
            //IOrganizationService adminservice = (IOrganizationService)serviceFactory.CreateOrganizationService(new Guid("")); // It will runs on the Other user Context Called Impersonation

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                Entity account = (Entity)context.InputParameters["Target"];

                // target entity which we need do Modifications

                if (context.Depth > 1)
                    return;

                if (account.Attributes["revenue"] != null)
                {
                   decimal revenue = ((Money)account.Attributes["revenue"]).Value;

                   revenue = Math.Round(revenue, 1);

                    account.Attributes["revenue"] = new Money(revenue);



                }


                


               

            }
        }
    }
}
