using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Spring.Social.OAuth1;
using Spring.Social.Intuit.Api;
using Spring.Social.Intuit.Connect;
using Intuit.Sb.Cdm.V2;

namespace Spring.MvcQuickStart.Controllers
{
    public class IntuitController : Controller
    {
        // Register your own Intuit app at https://dev.intuit.com/apps/new with "Read, Write and Access direct messages" access type
        // Configure the Callback URL with 'http://localhost/Intuit/Callback'
        // Set your consumer key & secret here
        private const string IntuitConsumerKey = "qyprdo3ucY4h732QWaErtouJ7peDwg";
        private const string IntuitConsumerSecret = "5tkxiVaU1DAaHMC0h6z7cx30sfxRY5fsMqLXIJBe";

        IOAuth1ServiceProvider<IIntuit> intuitProvider = 
            new IntuitServiceProvider(IntuitConsumerKey, IntuitConsumerSecret);

        // GET: /Intuit/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Intuit/SignIn
        public ActionResult SignIn()
        {
            OAuthToken requestToken = intuitProvider.OAuthOperations.FetchRequestTokenAsync("http://localhost:8099/Intuit/Callback", null).Result;
            Session["RequestToken"] = requestToken;

            return Redirect(intuitProvider.OAuthOperations.BuildAuthenticateUrl(requestToken.Value, null));
        }

        // GET: /Intuit/Callback
        public ActionResult Callback(string oauth_verifier)
        {
            OAuthToken requestToken = Session["RequestToken"] as OAuthToken;
            AuthorizedRequestToken authorizedRequestToken = new AuthorizedRequestToken(requestToken, oauth_verifier);
            OAuthToken token = intuitProvider.OAuthOperations.ExchangeForAccessTokenAsync(authorizedRequestToken, null).Result;

            Session["AccessToken"] = token;

            IIntuit intuitClient = intuitProvider.GetApi(token.Value, token.Secret);
            IntuitProfile profile = intuitClient.UserOperations.GetUserProfileAsync();
            return View(profile);
        }

        // GET: /Intuit/Customers
        public ActionResult Customers()
        {
            OAuthToken token = Session["AccessToken"] as OAuthToken;
            IIntuit intuitClient = intuitProvider.GetApi(token.Value, token.Secret);

            Customer[] customers = intuitClient.CustomerOperations.GetCustomers();
            return View(customers);
        }

        // POST: /Intuit/UpdateStatus
        [HttpPost]
        public ActionResult UpdateStatus(string status)
        {
            OAuthToken token = Session["AccessToken"] as OAuthToken;
            IIntuit intuitClient = intuitProvider.GetApi(token.Value, token.Secret);

            //intuitClient.TimelineOperations.UpdateStatusAsync(status);

            return View();
        }
    }
}
