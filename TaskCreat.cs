using System;
using Microsoft.Xrm.Sdk;

namespace PracticePlugins1
{
    public class TaskCreat : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = (IOrganizationService)serviceFactory.CreateOrganizationService(context.UserId);

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                Entity Contact = (Entity)context.InputParameters["Target"];

                Entity taskRecord = new Entity("task");

                taskRecord.Attributes.Add("subject", "Follow UP");

                taskRecord.Attributes.Add("description", "Please Follow Up with Cusomer BRO....");

                taskRecord.Attributes.Add("regardingobjectid", new EntityReference("contact", Contact.Id));

                taskRecord.Attributes.Add("prioritycode", new OptionSetValue(2));

                taskRecord.Attributes.Add("scheduledend", DateTime.Now.AddDays(5));

                Guid Taskguid = service.Create(taskRecord);



            }
        }
    }
}
