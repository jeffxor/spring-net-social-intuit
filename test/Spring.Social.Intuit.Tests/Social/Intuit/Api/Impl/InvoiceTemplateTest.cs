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
    public class InvoiceTemplateTests : AbstractIntuitTest
    {

        [Test]
        public void GetInvoice()
        {
            long anyInvoiceId = 28L;

            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/invoice/v2/132477010/28")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(xmlResource("intuit-invoice-response"), responseHeaders);


            Invoice invoice = intuit.InvoiceOperations.GetInvoice(anyInvoiceId).Result;

            Assert.NotNull(invoice);
            Assert.AreEqual(anyInvoiceId.ToString(), invoice.Id.Value);
        }

        [Test]
        public void testGetInvoices()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/invoices/v2/132477010")
                .AndExpectMethod(HttpMethod.POST)
                .AndRespondWith(xmlResource("intuit-invoicelist-response"), responseHeaders);

            Invoice[] invoices = intuit.InvoiceOperations.GetInvoices();

            Assert.NotNull(invoices);
            Assert.AreEqual(1, invoices.Length);
            Invoice invoice = invoices[0];
            Assert.AreEqual("28", invoice.Id.Value);
        }


        [Test]
        public void testCreateInvoice()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/invoice/v2/132477010")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody(xmlRequest("intuit-invoice-createrequest"))
                .AndRespondWith(xmlResource("intuit-invoice-response"), responseHeaders);

            Invoice defaultInvoice = InvoiceBuilder.DefaultInvoice()
                                    .WithTaxAmount(decimal.Parse("100.00"))
                                    .WithSalesTaxCodeId("1")
                                    .WithSalesTaxCodeName("IS_TAXABLE")
                                    .Build();

            Invoice invoice = intuit.InvoiceOperations.Create(defaultInvoice).Result;

            Assert.NotNull(invoice);
            Assert.AreEqual("28", invoice.Id.Value);
        }

        [Test]
        public void testUpdateInvoice()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/invoice/v2/132477010/28")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody(xmlRequest("intuit-invoice-updaterequest"))
                .AndRespondWith(xmlResource("intuit-invoice-response"), responseHeaders);

            Invoice defaultInvoice = InvoiceBuilder.DefaultInvoice()
                                    .WithId(28L)
                                    .WithSyncToken("0")
                                    .WithMetaData("2010-09-13T02:09:18-05:00", "2010-09-13T04:09:18-05:00")
                                    .WithTaxRate(decimal.Parse("0.5"))
                                    .Build();

            Invoice invoice = intuit.InvoiceOperations.Update(defaultInvoice).Result;

            Assert.NotNull(invoice);
            Assert.AreEqual("28", invoice.Id.Value);
        }

        [Test]
        public void testDeleteInvoice()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/invoice/v2/132477010/28?methodx=delete")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody(xmlRequest("intuit-invoice-deleterequest"))
                .AndRespondWith(xmlResource("intuit-invoice-deleteresponse"), responseHeaders);

            Invoice defaultInvoice = InvoiceBuilder.DefaultInvoice()
                                    .WithId(28L)
                                    .WithSyncToken("1")
                                    .WithMetaData("2010-09-13T01:18:14-07:00", "2010-09-13T01:20:45-07:00")
                                    .Build();

            bool isDeleted = intuit.InvoiceOperations.Delete(defaultInvoice);

            Assert.True(isDeleted);
        }

    }
}
