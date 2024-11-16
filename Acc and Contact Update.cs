using Microsoft.Xrm.Sdk;
using System;

namespace PracticePlugins1
{
    public class Class1 : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = (IOrganizationService)serviceFactory.CreateOrganizationService(context.UserId);

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                Entity Account = (Entity)context.InputParameters["Target"];

                Entity Contact = new Entity("contact");
                Contact.Attributes["lastname"] = Account.Attributes["name"];

                Guid contactid = service.Create(Contact);

                Account.Attributes["primarycontactid"] = new EntityReference("contact" , contactid); //since this is the lookupvalue we should use Entity Reference
                

            }
        }
    }
}
