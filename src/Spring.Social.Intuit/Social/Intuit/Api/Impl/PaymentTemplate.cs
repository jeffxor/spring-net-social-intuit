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
    public class PaymentTemplate : IPaymentOperations
    {
	    private bool isAuthorized;
	    private RestTemplate restTemplate;
	    private String companyId;

        public PaymentTemplate(RestTemplate restTemplate, Boolean isAuthorized, String companyId)
        {
            this.restTemplate = restTemplate;
            this.isAuthorized = isAuthorized;
            this.companyId = companyId;
        }

        public Task<Payment> GetPayment(long id)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(2);
            vars.Add("companyId", this.companyId);
            vars.Add("paymentID", id);
            return this.restTemplate.GetForObjectAsync<Payment>("/resource/payment/v2/{companyId}/{paymentID}", vars);
        }

        public Payment[] GetPayments()
        {
            IDictionary<string, object> uriVariables = new Dictionary<string, object>(1);
            uriVariables.Add("company", this.companyId);
            NameValueCollection form = new NameValueCollection();
            SearchResults response = this.restTemplate.PostForObjectAsync<SearchResults>("/resource/payments/v2/{company}", form, uriVariables).Result;
            if (response != null)
            {
                return ((Payments)response.CdmCollections).Payment;
            }
            return null;
        }

        public Task<Payment> Update(Payment payment)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(2);
            vars.Add("companyId", this.companyId);
            vars.Add("paymentID", payment.Id.Value);
            return this.restTemplate.PostForObjectAsync<Payment>("/resource/payment/v2/{companyId}/{paymentID}", payment, vars);
        }

        public Task<Payment> Create(Payment payment)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(1);
            vars.Add("companyId", this.companyId);
            return this.restTemplate.PostForObjectAsync<Payment>("/resource/payment/v2/{companyId}", payment, vars);
        }

        public Task<Payment> Save(Payment payment)
        {
            if (payment.Id != null && payment.Id.Value != null)
            {
                return Update(payment);
            }
            else
            {
                return Create(payment);
            }
        }

        public bool Delete(Payment payment)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(2);
            vars.Add("companyId", this.companyId);
            vars.Add("paymentID", payment.Id.Value);
            Payment response = this.restTemplate.PostForObjectAsync<Payment>("/resource/payment/v2/{companyId}/{paymentID}?methodx=delete", buildDelete(payment), vars).Result;
		    return (response.Id == null);
        }

	    private Payment buildDelete(Payment payment){
		    Payment delete = new Payment();
            delete.SyncToken = payment.SyncToken;
		    delete.Id = payment.Id;	
		    return delete;
	    }
    }
}
