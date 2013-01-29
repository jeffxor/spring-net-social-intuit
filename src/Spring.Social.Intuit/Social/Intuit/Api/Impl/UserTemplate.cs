using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Social.Intuit.Api;
using Spring.Http;
using Spring.Rest.Client;
using Intuit.Sb.Cdm.V2;

namespace Spring.Social.Intuit.Api.Impl
{
    class UserTemplate : IUserOperations 
    {
        private const String companyDataUrl = "https://services.intuit.com/sb/company/v2/availableList";
	    private RestTemplate restTemplate;
        private Boolean isAuthorized;
        private String companyId;

        public UserTemplate(RestTemplate restTemplate, Boolean isAuthorized, String companyId)
        {
            this.restTemplate = restTemplate;
            this.isAuthorized = isAuthorized;
            this.companyId = companyId;
        }

        public IntuitProfile GetUserProfileAsync()
        {
            QboUser response = this.restTemplate.GetForObjectAsync<QboUser>("/rest/user/v2/{companyId}", this.companyId).Result;
            return new IntuitProfile(response);
        }
    }
}
