using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace PracticePlugins1
{
    public class Create_Account_Method_2 : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {

            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = (IOrganizationService)serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));


            int stageNumber = 0;
            tracingService.Trace("stage {0}", ++stageNumber);


            var targetEntity = (Entity)context.InputParameters["Target"];

            tracingService.Trace("stage {0}", ++stageNumber);

            string currentAccountname = (string)targetEntity["name"];
            string newAccountname = currentAccountname + " (Copy)";
            tracingService.Trace("stage {0}", ++stageNumber);

            var newAccount = new Entity("nw_accountcopy");
            tracingService.Trace("stage {0}", ++stageNumber);
            newAccount["nw_name"] = newAccountname;
            tracingService.Trace("stage {0}", ++stageNumber);
            newAccount["nw_fax"] = "7894-18587";
            tracingService.Trace("stage {0}", ++stageNumber);
            //Guid accountId = service.Create(newAccount); // General Way of Creating the sevice using Service Organization Context

            //create Response and Request using Execute Method

            var request = new CreateRequest() { Target = newAccount };
            var response = (CreateResponse)service.Execute(request);
            Guid accountID = response.id;

            //Update using GUID value is accountID

            var existingAccountCopy = new Entity("nw_accountcopy", accountID);
            var updateAccountCopy = new Entity("nw_accountcopy");
            updateAccountCopy.Id = existingAccountCopy.Id;
            updateAccountCopy["nw_fax"] = "112-101010";
            service.Update(updateAccountCopy);

            //Delete
            //service.Delete("nw_accountcopy", accountID);

            //Retriving the data using GUID
            var retriveEntity = service.Retrieve("nw_accountcopy", accountID, new ColumnSet("nw_name", "nw_fax"));
            tracingService.Trace("stage {0} {1} {2}", ++stageNumber, retriveEntity["nw_name"], retriveEntity["nw_fax"]);

            //Retrieve using Alternate Keys
            RetrieveRequest requestR = new RetrieveRequest()
            {
                ColumnSet = new ColumnSet("name", "fax"),
                Target = new EntityReference("account", "name", "Devara NTR") // Retrive from the Account entity using Alternate Key

            };

            var responseR = (RetrieveResponse)service.Execute(requestR);
            Entity entityR = responseR.Entity;
                   

            //using the Compund Key means KeyCollection which are having more than one Key3

            var keyS = new KeyAttributeCollection();
            keyS.Add("name", "Devara NTR");

            RetrieveRequest requestS = new RetrieveRequest()
            {
                ColumnSet = new ColumnSet("name", "fax"),
                Target = new EntityReference("account", keyS) // Retrive from the Account entity using Alternate Key

            };

            var responseS = (RetrieveResponse)service.Execute(requestR);
            Entity entityS = responseR.Entity;

            tracingService.Trace("stage {0} {1}", ++stageNumber, entityS["fax"]); // Retriving form the Account Copy 













        }
    }
}
