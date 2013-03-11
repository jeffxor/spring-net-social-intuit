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
    public class InvoiceTemplate : IInvoiceOperations
    {
	    private bool isAuthorized;
	    private RestTemplate restTemplate;
	    private String companyId;

        public InvoiceTemplate(RestTemplate restTemplate, Boolean isAuthorized, String companyId)
        {
            this.restTemplate = restTemplate;
            this.isAuthorized = isAuthorized;
            this.companyId = companyId;
        }

        public Task<Invoice> GetInvoice(long id)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(2);
            vars.Add("companyId", this.companyId);
            vars.Add("invoiceID", id);
            return this.restTemplate.GetForObjectAsync<Invoice>("/resource/invoice/v2/{companyId}/{invoiceID}", vars);
        }

        public Invoice[] GetInvoices()
        {
            IDictionary<string, object> uriVariables = new Dictionary<string, object>(1);
            uriVariables.Add("company", this.companyId);
            NameValueCollection form = new NameValueCollection();
            SearchResults response = this.restTemplate.PostForObjectAsync<SearchResults>("/resource/invoices/v2/{company}", form, uriVariables).Result;
            if (response != null)
            {
                return ((Invoices)response.CdmCollections).Invoice;
            }
            return null;
        }

        public Task<Invoice> Update(Invoice invoice)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(2);
            vars.Add("companyId", this.companyId);
            vars.Add("invoiceID", invoice.Id.Value);
            return this.restTemplate.PostForObjectAsync<Invoice>("/resource/invoice/v2/{companyId}/{invoiceID}", invoice, vars);
        }

        public Task<Invoice> Create(Invoice invoice)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(1);
            vars.Add("companyId", this.companyId);
            return this.restTemplate.PostForObjectAsync<Invoice>("/resource/invoice/v2/{companyId}", invoice, vars);
        }

        public Task<Invoice> Save(Invoice invoice)
        {
            if (invoice.Id != null && invoice.Id.Value != null)
            {
                return Update(invoice);
            }
            else
            {
                return Create(invoice);
            }
        }

        public bool Delete(Invoice invoice)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(2);
            vars.Add("companyId", this.companyId);
            vars.Add("invoiceID", invoice.Id.Value);
            Invoice response = this.restTemplate.PostForObjectAsync<Invoice>("/resource/invoice/v2/{companyId}/{invoiceID}?methodx=delete", buildDelete(invoice), vars).Result;
		    return (response.Id == null);
        }

	    private Invoice buildDelete(Invoice invoice){
		    Invoice delete = new Invoice();
            delete.SyncToken = invoice.SyncToken;
		    delete.Id = invoice.Id;	
		    return delete;
	    }
    }
}
