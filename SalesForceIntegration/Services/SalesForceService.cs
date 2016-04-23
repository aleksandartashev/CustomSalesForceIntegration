using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using Salesforce.Common;
using Salesforce.Force;

namespace SalesForceIntegration.Services
{
    public class SalesForceService
    {
        /* Don't forget to add your SalesForce credentials in Web.config! */

        private readonly string _consumerkey = WebConfigurationManager.AppSettings["consumerkey"];
        private readonly string _consumersecret = WebConfigurationManager.AppSettings["consumersecret"];
        private readonly string _username = WebConfigurationManager.AppSettings["username"];
        private readonly string _password = WebConfigurationManager.AppSettings["password"];
        private readonly string _securitytoken = WebConfigurationManager.AppSettings["securitytoken"];
        private readonly string _url = "https://login.salesforce.com/services/oauth2/token";

        public SalesForceService()
        {
            AuthenticationClient = new AuthenticationClient();
        }

        public AuthenticationClient AuthenticationClient { get; private set; }

        public async Task<ForceClient> CreateForceClient()
        {
            await AuthenticationClient.UsernamePasswordAsync(
                _consumerkey,
                _consumersecret,
                _username,
                _password + _securitytoken,
                _url);

            return new ForceClient(
                AuthenticationClient.InstanceUrl,
                AuthenticationClient.AccessToken,
                AuthenticationClient.ApiVersion);
        }
    }
}