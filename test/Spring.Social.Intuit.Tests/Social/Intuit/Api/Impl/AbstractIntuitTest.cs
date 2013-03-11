using System.Net;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Rest.Client.Testing;
using Spring.Http;
using Spring.IO;
using Spring.Social.Intuit.Api.Impl;
using System.Text.RegularExpressions;


namespace Spring.Social.Intuit.Api.Impl
{

    public class AbstractIntuitTest
    {

        protected IntuitTemplate intuit;
        protected MockRestServiceServer mockServer;
        protected HttpHeaders responseHeaders;

        [SetUp]
        public void Setup()
        {
            intuit = new IntuitTemplate("API_KEY", "API_SECRET", "ACCESS_TOKEN", "ACCESS_TOKEN_SECRET");
            mockServer = MockRestServiceServer.CreateServer(intuit.RestTemplate);
            responseHeaders = new HttpHeaders();
            responseHeaders.ContentType = MediaType.TEXT_XML;

            mockServer.ExpectNewRequest()
                .AndExpectUri("https://services.intuit.com/sb/company/v2/availableList")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(xmlResource("intuit-companylist"), responseHeaders);
        }

        protected IResource xmlResource(string filename)
        {
            return new AssemblyResource(filename + ".xml", typeof(AbstractIntuitTest));
        }

        protected string xmlRequest(string filename)
        {
            IResource requestContent = new AssemblyResource(filename + ".xml", typeof(AbstractIntuitTest));
            StreamReader reader = new StreamReader(requestContent.GetStream());
            string request = reader.ReadToEnd();
            return Regex.Replace(request, @"[\t\r\n]", string.Empty);
        }
        
        [TearDown]
        public void TearDown()
        {
            mockServer.Verify();
        }
    }
}
