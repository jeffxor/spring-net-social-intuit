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
    public class ItemTemplateTests : AbstractIntuitTest
    {

        [Test]
        public void GetItem()
        {
            long anyItemId = 7L;

            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/item/v2/132477010/7")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(xmlResource("intuit-item-response"), responseHeaders);


            Item item = intuit.ItemOperations.GetItem(anyItemId).Result;

            Assert.NotNull(item);
            Assert.AreEqual(anyItemId.ToString(), item.Id.Value);
        }

        [Test]
        public void testGetItems()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/items/v2/132477010")
                .AndExpectMethod(HttpMethod.POST)
                .AndRespondWith(xmlResource("intuit-itemlist-response"), responseHeaders);

            Item[] items = intuit.ItemOperations.GetItems();

            Assert.NotNull(items);
            Assert.AreEqual(1, items.Length);
            Item item = items[0];
            Assert.AreEqual("7", item.Id.Value);
        }


        [Test]
        public void testCreateItem()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/item/v2/132477010")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody(xmlRequest("intuit-item-createrequest"))
                .AndRespondWith(xmlResource("intuit-item-response"), responseHeaders);

            Item defaultItem = ItemBuilder.DefaultItem()
                                    .Build();

            Item item = intuit.ItemOperations.Create(defaultItem).Result;

            Assert.NotNull(item);
            Assert.AreEqual("7", item.Id.Value);
        }

        [Test]
        public void testUpdateItem()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/item/v2/132477010/7")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody(xmlRequest("intuit-item-updaterequest"))
                .AndRespondWith(xmlResource("intuit-item-response"), responseHeaders);

            Item defaultItem = ItemBuilder.DefaultItem()
                                    .WithId(7L)
                                    .WithSyncToken("0")
                                    .WithMetaData("2010-09-13T02:09:18-05:00", "2010-09-13T04:09:18-05:00")
                                    .WithUnitPrice(decimal.Parse("1.5"))
                                    .Build();

            Item item = intuit.ItemOperations.Update(defaultItem).Result;

            Assert.NotNull(item);
            Assert.AreEqual("7", item.Id.Value);
        }

        [Test]
        public void testDeleteItem()
        {
            mockServer.ExpectNewRequest()
                .AndExpectUri("https://qbo.sbfinance.intuit.com/resource/item/v2/132477010/7?methodx=delete")
                .AndExpectMethod(HttpMethod.POST)
                .AndExpectBody(xmlRequest("intuit-item-deleterequest"))
                .AndRespondWith(xmlResource("intuit-item-deleteresponse"), responseHeaders);

            Item defaultItem = ItemBuilder.DefaultItem()
                                    .WithId(7L)
                                    .WithSyncToken("1")
                                    .WithMetaData("2010-09-13T01:18:14-07:00", "2010-09-13T01:20:45-07:00")
                                    .Build();

            bool isDeleted = intuit.ItemOperations.Delete(defaultItem);

            Assert.True(isDeleted);
        }

    }
}
