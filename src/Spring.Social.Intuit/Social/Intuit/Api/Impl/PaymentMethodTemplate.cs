using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Social.Intuit.Api;
using Spring.Rest.Client;
using Intuit.Sb.Cdm.V2;


namespace Spring.Social.Intuit.Api.Impl
{
    public class PaymentMethodTemplate : IPaymentMethodOperations
    {
        private bool isAuthorized;
        private RestTemplate restTemplate;
        private String companyId;

        public PaymentMethodTemplate(RestTemplate restTemplate, Boolean isAuthorized, String companyId)
        {
            this.restTemplate = restTemplate;
            this.isAuthorized = isAuthorized;
            this.companyId = companyId;
        }

        public Task<PaymentMethod> GetPaymentMethod(long id)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(2);
            vars.Add("companyId", this.companyId);
            vars.Add("paymentMethodID", id);
            return this.restTemplate.GetForObjectAsync<PaymentMethod>("/resource/payment-method/v2/{companyId}/{paymentMethodID}", vars);
        }

        public PaymentMethod[] GetPaymentMethods()
        {
            IDictionary<string, object> uriVariables = new Dictionary<string, object>(1);
            uriVariables.Add("company", this.companyId);
            NameValueCollection form = new NameValueCollection();
            SearchResults response = this.restTemplate.PostForObjectAsync<SearchResults>("/resource/payment-methods/v2/{company}", form, uriVariables).Result;
            if (response != null)
            {
                return ((PaymentMethods)response.CdmCollections).PaymentMethod;
            }
            return null;
        }
    }
}
