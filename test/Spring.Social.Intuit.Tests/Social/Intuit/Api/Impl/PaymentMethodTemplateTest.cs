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
    public class PaymentMethodTemplateTests : AbstractIntuitTest
    {

        [Test]
        public void GetPaymentMethod()
        {
            long anyPaymentMethodId = 8L;

            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/payment-method/v2/132477010/8")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(xmlResource("intuit-paymentmethod-response"), responseHeaders);


            PaymentMethod paymentMethod = intuit.PaymentMethodOperations.GetPaymentMethod(anyPaymentMethodId).Result;

            Assert.NotNull(paymentMethod);
            Assert.AreEqual(anyPaymentMethodId.ToString(), paymentMethod.Id.Value);
        }

        [Test]
        public void testGetPaymentMethods()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/payment-methods/v2/132477010")
                .AndExpectMethod(HttpMethod.POST)
                .AndRespondWith(xmlResource("intuit-paymentmethodlist-response"), responseHeaders);

            PaymentMethod[] paymentMethods = intuit.PaymentMethodOperations.GetPaymentMethods();

            Assert.NotNull(paymentMethods);
            Assert.AreEqual(1, paymentMethods.Length);
            PaymentMethod paymentMethod = paymentMethods[0];
            Assert.AreEqual("8", paymentMethod.Id.Value);
        }

    }
}
