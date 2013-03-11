using System.Net;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Rest.Client.Testing;
using Spring.Http;
using Spring.IO;
using Spring.Social.Intuit.Api.Impl;
using Spring.Social.Intuit.Api;
using Spring.Social.Intuit.Builder;
using Intuit.Sb.Cdm.V2;

namespace Spring.Social.Intuit.Api.Impl
{

    [TestFixture]
    public class CustomerTemplateTests : AbstractIntuitTest
    {

        [Test]
        public void GetCustomer()
        {
            long anyCustomerId = 17L;

            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/customer/v2/132477010/17")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(xmlResource("intuit-customer-response"), responseHeaders);


            Customer customer = intuit.CustomerOperations.GetCustomer(anyCustomerId).Result;

            Assert.NotNull(customer);
            Assert.AreEqual(anyCustomerId.ToString(), customer.Id.Value);
            Assert.AreEqual("John Doe", customer.Name);
        }

        [Test]
        public void testGetCustomers()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/customers/v2/132477010")
                .AndExpectMethod(HttpMethod.POST)
                .AndRespondWith(xmlResource("intuit-customerlist-response"), responseHeaders);

            Customer[] customers = intuit.CustomerOperations.GetCustomers();

            Assert.NotNull(customers);
            Assert.AreEqual(1, customers.Length);
            Customer customer = customers[0];
            Assert.AreEqual("17", customer.Id.Value);
            Assert.AreEqual("John Doe", customer.Name);
        }


        [Test]
        public void testCreateCustomer()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/customer/v2/132477010")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody(xmlRequest("intuit-customer-createrequest"))
                .AndRespondWith(xmlResource("intuit-customer-response"), responseHeaders);

            Customer defaultCustomer = CustomerBuilder.DefaultCustomer()
                                    .WithMobileNumber("(770) 349-1200")
                                    .WithFaxNumber("(770) 349-1300")
                                    .Build();

            Customer customer = intuit.CustomerOperations.Create(defaultCustomer).Result;

            Assert.NotNull(customer);
            Assert.AreEqual("17", customer.Id.Value);
            Assert.AreEqual("John Doe", customer.Name);
        }

        [Test]
        public void testUpdateCustomer()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/customer/v2/132477010/17")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody(xmlRequest("intuit-customer-updaterequest"))
                .AndRespondWith(xmlResource("intuit-customer-response"), responseHeaders);

            Customer defaultCustomer = CustomerBuilder.DefaultCustomer()
                                    .WithId(17L)
                                    .WithSyncToken("0")
                                    .WithMetaData("2010-09-13T02:09:18-05:00", "2010-09-13T04:09:18-05:00")
                                    .WithMobileNumber("(770) 349-4200")
                                    .WithAddressLine2("")
                                    .Build();

            Customer customer = intuit.CustomerOperations.Update(defaultCustomer).Result;

            Assert.NotNull(customer);
            Assert.AreEqual("17", customer.Id.Value);
            Assert.AreEqual("John Doe", customer.Name);
        }

        [Test]
        public void testDeleteCustomer()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/customer/v2/132477010/17?methodx=delete")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody(xmlRequest("intuit-customer-deleterequest"))
                .AndRespondWith(xmlResource("intuit-customer-deleteresponse"), responseHeaders);

            Customer defaultCustomer = CustomerBuilder.DefaultCustomer()
                                    .WithId(17L)
                                    .WithSyncToken("1")
                                    .WithMetaData("2010-09-13T01:18:14-07:00", "2010-09-13T01:20:45-07:00")
                                    .Build();

            bool isDeleted = intuit.CustomerOperations.Delete(defaultCustomer);

            Assert.True(isDeleted);
        }

    }
}
