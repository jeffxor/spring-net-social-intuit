using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Social.Intuit.Api;
using Intuit.Sb.Cdm.V2;
using Spring.Rest.Client;
using System.Xml.Serialization;

namespace Spring.Social.Intuit.Api.Impl
{
    class CompanyMetaDataTemplate : ICompanyMetaDataOperations
    {
        private const String companyDataUrl = "https://services.intuit.com/sb/company/v2/availableList";

        private RestTemplate restTemplate;

        public CompanyMetaDataTemplate(RestTemplate restTemplate)
        {
            this.restTemplate = restTemplate;
        }

        public CompanyMetaData CompanyMetaData()
        {
            RestResponse companyDataRestResponse = this.restTemplate.GetForObjectAsync<RestResponse>(CompanyMetaDataTemplate.companyDataUrl).Result;
            if (companyDataRestResponse != null)
            {
                return companyDataRestResponse.CompaniesMetaData.CompanyMetaData[1];
            }
            return null;
        }
    }
}
