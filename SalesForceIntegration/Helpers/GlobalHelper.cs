using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace SalesForceIntegration.Helpers
{

    public class GlobalHelper
    {
        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return en.ToString();
        }

        public static string GetErrorListFromModelState(ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList.Aggregate("", (current, err) => current + (err + " | "));
        }

        public static string GetStageName(int stage)
        {
            var stageName = string.Empty;
            switch (stage)
            {
                case (int)(OpportunityStage.ClosedLost):
                    stageName = GetDescription(OpportunityStage.ClosedLost);
                    break;
                case (int)(OpportunityStage.IdDecisionMakers):
                    stageName = GetDescription(OpportunityStage.IdDecisionMakers);
                    break;
                case (int)(OpportunityStage.ClosedWon):
                    stageName = GetDescription(OpportunityStage.ClosedWon);
                    break;
                case (int)(OpportunityStage.NeedsAnalysis):
                    stageName = GetDescription(OpportunityStage.NeedsAnalysis);
                    break;
                case (int)(OpportunityStage.NegotiationReview):
                    stageName = GetDescription(OpportunityStage.NegotiationReview);
                    break;
                case (int)(OpportunityStage.PerceptionAnalysis):
                    stageName = GetDescription(OpportunityStage.PerceptionAnalysis);
                    break;
                case (int)(OpportunityStage.ProposalPriceQuote):
                    stageName = GetDescription(OpportunityStage.ProposalPriceQuote);
                    break;
                case (int)(OpportunityStage.Prospecting):
                    stageName = GetDescription(OpportunityStage.Prospecting);
                    break;
                case (int)(OpportunityStage.Qualification):
                    stageName = GetDescription(OpportunityStage.Qualification);
                    break;
                case (int)(OpportunityStage.ValueProposition):
                    stageName = GetDescription(OpportunityStage.ValueProposition);
                    break;
            }
            return stageName;
        }

        public static string GetType(int typeVal)
        {
            var type = string.Empty;
            switch (typeVal)
            {
                case (int)(OpportunityType.ExistingCustomerDowngrade):
                    type = GetDescription(OpportunityType.ExistingCustomerDowngrade);
                    break;
                case (int)(OpportunityType.ExistingCustomerReplacement):
                    type = GetDescription(OpportunityType.ExistingCustomerReplacement);
                    break;
                case (int)(OpportunityType.ExistingCustomerUpgrade):
                    type = GetDescription(OpportunityType.ExistingCustomerUpgrade);
                    break;
                case (int)(OpportunityType.NewCustomer):
                    type = GetDescription(OpportunityType.NewCustomer);
                    break;
            }
            return type;
        }

        public static string GetLeadSource(int leadSourceVal)
        {
            var leadSource = string.Empty;
            switch (leadSourceVal)
            {
                case (int)OpportunityLeadSource.None:
                    leadSource = GetDescription(OpportunityLeadSource.None);
                    break;
                case (int)OpportunityLeadSource.Other:
                    leadSource = GetDescription(OpportunityLeadSource.Other);
                    break;
                case (int)OpportunityLeadSource.PartnerReferral:
                    leadSource = GetDescription(OpportunityLeadSource.PartnerReferral);
                    break;
                case (int)OpportunityLeadSource.PhoneInquiry:
                    leadSource = GetDescription(OpportunityLeadSource.PhoneInquiry);
                    break;
                case (int)OpportunityLeadSource.PurchasedList:
                    leadSource = GetDescription(OpportunityLeadSource.PurchasedList);
                    break;
                case (int)OpportunityLeadSource.Web:
                    leadSource = GetDescription(OpportunityLeadSource.Web);
                    break;
            }
            return leadSource;
        }

        public static string GetStatus(int statusVal)
        {
            var status = string.Empty;
            switch (statusVal)
            {
                case (int)(LeadStatus.OpenNotContacted):
                    status = GetDescription(LeadStatus.OpenNotContacted);
                    break;
                case (int)(LeadStatus.ClosedConverted):
                    status = GetDescription(LeadStatus.ClosedConverted);
                    break;
                case (int)(LeadStatus.ClosedNotConverted):
                    status = GetDescription(LeadStatus.ClosedNotConverted);
                    break;
                case (int)(LeadStatus.WorkingContacted):
                    status = GetDescription(LeadStatus.WorkingContacted);
                    break;
                default:
                    status = GetDescription(LeadStatus.OpenNotContacted);
                    break;
            }
            return status;
        }
    }


    // you can map these from your SFDC account

    public enum NotificationType
    {
        Success = 1,
        Error = 2,
        Warning = 3
    }

    public enum LeadStatus
    {
        [Display(Name = "Open - Not Contacted")]
        [Description("Open - Not Contacted")]
        OpenNotContacted = 1,

        [Display(Name = "Working - Contacted")]
        [Description("Working - Contacted")]
        WorkingContacted = 2,

        [Display(Name = "Closed - Converted")]
        [Description("Closed - Converted")]
        ClosedConverted = 3,

        [Display(Name = "Closed - Not Converted")]
        [Description("Closed - Not Converted")]
        ClosedNotConverted = 4
    }

    public enum OpportunityStage
    {
        [Display(Name = "Prospecting")]
        [Description("Prospecting")]
        Prospecting = 1,

        [Display(Name = "Qualification")]
        [Description("Qualification")]
        Qualification = 2,

        [Display(Name = "Needs Analysis")]
        [Description("Needs Analysis")]
        NeedsAnalysis = 3,

        [Display(Name = "Value Proposition")]
        [Description("Value Proposition")]
        ValueProposition = 4,

        [Display(Name = "Id. Decision Makers")]
        [Description("Id. Decision Makers")]
        IdDecisionMakers = 5,

        [Display(Name = "Perception Analysis")]
        [Description("Perception Analysis")]
        PerceptionAnalysis = 6,

        [Display(Name = "Proposal/Price Quote")]
        [Description("Proposal/Price Quote")]
        ProposalPriceQuote = 7,

        [Display(Name = "Negotiation / Review")]
        [Description("Negotiation / Review")]
        NegotiationReview = 8,

        [Display(Name = "Closed Won")]
        [Description("Closed Won")]
        ClosedWon = 9,

        [Display(Name = "Closed Lost")]
        [Description("Closed Lost")]
        ClosedLost = 10
    }

    public enum OpportunityType
    {
        [Display(Name = "None")]
        [Description("None")]
        None = 1,

        [Display(Name = "Existing Customer - Upgrade")]
        [Description("Existing Customer - Upgrade")]
        ExistingCustomerUpgrade = 2,

        [Display(Name = "Existing Customer - Replacement")]
        [Description("Existing Customer - Replacement")]
        ExistingCustomerReplacement = 3,

        [Display(Name = "Existing Customer - Downgrade")]
        [Description("Existing Customer - Downgrade")]
        ExistingCustomerDowngrade = 4,

        [Display(Name = "New Customer")]
        [Description("New Customer")]
        NewCustomer = 5
    }

    public enum OpportunityLeadSource
    {
        [Display(Name = "None")]
        [Description("None")]
        None = 1,

        [Display(Name = "Web")]
        [Description("Web")]
        Web = 2,

        [Display(Name = "Phone Inquiry")]
        [Description("Phone Inquiry")]
        PhoneInquiry = 3,

        [Display(Name = "Partner Referral")]
        [Description("Partner Referral")]
        PartnerReferral = 4,

        [Display(Name = "Purchased List")]
        [Description("Purchased List")]
        PurchasedList = 5,

        [Display(Name = "Other")]
        [Description("Other")]
        Other = 6
    }
}