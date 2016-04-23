using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SalesForceIntegration.Framework;
using SalesForceIntegration.Helpers;
using SalesForceIntegration.Models;
using SalesForceIntegration.Services;

namespace SalesForceIntegration.Controllers
{
    public class OpportunitiesController : Controller
    {
        private readonly SfdcDatabaseConnector _dbConnector;

        private readonly SalesForceService _salesForceService;

        public OpportunitiesController()
        {
            _dbConnector = new SfdcDatabaseConnector();
            _salesForceService = new SalesForceService();
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var allOpportunities = new SalesForceModels.OpportunitiesViewModel();
            try
            {
                var client = await _salesForceService.CreateForceClient();
                allOpportunities = await _dbConnector.GetOpportunities(client);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return View("Index", allOpportunities);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public async Task<ActionResult> Add(SalesForceModels.OpportunityModel input)
        {
            if (ModelState.IsValid)
            {
                var sfdcResponse = new SalesForceModels.SalesForceResponseModel();
                var client = await _salesForceService.CreateForceClient();

                input.StageName = GlobalHelper.GetStageName(Convert.ToInt32(input.StageName));
                input.Type = GlobalHelper.GetType(Convert.ToInt32(input.Type));
                input.LeadSource = GlobalHelper.GetLeadSource(Convert.ToInt32(input.LeadSource));

                sfdcResponse = await _dbConnector.CreateOpportunity(client, input);
                TempData["NotificationType"] = sfdcResponse.IsSuccess
                    ? NotificationType.Success.ToString()
                    : NotificationType.Error.ToString();

                TempData["Notification"] = sfdcResponse.Details;
                return RedirectToAction("Index", "Opportunities");
            }

            TempData["NotificationType"] = NotificationType.Error.ToString();
            TempData["Notification"] = GlobalHelper.GetErrorListFromModelState(ModelState);
            return View(input);
        }

        [HttpGet]
        public async Task<ActionResult> Update(string opportunity)
        {
            var model = new SalesForceModels.OpportunityModel();
            try
            {
                var client = await _salesForceService.CreateForceClient();
                model = await _dbConnector.GetOpportunityById(client, opportunity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Update(SalesForceModels.OpportunityModel input)
        {
            if (ModelState.IsValid)
            {
                var sfdcResponse = new SalesForceModels.SalesForceResponseModel();
                var client = await _salesForceService.CreateForceClient();

                input.StageName = GlobalHelper.GetStageName(Convert.ToInt32(input.StageName));
                input.Type = GlobalHelper.GetType(Convert.ToInt32(input.Type));
                input.LeadSource = GlobalHelper.GetLeadSource(Convert.ToInt32(input.LeadSource));

                // Do not specify Id or an external ID field in the request body or an error is generated.
                // https://developer.salesforce.com/docs/atlas.en-us.api_rest.meta/api_rest/resources_sobject_upsert.htm
                var sfId = input.Id;
                input.Id = null;

                sfdcResponse = await _dbConnector.UpdateOpportunity(client, sfId, input);

                TempData["NotificationType"] = sfdcResponse.IsSuccess
                    ? NotificationType.Success.ToString()
                    : NotificationType.Error.ToString();

                TempData["Notification"] = sfdcResponse.Details;
                return RedirectToAction("Index", "Opportunities");
            }
            TempData["NotificationType"] = NotificationType.Error.ToString();
            TempData["Notification"] = GlobalHelper.GetErrorListFromModelState(ModelState);
            return View(input);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(string opportunityId)
        {
            var sfdcResponse = new SalesForceModels.SalesForceResponseModel();

            try
            {
                var client = await _salesForceService.CreateForceClient();
                sfdcResponse = await _dbConnector.DeleteOpportunity(client, opportunityId);
            }
            catch (Exception ex)
            {
                sfdcResponse.Details = ex.Message;
            }

            return Json(new
            {
                IsDeleted = sfdcResponse.IsSuccess,
                Details = sfdcResponse.Details
            }, JsonRequestBehavior.DenyGet);
        }
    }
}
