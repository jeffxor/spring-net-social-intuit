using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

using Spring.IO;
using Spring.Social.OAuth1;
using Spring.Social.Intuit.Api;
using Spring.Social.Intuit.Api.Impl;
using Spring.Social.Intuit.Connect;

namespace Spring.Intuit.QuickStart
{
    class Program
    {

        // Register your own Twitter app at https://dev.twitter.com/apps/new with "Read, Write and Access direct messages" access type.
        // Set your consumer key & secret here
        private const string IntuitConsumerKey = "qyprdo3ucY4h732QWaErtouJ7peDwg";
        private const string IntuitConsumerSecret = "5tkxiVaU1DAaHMC0h6z7cx30sfxRY5fsMqLXIJBe";
        
        static void Main(string[] args)
        {
            try
            {
                IntuitServiceProvider intuitServiceProvider = new IntuitServiceProvider(IntuitConsumerKey, IntuitConsumerSecret);

                /* OAuth 'dance' */

                // Authentication using Out-of-band/PIN Code Authentication
                Console.Write("Getting request token...");
                OAuthToken oauthToken = intuitServiceProvider.OAuthOperations.FetchRequestTokenAsync("oob", null).Result;
                Console.WriteLine("Done");

                string authenticateUrl = intuitServiceProvider.OAuthOperations.BuildAuthorizeUrl(oauthToken.Value, null);
                Console.WriteLine("Redirect user for authentication: " + authenticateUrl);
                Process.Start(authenticateUrl);
                Console.WriteLine("Enter PIN Code from Twitter authorization page:");
                string pinCode = Console.ReadLine();

                Console.Write("Getting access token...");
                AuthorizedRequestToken requestToken = new AuthorizedRequestToken(oauthToken, pinCode);
                OAuthToken oauthAccessToken = intuitServiceProvider.OAuthOperations.ExchangeForAccessTokenAsync(requestToken, null).Result;
                Console.WriteLine("Done");

                /* API */

                IIntuit intuit = intuitServiceProvider.GetApi(oauthAccessToken.Value, oauthAccessToken.Secret);

                IntuitProfile IntuitProfile = intuit.UserOperations.GetUserProfileAsync();
                    Console.WriteLine("User is " + IntuitProfile.getName());
            }
            catch (AggregateException ae)
            {
                ae.Handle(ex =>
                {
                    Console.WriteLine(ex.Message);
                    //if (ex is TwitterApiException)
                    //{
                    //    Console.WriteLine(ex.Message);
                    //    return true;
                    //}
                    return false;
                });
            }
        }
    }
}
