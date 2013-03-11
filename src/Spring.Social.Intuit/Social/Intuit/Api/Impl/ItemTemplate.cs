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
    public class ItemTemplate : IItemOperations
    {
	    private bool isAuthorized;
	    private RestTemplate restTemplate;
	    private String companyId;

        public ItemTemplate(RestTemplate restTemplate, Boolean isAuthorized, String companyId)
        {
            this.restTemplate = restTemplate;
            this.isAuthorized = isAuthorized;
            this.companyId = companyId;
        }

        public Task<Item> GetItem(long id)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(2);
            vars.Add("companyId", this.companyId);
            vars.Add("itemID", id);
            return this.restTemplate.GetForObjectAsync<Item>("/resource/item/v2/{companyId}/{itemID}", vars);
        }

        public Item[] GetItems()
        {
            IDictionary<string, object> uriVariables = new Dictionary<string, object>(1);
            uriVariables.Add("company", this.companyId);
            NameValueCollection form = new NameValueCollection();
            SearchResults response = this.restTemplate.PostForObjectAsync<SearchResults>("/resource/items/v2/{company}", form, uriVariables).Result;
            if (response != null)
            {
                return ((Items)response.CdmCollections).Item;
            }
            return null;
        }

        public Task<Item> Update(Item item)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(2);
            vars.Add("companyId", this.companyId);
            vars.Add("itemID", item.Id.Value);
            return this.restTemplate.PostForObjectAsync<Item>("/resource/item/v2/{companyId}/{itemID}", item, vars);
        }

        public Task<Item> Create(Item item)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(1);
            vars.Add("companyId", this.companyId);
            return this.restTemplate.PostForObjectAsync<Item>("/resource/item/v2/{companyId}", item, vars);
        }

        public Task<Item> Save(Item item)
        {
            if (item.Id != null && item.Id.Value != null)
            {
                return Update(item);
            }
            else
            {
                return Create(item);
            }
        }

        public bool Delete(Item item)
        {
            IDictionary<string, object> vars = new Dictionary<string, object>(2);
            vars.Add("companyId", this.companyId);
            vars.Add("itemID", item.Id.Value);
            Item response = this.restTemplate.PostForObjectAsync<Item>("/resource/item/v2/{companyId}/{itemID}?methodx=delete", buildDelete(item), vars).Result;
		    return (response.Id == null);
        }

	    private Item buildDelete(Item item){
		    Item delete = new Item();
            delete.SyncToken = item.SyncToken;
		    delete.Id = item.Id;	
		    return delete;
	    }
    }
}
