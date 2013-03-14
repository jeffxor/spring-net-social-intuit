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
    public class PaymentTemplateTests : AbstractIntuitTest
    {

        [Test]
        public void GetPayment()
        {
            long anyPaymentId = 47L;

            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/payment/v2/132477010/47")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(xmlResource("intuit-payment-response"), responseHeaders);


            Payment payment = intuit.PaymentOperations.GetPayment(anyPaymentId).Result;

            Assert.NotNull(payment);
            Assert.AreEqual(anyPaymentId.ToString(), payment.Id.Value);
        }

        [Test]
        public void testGetPayments()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/payments/v2/132477010")
                .AndExpectMethod(HttpMethod.POST)
                .AndRespondWith(xmlResource("intuit-paymentlist-response"), responseHeaders);

            Payment[] payments = intuit.PaymentOperations.GetPayments();

            Assert.NotNull(payments);
            Assert.AreEqual(1, payments.Length);
            Payment payment = payments[0];
            Assert.AreEqual("47", payment.Id.Value);
        }


        [Test]
        public void testCreatePayment()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/payment/v2/132477010")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody(xmlRequest("intuit-payment-createrequest"))
                .AndRespondWith(xmlResource("intuit-payment-response"), responseHeaders);

            Payment defaultPayment = PaymentBuilder.DefaultPayment()                                
                                    .Build();

            Payment payment = intuit.PaymentOperations.Create(defaultPayment).Result;

            Assert.NotNull(payment);
            Assert.AreEqual("47", payment.Id.Value);
        }

        [Test]
        public void testUpdatePayment()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/payment/v2/132477010/47")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody(xmlRequest("intuit-payment-updaterequest"))
                .AndRespondWith(xmlResource("intuit-payment-response"), responseHeaders);

            Payment defaultPayment = PaymentBuilder.DefaultPayment()
                                    .WithId(47L)
                                    .WithSyncToken("0")
                                    .WithMetaData("2010-09-13T02:09:18-05:00", "2010-09-13T04:09:18-05:00")
                                    .WithTotalAmount(decimal.Parse("50.00"))
                                    .Build();

            Payment payment = intuit.PaymentOperations.Update(defaultPayment).Result;

            Assert.NotNull(payment);
            Assert.AreEqual("47", payment.Id.Value);
        }

        [Test]
        public void testDeletePayment()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/payment/v2/132477010/47?methodx=delete")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody(xmlRequest("intuit-payment-deleterequest"))
                .AndRespondWith(xmlResource("intuit-payment-deleteresponse"), responseHeaders);

            Payment defaultPayment = PaymentBuilder.DefaultPayment()
                                    .WithId(47L)
                                    .WithSyncToken("1")
                                    .WithMetaData("2010-09-13T01:18:14-07:00", "2010-09-13T01:20:45-07:00")
                                    .Build();

            bool isDeleted = intuit.PaymentOperations.Delete(defaultPayment);

            Assert.True(isDeleted);
        }

    }
}
