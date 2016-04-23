using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SalesForceIntegration.Controllers;

namespace SalesForceIntegration.Models
{
    public class SalesForceModels
    {
        public class LeadModel
        {
            // standard mandatory fields
            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            [Required]
            public string Status { get; set; }

            public string Id { get; set; }

            // standard optional fields
            public string Company { get; set; }
            public string Phone { get; set; }

            public string Email { get; set; }

            // custom optional fields
            public string LastEditedBy__c { get; set; }
            public DateTime? LastEditedOn__c { get; set; }
        }

        public class OpportunityModel
        {
            public string Id { get; set; }

            // standard mandatory fields
            [Required]
            public string Name { get; set; }
            [Required]
            public DateTime CloseDate { get; set; }
            [Required]
            public string StageName { get; set; }

            // standard optional fields
            public string Type { get; set; }
            public float Amount { get; set; }
            public string LeadSource { get; set; }
            [Range(1, 100)]
            public decimal Probability { get; set; }
        }

        public class LeadsViewModel
        {
            public List<LeadModel> Leads { get; set; }
            public LeadsViewModel()
            {
                Leads = new List<LeadModel>();
            }
        }

        public class OpportunitiesViewModel
        {
            public List<OpportunityModel> Opportunities { get; set; }
            public OpportunitiesViewModel()
            {
                Opportunities = new List<OpportunityModel>();
            }
        }

        public class SalesForceResponseModel
        {
            public bool IsSuccess { get; set; }
            public string Details { get; set; }
        }
    }
}
