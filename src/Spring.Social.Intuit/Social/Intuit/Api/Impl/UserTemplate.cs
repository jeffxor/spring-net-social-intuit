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
	    private RestTemplate restTemplate;
        private Boolean isAuthorized;

        public UserTemplate(RestTemplate restTemplate, Boolean isAuthorized)
        {
            this.restTemplate = restTemplate;
            this.isAuthorized = isAuthorized;
        }

        public IntuitProfile GetUserProfileAsync()
        {
            UserResponse response = this.restTemplate.GetForObjectAsync<UserResponse>("https://appcenter.intuit.com/api/v1/user/current").Result;
            return new IntuitProfile(response.User);
        }
    }
}
