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
    public class AccountTemplate : IAccountOperations
    {
	    private bool isAuthorized;
	    private RestTemplate restTemplate;
	    private String companyId;

        public AccountTemplate(RestTemplate restTemplate, Boolean isAuthorized, String companyId)
        {
            this.restTemplate = restTemplate;
            this.isAuthorized = isAuthorized;
            this.companyId = companyId;
        }

        public Task<Account> GetAccount(long id)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(2);
            vars.Add("companyId", this.companyId);
            vars.Add("accountID", id);
            return this.restTemplate.GetForObjectAsync<Account>("/resource/account/v2/{companyId}/{accountID}", vars);
        }

        public Account[] GetAccounts()
        {
            IDictionary<string, object> uriVariables = new Dictionary<string, object>(1);
            uriVariables.Add("company", this.companyId);
            NameValueCollection form = new NameValueCollection();
            SearchResults response = this.restTemplate.PostForObjectAsync<SearchResults>("/resource/accounts/v2/{company}", form, uriVariables).Result;
            if (response != null)
            {
                return ((Accounts)response.CdmCollections).Account;
            }
            return null;
        }

        public Task<Account> Update(Account account)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(2);
            vars.Add("companyId", this.companyId);
            vars.Add("accountID", account.Id.Value);
            return this.restTemplate.PostForObjectAsync<Account>("/resource/account/v2/{companyId}/{accountID}", account, vars);
        }

        public Task<Account> Create(Account account)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(1);
            vars.Add("companyId", this.companyId);
            return this.restTemplate.PostForObjectAsync<Account>("/resource/account/v2/{companyId}", account, vars);
        }

        public Task<Account> Save(Account account)
        {
            if (account.Id != null && account.Id.Value != null)
            {
                return Update(account);
            }
            else
            {
                return Create(account);
            }
        }

        public bool Delete(Account account)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(2);
            vars.Add("companyId", this.companyId);
            vars.Add("accountID", account.Id.Value);
            Account response = this.restTemplate.PostForObjectAsync<Account>("/resource/account/v2/{companyId}/{accountID}?methodx=delete", buildDelete(account), vars).Result;
		    return (response.Id == null);
        }

	    private Account buildDelete(Account account){
		    Account delete = new Account();
            delete.SyncToken = account.SyncToken;
		    delete.Id = account.Id;	
		    return delete;
	    }
    }
}
