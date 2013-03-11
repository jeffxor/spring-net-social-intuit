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
    public class IntuitTemplate : AbstractOAuth1ApiBinding, IIntuit
    {
        private static readonly Uri API_URI_BASE = new Uri("https://qbo.sbfinance.intuit.com");

        private String companyId;
        private IAccountOperations accountOperations;
        private IInvoiceOperations invoiceOperations;
        private IItemOperations itemOperations;
        private ICustomerOperations customerOperations;
        private ICompanyMetaDataOperations companyMetaDataOperations;
        private IPaymentMethodOperations paymentMethodOperations;
        private IPaymentOperations paymentOperations;
        private IUserOperations userOperations;
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
            
	    }


        public IAccountOperations AccountOperations
        {
            get
            {
                this.InitSubApis();
                return this.accountOperations;
            }
        }

        public ICompanyMetaDataOperations CompanyMetaDataOperations
        {
            get
            {
                this.InitSubApis();
                return this.companyMetaDataOperations;
            }
        }
        
        public ICustomerOperations CustomerOperations
        {
            get
            {
                this.InitSubApis();
                return this.customerOperations;
            }
        }

        public IInvoiceOperations InvoiceOperations
        {
            get
            {
                this.InitSubApis();
                return this.invoiceOperations;
            }
        }

        public IItemOperations ItemOperations
        {
            get
            {
                this.InitSubApis();
                return this.itemOperations;
            }
        }

        public IPaymentMethodOperations PaymentMethodOperations
        {
            get
            {
                this.InitSubApis();
                return this.paymentMethodOperations;
            }
        }

        public IPaymentOperations PaymentOperations
        {
            get
            {
                this.InitSubApis();
                return this.paymentOperations;
            }
        }

        public IUserOperations UserOperations
        {
            get
            {
                this.InitSubApis();
                return this.userOperations;
            }
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
                accountOperations = new AccountTemplate(this.RestTemplate, IsAuthorized, this.companyId);
                customerOperations = new CustomerTemplate(this.RestTemplate, IsAuthorized, this.companyId);
                invoiceOperations = new InvoiceTemplate(this.RestTemplate, IsAuthorized, this.companyId);
                itemOperations = new ItemTemplate(this.RestTemplate, IsAuthorized, this.companyId);
                paymentMethodOperations = new PaymentMethodTemplate(this.RestTemplate, IsAuthorized, this.companyId);
                paymentOperations = new PaymentTemplate(this.RestTemplate, IsAuthorized, this.companyId);
                userOperations = new UserTemplate(this.RestTemplate, IsAuthorized);
            }
        }
        
        private bool isIntialized()
        {
            return companyId != null;
        }
    }
}
