using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Salesforce.Force;
using SalesForceIntegration.Models;

namespace SalesForceIntegration.Repository
{
    public interface ISalesForceRepository
    {
        Task<SalesForceModels.LeadsViewModel> GetLeads(ForceClient client);
        Task<SalesForceModels.LeadModel> GetLeadById(ForceClient client, string leadId);
        Task<SalesForceModels.SalesForceResponseModel> UpdateLead(ForceClient client, string sfId, SalesForceModels.LeadModel lead);
        Task<SalesForceModels.SalesForceResponseModel> DeleteLead(ForceClient client, string salesForceId);
        Task<SalesForceModels.SalesForceResponseModel> CreateLead(ForceClient client, SalesForceModels.LeadModel lead);

        Task<SalesForceModels.OpportunitiesViewModel> GetOpportunities(ForceClient client);
        Task<SalesForceModels.OpportunityModel> GetOpportunityById(ForceClient client, string leadId);
        Task<SalesForceModels.SalesForceResponseModel> UpdateOpportunity(ForceClient client, string sfId, SalesForceModels.OpportunityModel opportunity);
        Task<SalesForceModels.SalesForceResponseModel> DeleteOpportunity(ForceClient client, string salesForceId);
        Task<SalesForceModels.SalesForceResponseModel> CreateOpportunity(ForceClient client, SalesForceModels.OpportunityModel opportunity);
    }
}