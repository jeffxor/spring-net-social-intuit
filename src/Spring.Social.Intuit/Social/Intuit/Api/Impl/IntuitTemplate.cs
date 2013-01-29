using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Rest.Client;
using Spring.Http.Converters;
using Spring.Http.Converters.Xml;
using Spring.Social.OAuth1;
using Spring.Social.Intuit.Api;
using Spring.Social.Intuit.Api.Impl;


namespace Spring.Social.Intuit.Api.Impl
{
    class IntuitTemplate : AbstractOAuth1ApiBinding, IIntuit
    {
        private static readonly Uri API_URI_BASE = new Uri("https://qbo.sbfinance.intuit.com");

        private String companyId;
        private CompanyMetaDataOperations companyMetaDataOperations;
        private IUserOperations userOperations;
        private ICustomerOperations customerOperations;

        /// <summary>
        /// Create a new instance of <see cref="TwitterTemplate"/>.
        /// </summary>
        /// <param name="consumerKey">The application's API key.</param>
        /// <param name="consumerSecret">The application's API secret.</param>
        /// <param name="accessToken">An access token acquired through OAuth authentication with Twitter.</param>
        /// <param name="accessTokenSecret">An access token secret acquired through OAuth authentication with Twitter.</param>
        public IntuitTemplate(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret) 
            : base(consumerKey, consumerSecret, accessToken, accessTokenSecret)
        {
            this.InitSubApis();
	    }

        public IUserOperations UserOperations
        {
            get { return this.userOperations; }
        }

        public ICustomerOperations CustomerOperations
        {
            get { return this.customerOperations; }
        }

        /// <summary>
        /// Enables customization of the <see cref="RestTemplate"/> used to consume provider API resources.
        /// </summary>
        /// <remarks>
        /// An example use case might be to configure a custom error handler. 
        /// Note that this method is called after the RestTemplate has been configured with the message converters returned from GetMessageConverters().
        /// </remarks>
        /// <param name="restTemplate">The RestTemplate to configure.</param>
        protected override void ConfigureRestTemplate(RestTemplate restTemplate)
        {
            restTemplate.BaseAddress = API_URI_BASE;
            //restTemplate.ErrorHandler = new TwitterErrorHandler();
        }

        /// <summary>
        /// Returns a list of <see cref="IHttpMessageConverter"/>s to be used by the internal <see cref="RestTemplate"/>.
        /// </summary>
        /// <remarks>
        /// This implementation adds <see cref="SpringJsonHttpMessageConverter"/> and <see cref="ByteArrayHttpMessageConverter"/> to the default list.
        /// </remarks>
        /// <returns>
        /// The list of <see cref="IHttpMessageConverter"/>s to be used by the internal <see cref="RestTemplate"/>.
        /// </returns>
        protected override IList<IHttpMessageConverter> GetMessageConverters()
        {
            IList<IHttpMessageConverter> converters = base.GetMessageConverters();
            converters.Add(new FormHttpMessageConverter());
            converters.Add(new XmlSerializableHttpMessageConverter());
            converters.Add(new StringHttpMessageConverter());
            
            return converters;
        }

        private void InitSubApis()
        {
            
            if (!isIntialized())
            {
                companyMetaDataOperations = new CompanyMetaDataTemplate(this.RestTemplate);
                this.companyId = this.companyMetaDataOperations.CompanyMetaData().ExternalRealmId;
                userOperations = new UserTemplate(this.RestTemplate, IsAuthorized, this.companyId);
                customerOperations = new CustomerTemplate(this.RestTemplate, IsAuthorized, this.companyId);                
            }

        }

        private bool isIntialized()
        {
            return companyId != null;
        }
    }
}
