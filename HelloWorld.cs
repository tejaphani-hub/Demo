using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace PracticePlugins1
{
    public class HelloWorld : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = (IOrganizationService)serviceFactory.CreateOrganizationService(context.UserId);

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                Entity entity = (Entity)context.InputParameters["Target"];

                string firstName = string.Empty;

                if (entity.Attributes.Contains("firstname"))
                {
                   firstName = entity.Attributes["firstname"].ToString();
                }

                
                string lastName = entity.Attributes["lastname"].ToString();


                entity.Attributes.Add("description", "Hello World " + firstName + lastName);
                

               


            }
        }
    }
}
