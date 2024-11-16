using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace PracticePlugins1
{
    public class PreImageDemo : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = (IOrganizationService)serviceFactory.CreateOrganizationService(context.UserId);

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                Entity entity = (Entity)context.InputParameters["Target"]; // target entity which we need do Modifications

                string modifiedBusinessPhone = entity.Attributes["telephone1"].ToString(); //accesing the Field Values in the string Formate

                // We need to Register the Pre Image in order to get the Old Values for the Attribute

                Entity preImage = (Entity)context.PreEntityImages["preImage"]; // Here the Target Entity is User Defined preImage Entity and by using PreEntityImages

                string oldBusinessPhone = preImage.Attributes["telephone1"].ToString();

                throw new InvalidPluginExecutionException("phone Number Changed to: " + oldBusinessPhone + " to " + modifiedBusinessPhone);


            }
        }
    }
}
