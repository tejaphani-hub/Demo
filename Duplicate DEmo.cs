using Microsoft.Xrm.Sdk;
using System;
using Microsoft.Xrm.Sdk.Query;

namespace PracticePlugins1
{
    public class Duplicate_DEmo : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = (IOrganizationService)serviceFactory.CreateOrganizationService(context.UserId);

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                Entity Contact = (Entity)context.InputParameters["Target"];

                string email = string.Empty;

                if (Contact.Attributes.Contains("emailaddress1"))
                {
                    email = Contact.Attributes["emailaddress1"].ToString();
                }

                QueryExpression query = new QueryExpression("contact");
                query.ColumnSet = new ColumnSet(new string[] {"emailaddress1"});
                query.Criteria.AddCondition("emailaddress1", ConditionOperator.Equal, email);

                EntityCollection ec = service.RetrieveMultiple(query);

                if(ec.Entities.Count > 0)
                {
                    throw new InvalidPluginExecutionException("contact with email is already exist");
                }

                              
            }
        }

    }
}
