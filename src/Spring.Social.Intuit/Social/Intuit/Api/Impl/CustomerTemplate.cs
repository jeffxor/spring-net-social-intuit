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
    public class CustomerTemplate : ICustomerOperations
    {
	    private bool isAuthorized;
	    private RestTemplate restTemplate;
	    private String companyId;

        public CustomerTemplate(RestTemplate restTemplate, Boolean isAuthorized, String companyId)
        {
            this.restTemplate = restTemplate;
            this.isAuthorized = isAuthorized;
            this.companyId = companyId;
        }

        public Task<Customer> GetCustomer(long id)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(2);
            vars.Add("companyId", this.companyId);
            vars.Add("customerID", id);
            return this.restTemplate.GetForObjectAsync<Customer>("/resource/customer/v2/{companyId}/{customerID}", vars);
        }

        public Customer[] GetCustomers()
        {
            IDictionary<string, object> uriVariables = new Dictionary<string, object>(1);
            uriVariables.Add("company", this.companyId);
            NameValueCollection form = new NameValueCollection();
            SearchResults response = this.restTemplate.PostForObjectAsync<SearchResults>("/resource/customers/v2/{company}", form, uriVariables).Result;
            if (response != null)
            {
                return ((Customers)response.CdmCollections).Customer;
            }
            return null;
        }

        public Task<Customer> Update(Customer customer)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(2);
            vars.Add("companyId", this.companyId);
            vars.Add("customerID", customer.Id.Value);
            return this.restTemplate.PostForObjectAsync<Customer>("/resource/customer/v2/{companyId}/{customerID}", customer, vars);
        }

        public Task<Customer> Create(Customer customer)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(1);
            vars.Add("companyId", this.companyId);
            return this.restTemplate.PostForObjectAsync<Customer>("/resource/customer/v2/{companyId}", customer, vars);
        }

        public Task<Customer> Save(Customer customer)
        {
            if (customer.Id != null && customer.Id.Value != null)
            {
                return Update(customer);
            }
            else
            {
                return Create(customer);
            }
        }

        public bool Delete(Customer customer)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(2);
            vars.Add("companyId", this.companyId);
            vars.Add("customerID", customer.Id.Value);
            Customer response = this.restTemplate.PostForObjectAsync<Customer>("{baseURL}/resource/customer/v2/{companyId}/{customerID}?methodx=delete", buildDelete(customer), vars).Result;
		    return (response.Id == null);
        }

	    private Customer buildDelete(Customer customer){
		    Customer delete = new Customer();
            delete.SyncToken = customer.SyncToken;
		    delete.Id = customer.Id;	
		    return delete;
	    }
    }
}
