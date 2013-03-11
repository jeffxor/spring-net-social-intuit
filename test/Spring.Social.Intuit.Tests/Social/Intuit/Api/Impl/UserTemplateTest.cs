using System.Net;
using System.Collections.Generic;
using NUnit.Framework;
using Spring.Rest.Client.Testing;
using Spring.Http;
using Spring.IO;
using Spring.Social.Intuit.Api.Impl;
using Spring.Social.Intuit.Api;



namespace Spring.Social.Intuit.Api.Impl
{

    [TestFixture]
    public class UserTemplateTests : AbstractIntuitTest
    {

        [Test]
        public void GetUserProfile()
        {

            mockServer.ExpectNewRequest()
                .AndExpectUri("https://appcenter.intuit.com/api/v1/user/current")
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(xmlResource("intuit-user-response"), responseHeaders);

            IntuitProfile userProfile = intuit.UserOperations.GetUserProfileAsync();

            Assert.NotNull(userProfile);
            Assert.AreEqual("jeffxor.williams@gmail.com", userProfile.Name);
        }
    }
}
