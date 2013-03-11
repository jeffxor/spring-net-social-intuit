using System.Net;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Rest.Client.Testing;
using Spring.Http;
using Spring.IO;
using Spring.Social.Intuit.Api.Impl;
using Spring.Social.Intuit.Api;
using Intuit.Sb.Cdm.V2;

namespace Spring.Social.Intuit.Api.Impl
{

    [TestFixture]
    public class CompanyMetaDataTemplateTests : AbstractIntuitTest
    {

        [Test]
        public void CompanyMetaData()
        {

            mockServer.ExpectNewRequest()
                .AndExpectUri("https://services.intuit.com/sb/company/v2/availableList")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(xmlResource("intuit-companylist"), responseHeaders);

            CompanyMetaData companyMetaData = intuit.CompanyMetaDataOperations.CompanyMetaData();

            Assert.NotNull(companyMetaData);
            Assert.AreEqual("132477010", companyMetaData.ExternalRealmId);
            Assert.AreEqual("Digital Assets", companyMetaData.QBNRegisteredCompanyName);

        }
    }
}
