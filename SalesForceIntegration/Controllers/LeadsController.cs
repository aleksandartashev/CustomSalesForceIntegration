using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using SalesForceIntegration.Framework;
using SalesForceIntegration.Helpers;
using SalesForceIntegration.Models;
using SalesForceIntegration.Services;

namespace SalesForceIntegration.Controllers
{
    public class LeadsController : Controller
    {
        private readonly SfdcDatabaseConnector _dbConnector;

        private readonly SalesForceService _salesForceService;

        public LeadsController()
        {
            _dbConnector = new SfdcDatabaseConnector();
            _salesForceService = new SalesForceService();
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var allLeads = new SalesForceModels.LeadsViewModel();
            try
            {
                var client = await _salesForceService.CreateForceClient();
                allLeads = await _dbConnector.GetLeads(client);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return View("Index", allLeads);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public async Task<ActionResult> Add(SalesForceModels.LeadModel input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var sfdcResponse = new SalesForceModels.SalesForceResponseModel();
                    var client = await _salesForceService.CreateForceClient();

                    input.LastEditedBy__c = "Aleksandar [from api]";
                    input.LastEditedOn__c = DateTime.UtcNow;
                    input.Status = GlobalHelper.GetStatus(Convert.ToInt32(input.Status));

                    sfdcResponse = await _dbConnector.CreateLead(client, input);
                    TempData["NotificationType"] = sfdcResponse.IsSuccess
                        ? NotificationType.Success.ToString()
                        : NotificationType.Error.ToString();

                    TempData["Notification"] = sfdcResponse.Details;
                    return RedirectToAction("Index", "Leads");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            TempData["NotificationType"] = NotificationType.Error.ToString();
            TempData["Notification"] = GlobalHelper.GetErrorListFromModelState(ModelState);
            return View(input);
        }

        [HttpGet]
        public async Task<ActionResult> Update(string leadId)
        {
            var model = new SalesForceModels.LeadModel();
            try
            {
                var client = await _salesForceService.CreateForceClient();
                model = await _dbConnector.GetLeadById(client, leadId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Update(SalesForceModels.LeadModel input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var sfdcResponse = new SalesForceModels.SalesForceResponseModel();
                    var client = await _salesForceService.CreateForceClient();

                    input.LastEditedBy__c = "Aleksandar [from web app]";
                    input.LastEditedOn__c = DateTime.UtcNow;
                    input.Status = GlobalHelper.GetStatus(Convert.ToInt32(input.Status));

                    // Do not specify Id or an external ID field in the request body or an error is generated.
                    // https://developer.salesforce.com/docs/atlas.en-us.api_rest.meta/api_rest/resources_sobject_upsert.htm
                    var sfId = input.Id;
                    input.Id = null;

                    sfdcResponse = await _dbConnector.UpdateLead(client, sfId, input);

                    TempData["NotificationType"] = sfdcResponse.IsSuccess
                        ? NotificationType.Success.ToString()
                        : NotificationType.Error.ToString();

                    TempData["Notification"] = sfdcResponse.Details;
                    return RedirectToAction("Index", "Leads");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            TempData["NotificationType"] = NotificationType.Error.ToString();
            TempData["Notification"] = GlobalHelper.GetErrorListFromModelState(ModelState);
            return View(input);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(string leadid)
        {
            var sfdcResponse = new SalesForceModels.SalesForceResponseModel();
            try
            {
                var client = await _salesForceService.CreateForceClient();
                sfdcResponse = await _dbConnector.DeleteLead(client, leadid);
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