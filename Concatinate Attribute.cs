using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace PracticePlugins1
{
    public class Class2 : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IOrganizationService service = (IOrganizationService)serviceProvider.GetService(typeof(IOrganizationService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            tracingService.Trace("stage 1");
            if (!context.InputParameters.Contains("Target"))
            {
                throw new InvalidPluginExecutionException("There is no Target");
            }
            try
            {
                var entity = (Entity)context.InputParameters["Target"];
                tracingService.Trace("stage {0} ", "2");
                if (entity.Attributes.Contains("fax"))
                {
                    string newFax = (string)entity["fax"];

                    var entityOld = (Entity)context.PreEntityImages["preImage"];
                    string oldFax = "";
                    if (entityOld.Attributes.Contains("fax"))
                    {
                        oldFax = (string)entityOld["fax"];
                    }
                    else
                    {
                       oldFax = "Nothing";
                    }
                    tracingService.Trace("stage {0} ", "3");
                    entity["address1_line3"] = "The Data was " + oldFax + " and Now " + newFax;
                    tracingService.Trace("stage {0} ", "4");
                    }
                else
                {
                    tracingService.Trace("stage {0} ", "5");
                    entity["address1_line3"] = "This has no Data ";
                    tracingService.Trace("stage {0} ", "6");
                }
            }
            catch
            {
                throw new InvalidPluginExecutionException("!catch");
            }
            
        }
           
            
           
    }
}
