using System;

using Spring.Social.OAuth1;
using Spring.Social.Intuit.Api;
using Spring.Social.Intuit.Api.Impl;

namespace Spring.Social.Intuit.Connect
{
    /// <summary>
    /// Intuit <see cref="IServiceProvider"/> implementation.
    /// </summary>
    /// <author>Jeffrey Williams</author>
    public class IntuitServiceProvider : AbstractOAuth1ServiceProvider<IIntuit>
    {
        /// <summary>
        /// Creates a new instance of <see cref="IntuitServiceProvider"/>.
        /// </summary>
        /// <param name="consumerKey">The application's API key.</param>
        /// <param name="consumerSecret">The application's API secret.</param>
        public IntuitServiceProvider(string consumerKey, string consumerSecret)
            : base(consumerKey, consumerSecret, new OAuth1Template(consumerKey, consumerSecret,
                "https://oauth.intuit.com/oauth/v1/get_request_token",
                "https://appcenter.intuit.com/Connect/Begin",
                "https://oauth.intuit.com/oauth/v1/get_access_token"))
        {
        }

        /// <summary>
        /// Returns an API interface allowing the client application to access protected resources on behalf of a user.
        /// </summary>
        /// <param name="accessToken">The API access token.</param>
        /// <param name="secret">The access token secret.</param>
        /// <returns>A binding to the service provider's API.</returns>
        public override IIntuit GetApi(string accessToken, string secret)
        {
            return new IntuitTemplate(this.ConsumerKey, this.ConsumerSecret, accessToken, secret);
        }
    }
}
