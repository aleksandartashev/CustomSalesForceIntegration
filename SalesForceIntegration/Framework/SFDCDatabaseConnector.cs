using System;
using System.Linq;
using System.Threading.Tasks;
using Salesforce.Force;
using SalesForceIntegration.Models;
using SalesForceIntegration.Repository;

namespace SalesForceIntegration.Framework
{
    public class SfdcDatabaseConnector : ISalesForceRepository
    {
        #region Leads

        public async Task<SalesForceModels.LeadsViewModel> GetLeads(ForceClient client)
        {
            var allLeads = new SalesForceModels.LeadsViewModel();
            var leads = await client.QueryAsync<SalesForceModels.LeadModel>("SELECT ID, FirstName, LastName, Company, Email, Status, Phone From Lead ORDER BY LastName ASC");
            if (leads.Records.Any())
            {
                foreach (var lead in leads.Records)
                {
                    allLeads.Leads.Add(new SalesForceModels.LeadModel
                    {
                        Id = lead.Id,
                        FirstName = lead.FirstName,
                        LastName = lead.LastName,
                        Company = lead.Company,
                        Email = lead.Email,
                        Status = lead.Status,
                        Phone = lead.Phone
                    });
                }
            }
            return allLeads;
        }

        public async Task<SalesForceModels.LeadModel> GetLeadById(ForceClient client, string leadId)
        {
            var leadModel = new SalesForceModels.LeadModel();
            var lead = await client.QueryByIdAsync<SalesForceModels.LeadModel>("Lead", leadId);
            if (lead != null)
            {
                leadModel.Id = lead.Id;
                leadModel.FirstName = lead.FirstName;
                leadModel.LastName = lead.LastName;
                leadModel.Company = lead.Company;
                leadModel.Email = lead.Email;
                leadModel.Status = lead.Status;
                leadModel.Phone = lead.Phone;
                leadModel.LastEditedBy__c = lead.LastEditedBy__c ?? string.Empty;
                leadModel.LastEditedOn__c = lead.LastEditedOn__c;
            };
            return leadModel;
        }

        public async Task<SalesForceModels.SalesForceResponseModel> UpdateLead(ForceClient client, string sfId, SalesForceModels.LeadModel lead)
        {
            var result = await client.UpdateAsync("Lead", sfId, lead);
            return new SalesForceModels.SalesForceResponseModel
            {
                IsSuccess = result.Success,
                Details = result.Success ? "Lead successfully updated." : "Problem while updating lead into SFDC. Please refresh the page and try again."
            };
        }

        public async Task<SalesForceModels.SalesForceResponseModel> DeleteLead(ForceClient client, string leadId)
        {
            var res = await client.DeleteAsync("Lead", leadId);
            return new SalesForceModels.SalesForceResponseModel
            {
                IsSuccess = res,
                Details = res ? "Lead succesfully deleted." : "Problem while deleting lead from SFDC.Please refresh the page and try again.",
            };
        }

        public async Task<SalesForceModels.SalesForceResponseModel> CreateLead(ForceClient client, SalesForceModels.LeadModel lead)
        {
            var result = await client.CreateAsync("Lead", lead);

            // by accessing result.Id you can obtain the unique SalesForce ID for the lead you just created
            return new SalesForceModels.SalesForceResponseModel
            {
                IsSuccess = result.Success,
                Details = result.Success ? "Lead successfully created." : "Problem while creating lead into SFDC.Please refresh the page and try again."
            };
        }

        #endregion

        #region Opportunities

        public async Task<SalesForceModels.OpportunitiesViewModel> GetOpportunities(ForceClient client)
        {
            var allOpportunities = new SalesForceModels.OpportunitiesViewModel();
            var opportunities = await client.QueryAsync<SalesForceModels.OpportunityModel>("SELECT ID, Name, CloseDate, StageName, Type, Amount, LeadSource,Probability From Opportunity ORDER BY Name ASC");
            if (opportunities.Records.Any())
            {
                foreach (var opportunity in opportunities.Records)
                {
                    allOpportunities.Opportunities.Add(new SalesForceModels.OpportunityModel
                    {
                        Id = opportunity.Id,
                        Name = opportunity.Name,
                        CloseDate = opportunity.CloseDate,
                        StageName = opportunity.StageName,
                        Type = opportunity.Type,
                        Amount = opportunity.Amount,
                        LeadSource = opportunity.LeadSource,
                        Probability = opportunity.Probability,
                        //Account = opportunity.Account
                    });
                }
            }
            return allOpportunities;
        }

        public async Task<SalesForceModels.OpportunityModel> GetOpportunityById(ForceClient client, string opportunityId)
        {
            var opportunityModel = new SalesForceModels.OpportunityModel();
            var opportunity = await client.QueryByIdAsync<SalesForceModels.OpportunityModel>("Opportunity", opportunityId);
            if (opportunity != null)
            {
                opportunityModel.Id = opportunity.Id;
                opportunityModel.LeadSource = opportunity.LeadSource;
                opportunityModel.StageName = opportunity.StageName;
                opportunityModel.Type = opportunity.Type;
                opportunityModel.Amount = opportunity.Amount;
                opportunityModel.Name = opportunity.Name;
                opportunityModel.Probability = opportunity.Probability;
                opportunityModel.CloseDate = opportunity.CloseDate;
            }
            return opportunityModel;
        }

        public async Task<SalesForceModels.SalesForceResponseModel> UpdateOpportunity(ForceClient client, string sfId, SalesForceModels.OpportunityModel opportunity)
        {
            var result = await client.UpdateAsync("Opportunity", sfId, opportunity);
            return new SalesForceModels.SalesForceResponseModel
            {
                IsSuccess = result.Success,
                Details = result.Success ? "Opportunity successfully updated." : "Problem while updating opportunity into SFDC. Please refresh the page and try again."
            };
        }

        public async Task<SalesForceModels.SalesForceResponseModel> DeleteOpportunity(ForceClient client, string opportunityId)
        {
            var res = await client.DeleteAsync("Opportunity", opportunityId);
            return new SalesForceModels.SalesForceResponseModel
            {
                IsSuccess = res,
                Details = res ? "Opportunity succesfully deleted." : "Problem while deleting opportunity from SFDC.Please refresh the page and try again.",
            };
        }

        public async Task<SalesForceModels.SalesForceResponseModel> CreateOpportunity(ForceClient client, SalesForceModels.OpportunityModel opportunity)
        {
            var result = await client.CreateAsync("Opportunity", opportunity);
            return new SalesForceModels.SalesForceResponseModel
            {
                IsSuccess = result.Success,
                Details = result.Success ? "Opportunity successfully created." : "Problem while creating opportunity into SFDC.Please refresh the page and try again."
            };
        }

        #endregion
    }
}