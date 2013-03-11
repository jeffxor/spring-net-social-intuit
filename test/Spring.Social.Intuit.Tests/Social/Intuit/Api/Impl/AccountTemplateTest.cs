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
    public class AccountTemplateTests : AbstractIntuitTest
    {

        [Test]
        public void GetAccount()
        {
            long anyAccountId = 44L;

            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/account/v2/132477010/44")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(xmlResource("intuit-account-response"), responseHeaders);


            Account account = intuit.AccountOperations.GetAccount(anyAccountId).Result;

            Assert.NotNull(account);
            Assert.AreEqual(anyAccountId.ToString(), account.Id.Value);
            Assert.AreEqual("Loan Account", account.Name);
        }

        [Test]
        public void testGetAccounts()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/accounts/v2/132477010")
                .AndExpectMethod(HttpMethod.POST)
                .AndRespondWith(xmlResource("intuit-accountlist-response"), responseHeaders);

            Account[] accounts = intuit.AccountOperations.GetAccounts();

            Assert.NotNull(accounts);
            Assert.AreEqual(1, accounts.Length);
            Account account = accounts[0];
            Assert.AreEqual("44", account.Id.Value);
            Assert.AreEqual("Loan Account", account.Name);
        }


        [Test]
        public void testCreateAccount()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/account/v2/132477010")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody(xmlRequest("intuit-account-createrequest"))
                .AndRespondWith(xmlResource("intuit-account-response"), responseHeaders);

            Account defaultAccount = AccountBuilder.DefaultAccount()
                                    .WithOpeningBalanceDate("2010-05-14")
                                    .Build();

            Account account = intuit.AccountOperations.Create(defaultAccount).Result;

            Assert.NotNull(account);
            Assert.AreEqual("44", account.Id.Value);
            Assert.AreEqual("Loan Account", account.Name);
        }

        [Test]
        public void testUpdateAccount()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/account/v2/132477010/44")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody(xmlRequest("intuit-account-updaterequest"))
                .AndRespondWith(xmlResource("intuit-account-response"), responseHeaders);

            Account defaultAccount = AccountBuilder.DefaultAccount()
                                    .WithId(44L)
                                    .WithSyncToken("0")
                                    .WithMetaData("2010-09-13T02:09:18-05:00", "2010-09-13T04:09:18-05:00")
                                    .WithDescription("Loan account type")
                                    .Build();

            Account account = intuit.AccountOperations.Update(defaultAccount).Result;

            Assert.NotNull(account);
            Assert.AreEqual("44", account.Id.Value);
            Assert.AreEqual("Loan Account", account.Name);
        }

        [Test]
        public void testDeleteAccount()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/account/v2/132477010/44?methodx=delete")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody(xmlRequest("intuit-account-deleterequest"))
                .AndRespondWith(xmlResource("intuit-account-deleteresponse"), responseHeaders);

            Account defaultAccount = AccountBuilder.DefaultAccount()
                                    .WithId(44L)
                                    .WithSyncToken("1")
                                    .WithMetaData("2010-09-13T01:18:14-07:00", "2010-09-13T01:20:45-07:00")
                                    .Build();

            bool isDeleted = intuit.AccountOperations.Delete(defaultAccount);

            Assert.True(isDeleted);
        }

    }
}
